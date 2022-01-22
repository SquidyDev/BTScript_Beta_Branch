using BTScript.Interpreter.Configuration;
using BTScript.Interpreter.Type;

namespace BTScript.Interpreter.Var
{
    class Variable
    {
        /*Variable Data*/
        string name;
        object value;
        Type.Type type;
        int reference; //The number of reference to the variable
        List<Variable> referencedVariable = new List<Variable>(); //The List of referenced variable
        string scopeCode;
        
        /*Use to get acces to data (all are readonly except Type)*/
        public string Name { get { return name; } }
        public object Value { get { return value; } }
        public Type.Type Type {
            get { return type; } 
            set
            {
            
            }
        }
        public int Reference
        {
            get { return reference; }
            set { reference = value; }
        }
        public string ScopeCode { get { return scopeCode; } }
        
        /*Default Constructor For Variable*/
        public Variable(string name, string value, string type, string scopeCode) 
        {
            this.name = name;
            this.type = Variable_Type.GetTypeFromValue(value, Interpreter_Type.GetTypeByName(type));

            if(type != this.type.typeName) 
            {
                Interpreter_Config.mainDebugger.Fatal($"Error variable {name} type is {type} but his value type is {this.type.typeName}");
            }

            this.value = value.Trim() == "?" ? this.type.baseValue : value;
            this.scopeCode = scopeCode;
            this.reference = 0;
        }

        public void ChangeVariable(string newValue) 
        {
            this.value = newValue;

            foreach(Variable var in referencedVariable) 
            {
                var.ChangeVariable(newValue);
            }
        }

        /*Delete all copy of this variable*/
        public void DeleteVariable() 
        {
            if (referencedVariable.Count == 0) return;

            foreach(Variable var in referencedVariable) 
            {
                var.DeleteVariable();
            }
        }

        //Return true if code and scopeCode correspond
        //Otherwise return false
        public bool HasScopeCode(string code) => scopeCode == code;
    }
}
