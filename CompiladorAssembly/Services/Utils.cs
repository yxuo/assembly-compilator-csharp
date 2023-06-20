namespace CompiladorAssembly.Services
{
    public static class Utils
    {

        public static List<string> SplitWithDelimiter(string input, string delimiter)
        {
            string[] substrings = input.Split(delimiter);
            List<string> result = new();

            if (input == delimiter)
            {
                result.Add(delimiter);
                return result;
            }

            for (int i = 0; i < substrings.Length; i++)
            {

                bool isCurrentSubstringExists = substrings[i].Length > 0;
                bool isNextSubstringExists = i + 1 < substrings.Length && substrings[i + 1].Length > 0;

                bool isFirstSubstringValid = i == 0 && substrings.Length > 1 && isCurrentSubstringExists;
                bool isNextSubstringValid = i > 0 && isNextSubstringExists;

                // add
                result.Add(substrings[i]);

                // add between
                if ((isFirstSubstringValid && isNextSubstringExists) || isNextSubstringValid)
                {
                    result.Add(delimiter);
                }

                // add first / last
                if (substrings[i].Length == 0)
                {
                    result[^1] = delimiter;
                }
            }

            return result;
        }


        public static List<string> SplitWithDelimiter(string input, List<string> delimiters)
        {
            List<string> resultList = new()
            {
                input
            };

            foreach (string delimiter in delimiters)
            {
                List<string> tempList = new();

                foreach (string substring in resultList)
                {
                    List<string> splitResult = SplitWithDelimiter(substring, delimiter);
                    tempList.AddRange(splitResult);

                }

                resultList = tempList;
            }
            return resultList;
        }

    }
}