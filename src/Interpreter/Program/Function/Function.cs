using BTScript.Interpreter.Var;
using BTScript.Interpreter.Type;

namespace BTScript.Interpreter.Func
{ 
	public class Function 
	{
		string name;
		string[] lines;
		public Dictionary<string, Type.Type> argsList = new Dictionary<string, Type.Type>();

		public void Run() 
		{

		}

		public Function(string name) 
		{
			this.name = name;
			this.lines = new string[]{
				"call in print \"Null Function.\""
			};
		}

		public Function(string name, string[] lines, string args) 
		{
			this.name = name;

		}
	}
}