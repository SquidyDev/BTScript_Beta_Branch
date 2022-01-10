using BTScript.Interpreter.Type;
using BTScript.Interpreter.Configuration;
using BTScript.Interpreter.Var;

namespace BTScript.Interpreter.Event
{
    /*This enum contains all the different event type that the interpreter can understand*/
    /*Information About all event can be founded in "Event Type.txt" file*/
    public enum EventType 
    {
        VARIABLE_ASSIGN = 0,
        VARIABLE_DELETE,
        VARIABLE_CHANGE,
        VARIABLE_COPY,
        FUNCTION_CALL,
        FUNCTION_DEFINE,
        IN_FUNCTION_CALL,
        COMMENT,
        EMPTY_LINE
    }

    static class Interpreter_Event
    {
        /*Get the current event associated to the line*/
        /*If no associated event is found, throw a fatal error*/
        public static EventType? GetEventType(string[] lineTokens) 
        {
            if (string.IsNullOrWhiteSpace(string.Join('\t', lineTokens))) return EventType.EMPTY_LINE;

            /*Check if the current line is a variable assign*/
            if(lineTokens[1] == ":" && lineTokens[3] == ":" && Interpreter_Type.IsType(Interpreter_Type.GetTypeByName(lineTokens[2])) && !Variable_Manager.Exist(lineTokens[4])) return EventType.VARIABLE_ASSIGN;

            /*Check if the current line is a variable delete*/
            /*TO DO : Check if the deleted objecct is a variable*/
            if(lineTokens[0] == "del") return EventType.VARIABLE_DELETE;

            /*Check if the current line is a variable change*/
            /*TO DO : Check if the changed object is a variable*/
            if(lineTokens[1] == "=") return EventType.VARIABLE_CHANGE;

            /*Check if the current line is a variable copy*/
            if(lineTokens[1] == ":" && lineTokens[3] == ":" && Interpreter_Type.IsType(Interpreter_Type.GetTypeByName(lineTokens[2])) && Variable_Manager.Exist(lineTokens[4])) return EventType.VARIABLE_COPY;

            /*Check if the current line is a function call*/
            /*TO DO : Check if the called object is a function*/
            if (lineTokens[0] == "call" && lineTokens[1] != "in") return EventType.FUNCTION_CALL;

            /*Check if the current line is a function define*/
            if(lineTokens[1] == "::" && lineTokens.Last() == "{") return EventType.FUNCTION_DEFINE;

            /*Check if the current line is an in function call*/
            /*TO DO : Check if the called builtin object exists*/
            if(lineTokens[0] == "call" && lineTokens[1] == "in" && (lineTokens[3] == ":"  || lineTokens[3] == "?")) return EventType.IN_FUNCTION_CALL;

            /*Check if the current line is a comment*/
            if(lineTokens[0] == "$" || lineTokens[0][1] == '$') return EventType.COMMENT;

            Interpreter_Config.mainDebugger.Fatal($"Unknow event for line : {string.Join(" ", lineTokens)}");
            return null;
        }
    }
}
