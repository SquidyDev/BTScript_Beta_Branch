namespace BTScript.Interpreter.State
{
    /*Contain the data to know if the interpreter needs to jump line*/
    [System.Serializable]
    public struct State 
    {
        public int lineIndex;
        public bool needToJump;

        public State(int index, bool needToJump) 
        {
            lineIndex = index;
            this.needToJump = needToJump;
        }
    }

    /*This class is use to check if the interpreter needs to jump to a specific line*/
    static class Interpreter_State
    {
        static State currentState;

        /*Return currentState*/
        public static State GetCurrentState() => currentState;

        /*Use to modificate the current state*/
        public static void SetState(int lineIndex, bool needToJump) 
        {
            currentState = new State(lineIndex, needToJump);
        }

        /*Use to modificate the current state based on an other state*/
        public static void SetState(State newState) { currentState = newState; }
    }
}
