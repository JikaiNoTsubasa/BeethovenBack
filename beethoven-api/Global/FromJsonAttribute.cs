namespace beethoven_api.Global;

public class FromJsonAttribute : Attribute
{
    public string? Name { get; set; }
    public FromJsonAttribute() {}

    public FromJsonAttribute(string name)
    {
        Name = name;
    }
}