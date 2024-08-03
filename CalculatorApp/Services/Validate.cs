namespace CalculatorApp.Services
{
    public static class Validate
    {
        public static string FormatErrors(string input)
        {
            if (string.IsNullOrEmpty(input))
                return "Invalid Data";

            if (input.Contains('/'))
            {
                int pos = input.IndexOf('/');
                if(pos > 0)
                {
                    var findZero = input[..(pos + 1)];
                    if (findZero != null)
                    {
                        return "Divide by Zero Error";
                    }
                }
            }

            if (!IsMathOps(input))
            {
               
                 return "Invalid Data";
                    
            }


            Stack<char> brackets = new();

            foreach (var c in input)
            {
                if (c == '[' || c == '{' || c == '(')
                    brackets.Push(c);
                else if (c == ']' || c == '}' || c == ')')
                {
                    // Too many closing brackets, e.g. (123))
                    if (brackets.Count <= 0)
                        return "Format Error";

                    char open = brackets.Pop();

                    // Inconsistent brackets, e.g. (123]
                    if (c == '}' && open != '{' ||
                        c == ')' && open != '(' ||
                        c == ']' && open != '[')
                        return "Format Error";
                }
            }

            // Too many opening brackets, e.g. ((123) 
            if (brackets.Count > 0)
                return "Format Error" ;

            return "false";
        }
        static bool IsMathOps(string input)
        {
            foreach (var c in input)
            {
               if( c == '+' || c == '-' || c == '*' || c == '/')
                    return true;
            }
            return false;
        }

    }
}
