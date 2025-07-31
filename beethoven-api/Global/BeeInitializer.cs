using System;
using beethoven_api.Database;
using beethoven_api.Database.DBModels;
using beethoven_api.Global.Security;
using log4net;

namespace beethoven_api.Global;

public class BeeInitializer
{
    private static readonly ILog log = LogManager.GetLogger(typeof(BeeInitializer));

    #region Global parameters
    public static void InitGlobalParameters(BeeDBContext context)
    {
        // Create param Bearer expirancy
        CreateParameterIfNotExist(
            context,
            BeeApplicationConstants.PARAM_BEARER_EXPIRATION_HOURS,
            "1",
            "int",
            "Bearer token expiration in hours"
        );

        // Create param document storage path
        CreateParameterIfNotExist(
            context,
            BeeApplicationConstants.PARAM_DOCUMENT_STORAGE_PATH,
            "documents",
            "string",
            "Storage path for documents"
        );

        // Create param lucene storage path
        CreateParameterIfNotExist(
            context,
            BeeApplicationConstants.PARAM_LUCENE_INDEX_PATH,
            "lucene/index",
            "string",
            "Storage path for lucene index"
        );
    }

    public static void CreateParameterIfNotExist(BeeDBContext context, string parameterName, string parameterValue, string parameterType, string? parameterDesc = null)
    {
        log.Debug($"Checking if parameter {parameterName} exists...");
        // Check if param already exists
        if (!context.GlobalParameters.Any(p => p.Name.Equals(parameterName)))
        {
            GlobalParameter param = new()
            {
                Name = parameterName,
                Value = parameterValue,
                Type = parameterType,
                Description = parameterDesc
            };
            context.GlobalParameters.Add(param);
            context.SaveChanges();
            log.Debug($"Parameter {parameterName} created");
        }
        else
        {
            log.Debug($"Parameter {parameterName} has value {parameterValue}");
        }
    }

    #endregion
    #region Default User
    public static void CreateDefaultUser(BeeDBContext context)
    {
        if (!context.Users.Any())
        {
            log.Info("No user found in DB, ceating default user...");
            User user = new()
            {
                Firstname = "Master",
                Lastname = "Admin",
                Name = "Master Admin",
                Email = "admin@beethoven.fr",
                Password = BeeHash.GetHash("admin"),
                Avatar = "avatar-default.png"
            };
            context.Users.Add(user);
            context.SaveChanges();
        }
        else
        {
            log.Info("Default user already created");
        }
    }
    #endregion

}
