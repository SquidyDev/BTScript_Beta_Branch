using BTScript.Interpreter.Event;
using BTScript.Interpreter.Configuration;
using BTScript.Interpreter.Utility.Array;
using BTScript.Interpreter.Type;

namespace BTScript.Interpreter.Var
{
    static class Variable_Manager
    {
        /*The list of variables in our program*/
        static Dictionary<string, Variable> variables  = new Dictionary<string, Variable>();

        /*Return a printable string containing a list of the variable in memory*/
        public static new string ToString() 
        {
            string output = "";

            output += "Begin Stack.\n";
            foreach(Variable? var in variables.Values) 
            {
                output += $"\t {var.Name} ({var.Type.typeName}) : {var.Value}\n";
            }
            output += "End Stack.";

            return output;
        }

        /*Add a variable (don't work for copy)*/
        public static void AddVariable(string[] lineTokens)
        {
            string name = lineTokens[0];

            if (Exist(name)) Interpreter_Config.mainDebugger.Fatal($"Variable {name} already exists in memory");

            string rawValue = Array_Utility.JoinArray<string, char>(lineTokens, ' ', startIndex: 4);
            string value = rawValue == "?" ? Interpreter_Type.GetTypeByName(rawValue).baseValue.ToString() : rawValue;

            Variable newVariable = new Variable(name, value, lineTokens[2]);
            variables.Add(name, newVariable);
        }

        /*Adds a new variable with value copied from an other variable*/
        public static void CopyVariable(string[] lineTokens) 
        {
            string name = lineTokens[0];
            string copyName = lineTokens[4];
            string type = lineTokens[2];

            /*For debugging*/
            if (!Exist(copyName)) Interpreter_Config.mainDebugger.Fatal($"Unable to copy from source {copyName} to {name} because source doesn't exist in memory");
            if (Exist(name)) Interpreter_Config.mainDebugger.Fatal($"Unable to copy from source {copyName} to {name} because dest already exist in memory");

            Variable varCopy = new Variable(name, GetVariable(copyName).Value.ToString(), type);
            variables.Add(name, varCopy);
        }

        /*Use to remove a variable*/
        public static void RemoveVariable(string[] lineTokens) 
        {
            string name = lineTokens[1];

            if (!Exist(name)) { Interpreter_Config.mainDebugger.Fatal($"Variable {name} doesn't exist in the current context (can't delete a non existing variable)"); }

            variables.Remove(name);
        }

        /*Use to change a variable value*/
        public static void ChangeVariable(string[] lineTokens) 
        {
            if (!Exist(lineTokens[0])) Interpreter_Config.mainDebugger.Fatal($"Unknown variable {lineTokens[0]}");

            Variable choosedVariable = variables[lineTokens[0]];

            string newValue = Array_Utility.JoinArray<string, char>(lineTokens, ' ', startIndex: 2);
            Type.Type type = Variable_Type.GetTypeFromValue(newValue, choosedVariable.Type);

            if(choosedVariable.Type != type) 
            {
                Interpreter_Config.mainDebugger.Fatal($"Variable {choosedVariable.Name} is type {choosedVariable.Type} but his new value is type of {type}");
            }

            choosedVariable.ChangeVariable(Test(newValue.Trim(), choosedVariable.Type));
        }

        public static void ChangeVariable(string name, object newValue) 
        {
            ChangeVariable($"{name} = {newValue}".Split(' '));
        }

        /*Return The Variable with the name KEY*/
        public static Variable GetVariable(string key)
        {
            if(Exist(key)) return variables[key];

            Interpreter_Config.mainDebugger.Fatal($"Couldn't find variable {key} in memory");

            return null;
        }

        public static string? Test(string key, Type.Type type) 
        {
			if (Exist(key) && GetVariable(key).Type == type) 
            {
                return GetVariable(key).Value.ToString();
            }else if (!Exist(key) && Variable_Type.GetTypeFromValue(key, type) == type) 
            {
                return key;
            }

            Interpreter_Config.mainDebugger.Fatal($"Wrong type for Value {key} type must be {type}");
            return null;
        }

        public static string? Test(string key) 
        {
			if (Exist(key)) { return GetVariable(key).Value.ToString();} else { return key; }
        }

        /*Return true if the variable name exist in memory*/
        public static bool Exist(string name) => variables.ContainsKey(name);
    }
}
