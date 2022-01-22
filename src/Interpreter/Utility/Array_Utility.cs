namespace BTScript.Interpreter.Utility.Array
{
    static class Array_Utility
    {
        /*Same behaviour has string.Join() but allows to join any type of joinable array*/
        /*Also allow to set a start and end index*/
        /*Type must be non nullable by default*/
        public static T? JoinArray<T, U>(T[] array, U separator, int startIndex = 0, int endIndex=-1) 
        {
            T? output = default(T);

            if(endIndex == -1) endIndex = array.Length;

            for(int i = startIndex; i < endIndex; i++) 
            {
                output = (dynamic?)output + (dynamic?)array[i] + (dynamic?)separator;
            }
            return output;
        }

        /*Use to slice an array
         * base Array : The Base T array
         * endCharater : wil reduce number of start by one (if number of start is 0, the function will return output)
         * startCharacter 
         */
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
                output.Add(currentItem);

                if(currentItem == endChar)
				{
                    if(numberOfStart == 0) 
                    {
                        break;
                    }

                    numberOfStart--;
				}

                if(currentItem == startChar) 
                {
                    numberOfStart++;
                }
			}

            return output.ToArray();
        }
    }
}
