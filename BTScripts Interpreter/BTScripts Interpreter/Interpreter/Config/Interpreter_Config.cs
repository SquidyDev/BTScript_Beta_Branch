using BTScript.Debug;
using BTScript.Interpreter.Type;
using BTScript.Interpreter.Func;
using BTScript.Interpreter.Var;

namespace BTScript.Interpreter.Configuration
{
    static class Interpreter_Config
    {
        static string? pathToMainFile;
        static string? pathToMainDirectory;
        static string? mainFile;

        public static Debugger mainDebugger = new Debugger(LogLevel.DEBUG);
        public static FileDebugger mainFileDebugger = new FileDebugger();

        public static Interpreter mainInterpreter;

        //Set all the Intepreter Configuration Variable (Debugger, path to file) also Configurate the interpreter types
        public static void Config(string mainFilePath) 
        {
            /*Initialize all Path related information*/
            pathToMainDirectory = Path.GetDirectoryName(mainFilePath) + "/";
            pathToMainFile = mainFilePath;
            mainFile = Path.GetFileName(mainFilePath);

            /*Initialize all the main debugger*/
            mainFileDebugger = new FileDebugger(LogLevel.INFO, "log/" + FileDebugger.GetDate());
            mainDebugger.Attach(mainFileDebugger);

            /*Initialize the different pre build type*/
            Interpreter_Type.Init();

            /*Initialize all the builtins function*/
            Builtins.Init();

            /*Initialize some core variables*/
            Variable_Manager.AddVariable("ret : string : \"Return Value\"".Split(' '));
            Variable_Manager.AddVariable("event : string : \"Last Event\"".Split(' '));

            /*Initialize the Interpreter*/
            mainInterpreter = new Interpreter(mainFile, pathToMainDirectory);
            mainInterpreter.InitInterpreter();
        }
    }
}
