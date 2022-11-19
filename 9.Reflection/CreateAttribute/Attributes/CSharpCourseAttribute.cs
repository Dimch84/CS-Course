using System;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class CSharpCourseAttribute : Attribute
{
    public CSharpCourseAttribute(string name)
    {
        this.Name = name;
    }

    public string Name { get; private set; }
}
