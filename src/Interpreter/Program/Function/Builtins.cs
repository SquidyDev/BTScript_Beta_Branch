using BTScript.Interpreter.Type;
using BTScript.Interpreter.Var;
using BTScript.Interpreter.Configuration;
using BTScript.Debug;
using System.Collections;
using BTScript.Interpreter.Utility.String;
using BTScript.Interpreter.Utility.Array;

namespace BTScript.Interpreter.Func
{
	static class Builtins
	{
		static Debugger debug = new Debugger();

		/*A List of Actions, Correspond to the builtin functions*/
		static Dictionary<string, Action<object[]>?> builtins_function = new Dictionary<string, Action<object[]>?>();

		/*Return a function based on his key*/
		public static Action<object[]>? GetFunction(string name) 
		{
			if (!Exist(name)) 
			{
				debug.Fatal($"Couldn't Find Builtin function {name}");
				return null;
			}

			return builtins_function[name];
		}
		
		/*Use to add a new function in program runtime*/
		public static void AddFunction(string name, Action<object[]>? func) 
		{
			if (Exist(name)) 
			{
				debug.Fatal($"Couldn't add function {name} to builins function because it already exists");
				return;
			}

			builtins_function.Add(name, func);
		}

		public static bool Exist(string name) => builtins_function.ContainsKey(name);

		public static void Call(string name, string args) 
		{
			object[] func_args = args.Split('$');
			Action<object[]>? func = GetFunction(name);

			if (func == null) return;

			func.Invoke(func_args);
		}

		public static void Init() 
		{
			builtins_function.Add("print", Print);
			builtins_function.Add("input", Input);
			builtins_function.Add("get_stack", Get_Stack);
			builtins_function.Add("to_string", ToString);
			debug = Interpreter_Config.mainDebugger;
		}

		public static void Return(string value) 
		{
			string valueToPuchInMemory = $"\"{value}\"";
			Variable_Manager.ChangeVariable("ret", valueToPuchInMemory);
		}

		static void Print(object[] args)
		{
			string? message = String_Utility.GetString(args[0].ToString().Trim());
			string? ending_character = args.Length >= 2 ? args[1].ToString()?.Trim() : "\n";

			if(ending_character != "\n") 
			{
				ending_character = String_Utility.GetString(ending_character);
			}

			Console.Write($"{message}{ending_character}");

			Return(message);
		}

		static void Input(object[] args) 
		{
			Print(args);

			string input = Console.ReadLine();
			Return(input);
		}

		static void ToString(object?[] args) 
		{
			string?[] strArgs = Array.ConvertAll(args, x => x?.ToString());
			string? line = Array_Utility.JoinArray<string, char>(strArgs, ' ').Trim();

			if (Variable_Manager.Exist(line)) 
			{
				string? oldValue = Variable_Manager.GetVariable(line).Value.ToString();
				string newValue = $"\"{oldValue}\"";
				Variable_Manager.ChangeVariable("ret", newValue);
			}
		}

		static void Get_Stack(object[] args) 
		{
			Console.WriteLine(Variable_Manager.ToString());
		}
	}
}
