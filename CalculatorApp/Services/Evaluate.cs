namespace CalculatorApp.Services
{
    public static class Evaluate
    {
        public static double Calculate(string input)
        {
            List<string> tokens = Tokenize(input);
            Stack<double> values = new();
            Stack<char> ops = new();

            foreach (string token in tokens)
            {
                if (double.TryParse(token, out double num))
                {
                    values.Push(num);
                }
                else if (token == "(")
                {
                    ops.Push('(');
                }
                else if (token == ")")
                {
                    while (ops.Peek() != '(')
                    {
                        values.Push(Compute(values.Pop(), values.Pop(), ops.Pop()));
                    }
                    ops.Pop(); // Remove '(' from ops stack
                }
                else if (IsMathOps(token[0]))
                {
                    while (ops.Count > 0 && Precedence(ops.Peek()) >= Precedence(token[0]))
                    {
                        values.Push(Compute(values.Pop(), values.Pop(), ops.Pop()));
                    }
                    ops.Push(token[0]);
                }
            }

            while (ops.Count > 0)
            {
                values.Push(Compute(values.Pop(), values.Pop(), ops.Pop()));
            }

            return values.Pop();
        }

        static List<string> Tokenize(string input)
        {
            List<string> tokens = [];
            int i = 0;
            while (i < input.Length)
            {
                if (char.IsWhiteSpace(input[i]))
                {
                    i++;
                    continue;
                }
                if (char.IsDigit(input[i]) || input[i] == '.')
                {
                    int start = i;
                    while (i < input.Length && (char.IsDigit(input[i]) || input[i] == '.'))
                    {
                        i++;
                    }
                    tokens.Add(input.Substring(start, i - start));
                }
                else if (IsMathOps(input[i]) || input[i] == '(' || input[i] == ')')
                {
                    tokens.Add(input[i].ToString());
                    i++;
                }
            }
            return tokens;
        }

         static bool IsMathOps(char c)
        {
            return c == '+' || c == '-' || c == '*' || c == '/';
        }

        static int Precedence(char op)
        {
            switch (op)
            {
                case '+':
                case '-':
                    return 1;
                case '*':
                case '/':
                    return 2;
                default:
                    return 0;
            }
        }

        static double Compute(double b, double a, char op)
        {
            switch (op)
            {
                case '+': return a + b;
                case '-': return a - b;
                case '*': return a * b;
                case '/':
                    if (b == 0)
                        throw new ArgumentException("Divide by 0 error");
                    return a / b;
                default: throw new ArgumentException("Invalid operator");
            }
        }
    }
}
