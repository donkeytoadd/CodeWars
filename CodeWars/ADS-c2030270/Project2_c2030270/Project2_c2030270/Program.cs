namespace Project2_c2030270
{
    class Project2
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Enter the expression you want to convert: ");
            string expression = Console.ReadLine();

            // Replace operators in the expression and assign it back
            expression = expression.Replace("||", "|")
                                 .Replace("&&", "&")
                                 .Replace(">=", "@")
                                 .Replace("<=", "$")
                                 .Replace("!=", "!")
                                 .Replace("**", "^");

            string postfix = InfixToPostfix(expression);
            string evaluation = Evaluation(postfix);

            Console.WriteLine("Infix: " + expression);
            Console.WriteLine("Postfix: " + postfix);
            Console.WriteLine("Evaluation: " + evaluation);
        }

        public static string InfixToPostfix(string expression)
        {
            Stack<char> stack = new Stack<char>();
            string postfix = "";

            for (int i = 0; i < expression.Length; i++)
            {
                var x = expression[i];
                if (char.IsLetter(x))
                {
                    Console.WriteLine("Please enter a value for the variable: " + x);
                    int userInput = Convert.ToInt32(Console.ReadLine());
                    postfix += userInput;
                }
                else if (char.IsDigit(x))
                {
                    // Keep reading digits until a non-digit is encountered
                    string operand = x.ToString();
                    while ((i + 1 < expression.Length) && (char.IsDigit(expression[i + 1]) || expression[i + 1] == '.'))
                    {
                        operand += expression[++i];
                    }

                    postfix += operand;
                }
                else if (x == '(')
                {
                    stack.Push(x);
                }
                else if (x == ')')
                {
                    while (stack.Count > 0 && stack.Peek() != '(')
                    {
                        postfix += stack.Pop();
                    }

                    if (stack.Count > 0 && stack.Peek() != '(')
                    {
                        return "Invalid Expression";
                    }
                    else
                    {
                        stack.Pop();
                    }
                }
                else
                {
                    while ((stack.Count > 0) && (Priority((char)stack.Peek()) >= Priority(x)))
                    {
                        postfix += stack.Pop();
                    }
                    stack.Push(x);
                }
            }

            while (stack.Count > 0)
            {
                postfix += stack.Pop();
            }

            return postfix;
        }

        public static string Evaluation(string postfix)
        {
            Stack<string> operands = new Stack<string>();
            postfix += ")";
            string result = "";

            foreach (var j in postfix)
            {
                if (j == ')')
                {
                    result = operands.Pop();
                    operands.Push(result);
                }
                else if (char.IsDigit(j))
                {
                    operands.Push(j.ToString());
                }
                else if (IsOperator(j) || IsBooleanOperator(j) || IsUnaryOperator(j))
                {
                    var temp2 = operands.Pop();
                    var temp1 = (IsUnaryOperator(j) || IsBooleanOperator(j)) ? "" : operands.Pop();
                    var op = j;

                    var answer = EvaluateExpression(temp1, temp2, op);
                    if (answer != null)
                    {
                        operands.Push(answer);
                    }
                }
            }

            if (operands.Count > 0)
            {
                return operands.Pop();
            }

            return "";
        }

        private static string EvaluateExpression(string value1, string value2, char op)
        {
            if (IsUnaryOperator(op))
            {
                // Handle unary operators
                var operand = Convert.ToInt32(value2);
                if (op == '-')
                {
                    operand = -operand;
                }
                return Convert.ToString(operand);
            }

            if (IsBooleanOperator(op))
            {
                
                // Handle boolean operators
                var boolOperand1 = Convert.ToBoolean(value1);
                var boolOperand2 = Convert.ToBoolean(value2);

                if (op == '&')
                {
                    return (boolOperand1 && boolOperand2).ToString();
                }

                if (op == '|')
                {
                    return (boolOperand1 || boolOperand2).ToString();
                }

                if (op == '!')
                {
                    return (!boolOperand2).ToString();
                }
                
                
                /*
                if (IsBooleanOperator(op))
                {
                    // Handle boolean operators
                    var operand1 = Convert.ToString(value1);
                    var operand2 = Convert.ToString(value2);

                    if (op == '&')
                    {
                        return (operand1 != operand2).ToString();
                    }

                    if (op == '|')
                    {
                        return (operand1 != operand2).ToString();
                    }

                    if (op == '!')
                    {
                        return (operand1 != operand2).ToString();
                    }
                }*/
                

            }
            else
            {
                // Handle arithmetic operators
                var operand1 = Convert.ToInt32(value1);
                var operand2 = Convert.ToInt32(value2);

                switch (op)
                {
                    case '+':
                        return Convert.ToString(operand1 + operand2);

                    case '-':
                        return Convert.ToString(operand1 - operand2);

                    case '*':
                        return Convert.ToString(operand1 * operand2);

                    case '/':
                        return Convert.ToString(operand1 / operand2);

                    case '^':
                        return Convert.ToString(Math.Pow(operand1, operand2));

                    case '@':
                        return (operand1 >= operand2).ToString();

                    case '$':
                        return (operand1 <= operand2).ToString();

                    case '>':
                        return (operand1 > operand2).ToString();

                    case '<':
                        return (operand1 < operand2).ToString();


                    default:
                        // Handle the case when op is not one of the specified operators
                        throw new ArgumentException("Invalid operator: " + op);
                }


                /*
                if (op == '+')
                {
                    var result = operand1 + operand2;
                    return Convert.ToString(result);
                }

                if (op == '-')
                {
                    var result = operand1 - operand2;
                    return Convert.ToString(result);
                }

                if (op == '*')
                {
                    var result = operand1 * operand2;
                    return Convert.ToString(result);
                }

                if (op == '/')
                {
                    var result = operand1 / operand2;
                    return Convert.ToString(result);
                }

                if (op == '^')
                {
                    var result = Math.Pow(operand1, operand2);
                    return Convert.ToString(result);
                }

                if (op == '@')
                {
                    return (operand1 >= operand2).ToString();
                }

                if (op == '$')
                {
                    return (operand1 <= operand2).ToString();

                }
                */
            }

            return null;
        }


        public static int Priority(char op)
        {
            int priority = -1;

            if (op == '|')
            {
                priority = 1;
            }

            if (op == '&')
            {
                priority = 2;
            }

            if (op == '<' || op == '>' || op == '$' || op == '@')
            {
                priority = 3;
            }

            if (op == '+' || op == '-')
                priority = 4;

            if (op == '*' || op == '/')
            {
                priority = 5;
            }
            // power   unary plus  unary minus        not
            if (op == '^' || op == '#' || op == '~' || op == '§')
            {
                priority = 6;
            }

            return priority;
        }

        public static bool IsOperator(char op)
        {
            char[] operators = { '+', '-', '*', '/', '^', '#', '~', '§', '<', '>', '@', '$', '&', '|', '!' };
            bool contains = operators.Contains(op);

            return contains;
        }

        public static bool IsUnaryOperator(char op)
        {
            char[] unaryOperators = { '+', '-' };
            return unaryOperators.Contains(op);
        }

        public static bool IsBooleanOperator(char op)
        {
            char[] booleanOperators = { '!', '&', '|' };
            return booleanOperators.Contains(op);
        }
    }
}
