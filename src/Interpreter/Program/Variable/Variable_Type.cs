using BTScript.Interpreter.Type;
using BTScript.Interpreter.Configuration;
using BTScript.Interpreter.Var;

namespace BTScript.Interpreter.Var
{
    static class Variable_Type
    {
        /*Return a type in Type List based on the variable value*/
        public static Type.Type GetTypeFromValue(string value, Type.Type baseType) 
        {
            string trimedValue = Variable_Manager.Test(value.Trim()).Trim();

            if (trimedValue == "?") return baseType;
            
            /*Try all the possible base type*/
            if(IsString(trimedValue)) { return Interpreter_Type.GetTypeByName("string"); }
            if(IsChar(trimedValue)) { return Interpreter_Type.GetTypeByName("char"); }
            if(IsBool(trimedValue)) { return Interpreter_Type.GetTypeByName("bool"); }
            if(IsInt(trimedValue)) { return Interpreter_Type.GetTypeByName("int"); }
            if (IsFloat(trimedValue)) { return Interpreter_Type.GetTypeByName("float"); }

            /*If no type is detected throw an error and stop the program*/
            Interpreter_Config.mainDebugger.Fatal($"Unknow type for value {value}");
            return new Type.Type("NullType");
        }

        /*Method to check if a string is a special type*/
        /*string to test must be trimed before*/
        public static bool IsString(string trimedValue) => trimedValue[0] == '"' && trimedValue.Last() == '"';
        public static bool IsChar(string trimedValue) => trimedValue.Length == 3 && trimedValue[0] == '\'' && trimedValue[2] == '\'';
        public static bool IsBool(string trimedValue) => trimedValue == "true" || trimedValue == "false";
        public static bool IsInt(string trimedValue) { try { Convert.ToInt64(trimedValue); return true; } catch { return false; } }
        public static bool IsFloat(string trimedValue) { try { Convert.ToDouble(trimedValue.Replace('.', ',')); return true; } catch { return false;} }
    }
}
