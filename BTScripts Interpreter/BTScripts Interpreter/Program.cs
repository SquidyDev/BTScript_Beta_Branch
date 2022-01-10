using BTScript.Interpreter.Configuration;
using BTScript.Interpreter.Var;

namespace BTScript
{
    static class Program 
    {
        static void Main(string[] args) 
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("BTScripts Interpreter -- Version 0.0.1 -- Running on Windows.");

            string pathToMainFile = "Project/main.bts";
            Interpreter_Config.Config(pathToMainFile);
            Console.ReadLine();
        }
    }
}