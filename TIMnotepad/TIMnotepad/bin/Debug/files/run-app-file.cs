using System;
using System.IO;
 
class Program
{
    static void Main(string[] args)
    {
        Console.Write("Ввидите число> ");
        Int64 inp     = Convert.ToInt64(Console.ReadLine());
        string output = Math.Pow(inp,Convert.ToString(inp).Length).ToString();
        Console.WriteLine(output);
        Console.ReadKey();
    }
}