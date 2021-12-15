using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Calculator
{
    public static class CalculatorDomain
    {

        public static double Calculate(String s)
        {
            Stack<double> stack = new Stack<double>();
            var sArray = s.ToArray();
            int Decimals = 0;
            char preSign = '+';
            double num = 0;
            int bracketsCount = 0;
            int bracketsStartIndex = 0;
            int n = sArray.Length;
            for (int i = 0; i < n; ++i)
            {
                switch (sArray[i])
                {
                    case '(':
                        if (bracketsCount == 0)
                        {
                            bracketsStartIndex = i;
                        }
                        bracketsCount++;
                        break;
                    case ')':
                        bracketsCount--;
                        if (bracketsCount == 0)
                        {
                            num = Calculate(s.Substring(bracketsStartIndex + 1, i - bracketsStartIndex - 1));
                        }
                        break;
                }

                if (bracketsCount  == 0)
                {
                    if (Char.IsDigit(sArray[i]))
                    {
                        if (Decimals > 0)
                        {
                            num = num  + ((double)(sArray[i] - '0') / Math.Pow(10, Decimals));
                            Decimals++;
                        }
                        else
                        {
                            num = num * 10 + sArray[i] - '0';
                        }
                        
                    }
                    if (sArray[i] == '.')
                    {
                        Decimals = 1;
                    }

                    if (!Char.IsDigit(sArray[i]) && sArray[i] != '.' && sArray[i] != ' ' || i == n - 1 )
                    {
                        switch (preSign)
                        {
                            case '+':
                                stack.Push(num);
                                break;
                            case '-':
                                stack.Push(-num);
                                break;
                            case '*':
                                stack.Push(stack.Pop() * num);
                                break;
                            case '/':
                                stack.Push(stack.Pop() / num);
                                break;
                        }
                        preSign = sArray[i];
                        Decimals = 0;
                        num = 0;
                    }
                }
               
            }
            double ans = 0;
            while (stack.Any())
            {
                ans += stack.Pop();
            }
            return ans;
        }


    }


    

}
