using System;
using System.Reflection;

class Program
{
    static void Main(string[] args)
    {
        string dllPath = "K1.dll";
        Assembly assembly = Assembly.LoadFrom(dllPath);

        Console.WriteLine($"Inspecting assembly: {assembly.FullName}\n");

        foreach (Type type in assembly.GetTypes())
        {
            Console.WriteLine($"Type: {type.FullName}");
            foreach (MethodInfo method in type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static))
            {
                Console.WriteLine($"  Method: {method.Name}");
                foreach (ParameterInfo parameter in method.GetParameters())
                {
                    Console.WriteLine($"    Parameter: {parameter.Name}, Type: {parameter.ParameterType}");
                }
            }
        }

        // Example of calling a method with reflection
        CallMethodWithReflection(assembly);
    }

    static void CallMethodWithReflection(Assembly assembly)
    {
        try
        {
            // Assuming there is a type and method to call, adjust these as needed
            string typeName = "YourNamespace.YourClass"; // Replace with actual type
            string methodName = "YourMethod"; // Replace with actual method

            Type type = assembly.GetType(typeName);
            if (type == null)
            {
                Console.WriteLine($"Type {typeName} not found in assembly.");
                return;
            }

            MethodInfo method = type.GetMethod(methodName);
            if (method == null)
            {
                Console.WriteLine($"Method {methodName} not found in type {typeName}.");
                return;
            }

            // Create an instance of the type if it's not static
            object instance = Activator.CreateInstance(type);

            // Prepare parameters for the method
            object[] parameters = new object[] { "example parameter" }; // Adjust as needed

            // Call the method
            object result = method.Invoke(instance, parameters);
            Console.WriteLine($"Method {methodName} called, result: {result}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
