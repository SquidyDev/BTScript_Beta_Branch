using BTScript.Interpreter.Var;
using BTScript.Interpreter.Type;

namespace BTScript.Interpreter.Utility.String
{
	static class String_Utility
	{
        public static string CheckStringUntil(string str, char start_separator, char end_separator, int startIndex = 0)
        {
            string output = "";

            int need_to_skip = 0;

            for (int i = startIndex; i < str.Length; i++)
            {
                output += str[i];
                if (str[i] == start_separator) need_to_skip++;
                if (str[i] == end_separator)
                {
                    need_to_skip--;

                    if (need_to_skip <= 0)
                    {
                        break;
                    }
                }
            }

            return output;
        }

        //Use to for example remove "" from a string
        // "Hello World" -> Hello World
        public static string RemoveLeadingEnding(string? str, char startChar, char endChar) 
        {
            string input = str.TrimEnd(' ');

            if (input == null) return "";

            string output = "";

            if(input[0] != startChar) 
            {
                output += input[0];
            }

            output += input.Substring(1, input.Length - 2);

            if(input.Last() != endChar) 
            {
                output += input.Last();
            }

            return output;
        }

        public static string? GetString(string baseStr) => RemoveLeadingEnding(Variable_Manager.Test(baseStr, Interpreter_Type.GetTypeByName("string")), '"', '"');
    }
}
