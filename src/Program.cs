using BTScript.Interpreter.Configuration;
using BTScript.Interpreter.Var;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using BTScript.Debug;

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

        static void EvaluationTest() 
        {
            var result = CSharpScript.EvaluateAsync<double>("(24.0 * 24) / 25 - 45 + 5").Result;
            Console.WriteLine(result);

            var result2 = CSharpScript.EvaluateAsync<bool>("10 <= 15").Result;
            Console.WriteLine(result2);

            var result3 = CSharpScript.EvaluateAsync<string>("\"Hello\" + \" World\"").Result;
            Console.WriteLine(result3);
        }

        static void DebbugerTest() 
        {
            Debugger myDebugger = new Debugger(LogLevel.INFO);
            Popup_Debugger myPopupDebugger = new Popup_Debugger(LogLevel.ERROR);
            myDebugger.Attach(myPopupDebugger);

            myDebugger.Debug("Test");
            Thread.Sleep(1000);
            myDebugger.Info("Test");
            Thread.Sleep(1000);
            myDebugger.Warn("Test");
            Thread.Sleep(1000);
            myDebugger.Error("Test");
            Thread.Sleep(1000);
            myDebugger.Fatal("Test");
        }
    }
}