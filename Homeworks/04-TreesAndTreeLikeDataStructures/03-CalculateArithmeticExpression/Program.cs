namespace _03_CalculateArithmeticExpression
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    public class Program
    {
        public static void Main(string[] args)
        {
            string input = Console.ReadLine();
            string[] parsedInput = ParseInput(input);
            string[] result = ConvertToPostfixNotation(parsedInput);
            double finalResult = EvaluatePostix(result);
            Console.WriteLine(finalResult);
        }

        private static double EvaluatePostix(string[] expressionArgs)
        {
            Stack<double> result = new Stack<double>();

            foreach (string token in expressionArgs)
            {
                double n;
                bool isNumeric = double.TryParse(token, out n);
                if (isNumeric)
                {
                    result.Push(n);
                }
                else
                {
                    var secondOperand = result.Pop();
                    var firstOperand = result.Pop();
                    
                    var arithmeticOperator = token;

                    switch (arithmeticOperator)
                    {
                        case "+":
                            result.Push(firstOperand + secondOperand);
                            break;
                        case "-":
                            result.Push(firstOperand - secondOperand);
                            break;
                        case "*":
                            result.Push(firstOperand * secondOperand);
                            break;
                        case "/":
                            result.Push(firstOperand / secondOperand);
                            break;
                    }
                }
            }

            return result.Pop();
        }

        private static string[] ParseInput(string input)
        {
            MatchCollection matches = Regex.Matches(input, @"(-?\d+\.\d+|-?\d+|\*|\-|\–|\+|\/|\(|\))");
            string[] result = new string[matches.Count];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = matches[i].Groups[1].Value;
            }

            return result;
        }

        private static Dictionary<string, int> operators = new Dictionary<string, int>()
        {
            {"+", 1},
            {"-", 1},
            {"*", 2},
            {"/", 2},
            {"(", 3},
            {")", 3}
        };

        private static string[] ConvertToPostfixNotation(string[] tokens)
        {
            Stack<string> stack = new Stack<string>();
            Queue<string> queue = new Queue<string>();

            foreach (string token in tokens)
            {
                double n;
                bool isNumeric = double.TryParse(token, out n);
                if (isNumeric)
                {
                    queue.Enqueue(token);
                }
                else if (operators.ContainsKey(token) || token == ")" || token == "(")
                {
                    if (stack.Count == 0)
                    {
                        stack.Push(token);
                    }
                    else if (token == ")")
                    {
                        while (stack.Count > 0)
                        {
                            var currentTop = stack.Pop();
                            if (currentTop == "(")
                            {
                                break;
                            }

                            queue.Enqueue(currentTop);
                        }
                    }
                    else
                    {
                        var topElement = stack.Peek();
                        //Has the top element higher precendence than the current token
                        while (stack.Count > 0 && HasHigherPrecedence(topElement, token))
                        {
                            if (topElement == "(")
                            {
                                break;
                            }

                            topElement = stack.Pop();
                            if (topElement != "(")
                            {
                                queue.Enqueue(topElement);
                            }
                        }

                        stack.Push(token);
                    }
                }
                else
                {
                    Console.WriteLine("{0} is invalid token!", token);
                }
            }

            while (stack.Count > 0)
            {
                var topElement = stack.Pop();
                queue.Enqueue(topElement);
            }

            return queue.ToArray();
        }

        //Has the top element higher precendence than the current token
        private static bool HasHigherPrecedence(string top, string token)
        {
            if (operators[top] > operators[token])
            {
                return true;
            }

            return false;
        }
    }
}
