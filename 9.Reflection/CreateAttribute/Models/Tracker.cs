using System;
using System.Linq;
using System.Reflection;

public class Tracker
{
    public void PrintMethodsByAuthor()
    {
        Type classType = typeof(Program);
        MethodInfo[] methods = classType.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);

        foreach (MethodInfo method in methods)
        {
            if (method.CustomAttributes.Any(a => a.AttributeType == typeof(CSharpCourseAttribute)))
            {
                object[] attributes = method.GetCustomAttributes(false)
                    .Where(a => a is CSharpCourseAttribute)
                    .Select(a => a)
                    .ToArray();

                foreach (CSharpCourseAttribute attribute in attributes)
                {
                    Console.WriteLine($"{method.Name} is written by {attribute.Name}");
                }
            }
        }
    }
}