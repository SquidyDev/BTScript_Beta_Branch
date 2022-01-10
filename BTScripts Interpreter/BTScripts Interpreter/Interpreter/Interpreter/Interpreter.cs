using BTScript.Interpreter.Configuration;
using BTScript.Interpreter.Var;
using BTScript.Interpreter.State;
using BTScript.Interpreter.Event;
using BTScript.Interpreter.Utility.Array;
using BTScript.Interpreter.Func;
using BTScript.Debug;

namespace BTScript.Interpreter
{
    class Interpreter
    {
        string mainFile;
        string mainDirectoryPath;
        string mainFilePath;

        Debugger mainDebugger;

        /*Main Constructor for the interpreter*/
        public Interpreter(string mainFile, string mainDirectoryPath) 
        {
            this.mainFile = mainFile;
            this.mainDirectoryPath = mainDirectoryPath;
            mainFilePath = Path.Combine(mainDirectoryPath, mainFile);
            mainDebugger = Interpreter_Config.mainDebugger;
            Interpreter_State.SetState(0, false);
        }

        /*Check if the path to main file is valid*/
        /*Check if the main file extension is valid*/
        public void InitInterpreter() 
        {
            if (!File.Exists(mainFilePath)) mainDebugger.Fatal($"Path to main file '{mainFilePath}' is invalid.");

            if (Path.GetExtension(mainFilePath) != ".bts") mainDebugger.Fatal($"Invalid main file extension, main file extension must be .bts not {Path.GetExtension(mainFilePath)}");

            Interprete(File.ReadAllLines(mainFilePath));
        }

        /*Use to interprete a group of lines*/
        /*Call Interprete Line for each Line*/
        public void Interprete(string[] lines) 
        {
            for (int i = 0; i < lines.Length; i++)
            {
                if(Interpreter_State.GetCurrentState().needToJump == true) 
                {
                    i = Interpreter_State.GetCurrentState().lineIndex - 1;
                    Interpreter_State.SetState(0, false);
                    continue;
                }

                string currentLine = lines[i];

                InterpreteLine(currentLine);
            }
        }

        public void InterpreteLine(string line) 
        {
            string[] lineTokens = line.Split(' ');

            /*Get the associated event to the current line*/
            EventType? lineEvent = Interpreter_Event.GetEventType(lineTokens);

            mainDebugger.Debug(lineEvent.ToString());

            /*Skip Line if it is a comment*/
            if (lineEvent == EventType.COMMENT || lineEvent == EventType.EMPTY_LINE) return;

            if(lineEvent == EventType.VARIABLE_ASSIGN) 
            {
                Variable_Manager.AddVariable(lineTokens);
            }

            if(lineEvent == EventType.VARIABLE_DELETE) 
            {
                Variable_Manager.RemoveVariable(lineTokens);
            }

            if(lineEvent == EventType.VARIABLE_CHANGE) 
            {
                Variable_Manager.ChangeVariable(lineTokens);
            }

            if(lineEvent == EventType.IN_FUNCTION_CALL) 
            {
                string name = lineTokens[2];

                string args = "\"\"$\"\"";

				if (lineTokens[3] == ":") 
                {
                    args = Array_Utility.JoinArray<string, char>(lineTokens, ' ', startIndex: 4);
                }

                Builtins.Call(name, args);
            }

            Variable_Manager.ChangeVariable("event", $"\"{lineEvent}\"");
        }
    }
}
