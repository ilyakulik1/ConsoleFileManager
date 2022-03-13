namespace ConsoleFileManager
{
    internal static class StringMethods
    {
        public static string RemoveFromEnd(string input, int split)
        {
            char[] chars = new char[input.Length - split];
            for (int i = 0; i < input.Length - split; i++)
            {
                chars[i] = input[i];
            }
            return new string(chars);
        }


        public static string RemoveFromBegin(string input, int split)
        {
            char[] chars = new char[split];
            for (int i = 0; i < split; i++)
            {
                chars[i] = input[i];
            }
            return new string(chars);
        }


        public static string RemoveAfterChar(string input, char ch)
        {
            int counter = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == ch)
                {
                    break;
                }
                counter++;
            }
            char[] chars = new char[counter];
            for (int i = 0; i < chars.Length; i++)
            {
                chars[i] = input[i];
            }
            return new string(chars);
        }


        public static string RemoveBeforeChar(string input, char ch)
        {
            int counter = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == ch)
                {
                    break;
                }
                counter++;
            }
            char[] chars = new char[input.Length - counter];
            for (int i = 0; i < chars.Length; i++)
            {
                chars[i] = input[i + counter];
            }
            return new string(chars);
        }


        public static string[] AddElement(string[] input, string value)
        {
            string[] newInput = new string[input.Length + 1];
            for (int i = 0; i < newInput.Length; i++)
            {
                if (i == input.Length)
                {
                    newInput[i] = StringMethods.RemoveFromEnd(value, 1);
                    break;
                }
                newInput[i] = input[i];
            }
            return newInput;
        }


        public static string[] Split(string input, char ch)
        {
            int count = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == ch)
                {
                    count++;
                }
            }
            string[] args = new string[count + 1];
            count = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == ch)
                {
                    count++;
                    continue;
                }
                args[count] += input[i];
            }
            return args;
        }
    }
}
