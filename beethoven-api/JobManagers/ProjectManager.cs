using System;
using beethoven_api.Database;
using beethoven_api.Database.DBModels;
using beethoven_api.Global.Query;
using Microsoft.EntityFrameworkCore;

namespace beethoven_api.JobManagers;

public class ProjectManager(BeeDBContext context) : BeeManager(context)
{

    public Project CreateProject(string name, long ownerId, long userId, bool? initializePhases = null)
    {
        Project prj = new()
        {
            Name = name,
            OwnerId = ownerId
        };

        prj.MarkCreated(userId);

        _context.Projects.Add(prj);
        _context.SaveChanges();

        if (initializePhases is not null && initializePhases.Value)
        {
            CreateDefaultPhases(prj);
        }

        // Create default permissions
        ProjectPermission p1 = new()
        {
            ProjectId = prj.Id,
            UserId = ownerId
        };
        p1.SetAllPermissions(true);
        _context.ProjectPermissions.Add(p1);
        _context.SaveChanges();

        return prj;
    }

    public void CreateDefaultPhases(Project project)
    {
        ProjectPhase p1 = new()
        {
            Name = "Initialisation",
            Project = project
        };
        project.CurrentPhase = p1;
        _context.ProjectPhases.Add(p1);
        _context.SaveChanges();

        ProjectPhase p2 = new()
        {
            Name = "Préparation",
            Project = project
        };
        _context.ProjectPhases.Add(p2);
        _context.SaveChanges();

        ProjectPhase p3 = new()
        {
            Name = "Réalisation",
            Project = project
        };
        _context.ProjectPhases.Add(p3);
        _context.SaveChanges();

        ProjectPhase p4 = new()
        {
            Name = "Cloture",
            Project = project
        };
        _context.ProjectPhases.Add(p4);
        _context.SaveChanges();

        ProjectPhase p5 = new()
        {
            Name = "Archivage",
            Project = project
        };
        _context.ProjectPhases.Add(p5);
        _context.SaveChanges();

        p1.NextPhase = p2;
        p2.NextPhase = p3;
        p3.NextPhase = p4;
        p4.NextPhase = p5;
        p2.PreviousPhase = p1;
        p3.PreviousPhase = p2;
        p4.PreviousPhase = p3;
        p5.PreviousPhase = p4;
        _context.SaveChanges();
    }

    private IQueryable<Project> GenerateProjectQuery()
    {
        return _context.Projects
            .Include(p => p.Phases)
            .Include(p => p.Permissions)
            .Include(p => p.Owner)
            ;
    }

    public Project FetchProject(long id)
    {
        return GenerateProjectQuery().FirstOrDefault(p => p.Id == id) ?? throw new Exception("Project not found");
    }

    public List<ProjectPhase> FetchProjectPhases(long id)
    {
        var phases = GenerateProjectQuery().FirstOrDefault(p => p.Id == id)?.Phases ?? [];
        var first = phases.FirstOrDefault(p => p.PreviousPhaseId == null);
        if (first == null) return [];
        var orderedPhases = new List<ProjectPhase>();
        var current = first;
        while (current != null)
        {
            orderedPhases.Add(current);
            current = phases.FirstOrDefault(p => p.PreviousPhaseId == current.Id);
        }
        return orderedPhases;
    }

    public List<Project> FetchProjectsForUser(long userId)
    {
        List<Project> projects = [..GenerateProjectQuery()
            .Where(p => p.Permissions!.Any(p => p.UserId == userId && p.CanRead == true))
        ];
        return projects;
    }

    public Project? FetchProjectForUser(long projectId, long userId)
    {
        Project? project = GenerateProjectQuery()
        .FirstOrDefault(p => p.Id == projectId && p.Permissions!.Any(p => p.UserId == userId && p.CanRead == true));
        return project;
    }

    public List<ProjectPermission> FetchProjectPermissions(long projectId, long? userId = null)
    {
        return [.._context.ProjectPermissions
            .Where(p => p.ProjectId == projectId)
            .Where(userId is not null, p => p.UserId == userId)
            ];
    }

    public List<Document> FetchProjectDocuments(long projectId)
    {
        return [.._context.Documents
            .Include(d => d.Versions)
            .Where(d => d.EntityId == projectId)
            ];
    }
}
