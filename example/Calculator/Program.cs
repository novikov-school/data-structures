using Stack.List;
using System;

namespace Calculator
{
    class Program
    {
        // calc.exe 5 6 7 * + 1 -
        static void Main(string[] args)
        {
            // The stack of integers not yet operated on
            Stack<int> values = new Stack<int>();

            foreach (string token in args)
            {
                // if the value is an integer...
                int value;
                if (int.TryParse(token, out value))
                {
                    // ... push it to the stack
                    values.Push(value);
                }
                else
                {
                    // otherwise evaluate the expresion...
                    int rhs = values.Pop();
                    int lhs = values.Pop();

                    // ... and pop the result back to the stack
                    switch (token)
                    {
                        case "+":
                            values.Push(lhs + rhs);
                            break;
                        case "-":
                            values.Push(lhs - rhs);
                            break;
                        case "*":
                            values.Push(lhs*rhs);
                            break;
                        case "/":
                            values.Push(lhs/rhs);
                            break;
                        case "%":
                            values.Push(lhs%rhs);
                            break;
                        default:
                            throw new ArgumentException($"Unrecognized token: {token}");
                    }
                }
            }

            // the last item on the stack is the result
            Console.WriteLine(values.Pop());
        }
    }
}