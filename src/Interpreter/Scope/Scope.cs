using BTScript.Interpreter.Configuration;
using BTScript.Interpreter.Var;

namespace BTScript.Interpreter.Scope
{
	/*Use to generate new scopes
	  Delete all variables with those scopes
	  Acces important scopes (such as global)
	 */
	static class Interpreter_Scope
	{
		//The Length of a scope code
		static int scopeCodeLength = 15;

		//The Global Scope code (readonly)
		static string globalScope = "";

		public static string GlobalScope { get { return globalScope; } }

		//The list of scope currently in use (to avoid copys)
		static List<string> scopeList = new List<string>();

		//All ASCII chars that can be used to generate a scope code
		public static readonly string ascii = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!\"#$%&'()*+,-./:;<=>?@[]^_`{|}~";

		//Use to generate random chars for the scope codes
		static Random random = new Random();

		//Create a new scope code and return it 
		//Also stores the scope code in scopeList (to avoid duplication)
		public static string GenerateScope() 
		{
			string newScope = "";

			while (true)
			{
				for (int i = 0; i < scopeCodeLength; i++)
				{
					char randomChar = ascii[random.Next(ascii.Length)];
					newScope += randomChar;
				}

				if (!scopeList.Contains(newScope)) break;
			}

			scopeList.Add(newScope);
			Interpreter_Config.mainDebugger.Debug($"Generated new scope code : {newScope}");
			return newScope;
		}

		//Delete all the variable with scopeCode has their scopeCode
		public static void ClearScope(string scopeCode) 
		{
			if (!IsValid(scopeCode)) 
			{
				Interpreter_Config.mainDebugger.Fatal($"Unknow Scope {scopeCode}. Program Exit...");
			}

			foreach(Variable variable in Variable_Manager.GetVariables()) 
			{
				if (variable.HasScopeCode(scopeCode)) Variable_Manager.RemoveVariable(variable.Name);
			}

			scopeList.Remove(scopeCode);
		}

		//Return true if scopeList contains scopeCode otherwise return false
		public static bool IsValid(string scopeCode) => scopeList.Contains(scopeCode);
		
		//Return "global" if the scopeCode correspond to the global scope code
		public static string GetScope(string scopeCode) 
		{
			return scopeCode == globalScope ? "global" : scopeCode;
		}

		//Create a code for the global scope
		public static void Init() 
		{
			globalScope = GenerateScope();
		}
	}
}
