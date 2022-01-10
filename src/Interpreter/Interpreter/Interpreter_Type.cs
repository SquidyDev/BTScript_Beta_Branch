using BTScript.Interpreter.Configuration;

namespace BTScript.Interpreter.Type
{
    [System.Serializable]
    public struct Type 
    {
        /*Type Data*/
        public string typeName;
        public bool inVariable;
        public bool inFunction;
        public object? baseValue;

        /*Constructor With name only (variable & function will be true so)*/
        public Type(string name, object? baseValue=null) 
        {
            typeName = name;
            inVariable = true;
            inFunction = true;
            this.baseValue = baseValue;
        }

        /*Constructor with all data in types*/
        public Type(string name, bool variable, bool function, object? baseValue) 
        {
            typeName = name;
            inFunction = function;
            inVariable = variable;
            this.baseValue = baseValue;
        }

        /*Return the name of the type*/
        public override string ToString()
        {
            return typeName;
        }

        /*return true if both typeA and typeB are exactly the same*/
        public static bool operator==(Type typeA, Type typeB) 
        {
            if (typeA.baseValue == typeA.baseValue && typeA.typeName == typeB.typeName && typeA.inFunction == typeB.inFunction && typeA.inVariable == typeB.inVariable) return true;

            return false;
        }

        /*Return true if typeA and typeB are not the same*/
        public static bool operator!=(Type typeA, Type typeB) 
        {
            return !(typeA == typeB);
        }
    }

    /*Contains All The current Function, Variable Types in the interpreter*/
    static class Interpreter_Type
    {
        static List<Type> types = new List<Type>();

        static Type[] builtinTypes = { new Type("int", baseValue:0), new Type("float", baseValue:0.0), new Type("bool", baseValue:"false"), new Type("char", baseValue:"'0'"), new Type("string", baseValue:"\"\""), new Type("pointer", baseValue:".-> null") };

        /*Init all the prebuiltin types*/
        /*Should Only be call one time at program start*/
        public static void Init() 
        {
            foreach(Type type in builtinTypes) 
            {
                types.Add(type);
            }
        }

        /*Add a custom type during program execution*/
        public static void AddType(Type newType) 
        {
            if (types.Contains(newType)) 
            {
                Interpreter_Config.mainDebugger.Fatal($"Type {newType.typeName} already exist, two type can't have the same name.");
            }

            types.Add(newType);
        }

        /*Remove special type during program execution*/
        public static void DeleteType(Type type) 
        {
            if (!types.Contains(type)) 
            {
                Interpreter_Config.mainDebugger.Fatal($"Can not remove type {type.typeName} from type list because it does not exist.");
            }

            types.Remove(type);
        }

        /*Check if the type is in the types list*/
        public static bool IsType(Type type) 
        {
            return types.Contains(type);
        }

        /*Return a type object based on his name (nullable)*/
        public static Type GetTypeByName(string typeName) 
        {
            foreach(Type type in types) 
            {
                if (type.typeName == typeName) return type;
            }

            Interpreter_Config.mainDebugger.Fatal($"Unknow type name {typeName}");
            return new Type("NullType");
        }
    }
}
