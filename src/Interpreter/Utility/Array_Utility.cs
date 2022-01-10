namespace BTScript.Interpreter.Utility.Array
{
    static class Array_Utility
    {
        /*Same behaviour has string.Join() but allows to join any type of joinable array*/
        /*Also allow to set a start and end index*/
        /*Type must be non nullable by default*/
        public static T JoinArray<T, U>(T[] array, U separator, int startIndex = 0, int endIndex=-1) 
        {
            T output = default(T);

            if(endIndex == -1) endIndex = array.Length;

            for(int i = startIndex; i < endIndex; i++) 
            {
                output = (dynamic)output + (dynamic)array[i] + (dynamic)separator;
            }
            return output;
        }

        public static T[] SliceArrayUntil<T>(T[] baseArray, T endCharacter, T startCharacter, int startIndex = 0, int endIndex = -1) 
        {
            if(endIndex < 0) endIndex = baseArray.Length;

            List<T> output = new List<T>();
            dynamic endChar = endCharacter;
            dynamic startChar = startCharacter;

            int numberOfStart = 0;

            foreach(T item in baseArray)
			{
                dynamic currentItem = item;

                if(currentItem == endChar)
				{
                    if(numberOfStart == 0) 
                    {
                        break;
                    }

                    numberOfStart--;
				}
			}
        }
    }
}
