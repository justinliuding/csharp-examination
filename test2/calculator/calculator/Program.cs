using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;

namespace calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0 || !File.Exists(args[0]))
            {
                Console.WriteLine("文件参数错误，请重新运行！");
                Console.ReadKey();
                return;
            }
            var data = ReadData(args[0]);
            //不处理任何错误参数的情况
            var result = Calculate(data);
            Console.WriteLine($"计算结果为:{result}");
            Console.ReadKey();
        }
        //递归运算
        static double Calculate(string s)
        {
            if (double.TryParse(s, out double num))
            {
                return num;
            }
            //处理括号
            var start = -1;
            var end = -1;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '(')
                {
                    start = i;
                }
                if (s[i] == ')')
                {
                    end = i;
                    break;
                }
            }
            if (start >= 0)
            {
                var str = s.Substring(start + 1, end - start - 1);
                var r = Calculate(str).ToString();
                return Calculate(s.Replace("(" + str + ")", r));
            }
            //处理乘除
            for (var i = 0; i < s.Length; i++)
            {
                if (s[i] == '*' || s[i] == '/')
                {
                    return Calculate(s, i);
                }
            }
            //处理加减
            for (var i = 0; i < s.Length; i++)
            {
                if (s[i] == '+' || s[i] == '-')
                {
                    return Calculate(s, i);
                }
            }
            throw new Exception("参数错误");


        }

        static double Calculate(string s, int i)
        {
            var j = i - 1;
            //向前拿第一个数
            for (; j >= 0; j--)
            {
                if ((s[j] < '0' || s[j] > '9') && s[j] != '.')
                {
                    break;
                }
            }
            j++;
            var k = i + 1;
            //向后拿第二个数
            for (; k < s.Length; k++)
            {
                if ((s[k] < '0' || s[k] > '9') && s[k] != '.')
                {
                    break;
                }
            }
            k--;
            var num1 = double.Parse(s.Substring(j, i - j));
            var num2 = double.Parse(s.Substring(i + 1, k - i));
            double replace;
            if (s[i] == '*')
            {
                replace = num1 * num2;
            }
            else if (s[i] == '/')
            {
                replace = num1 / num2;
            }
            else if (s[i] == '+')
            {
                replace = num1 + num2;
            }
            else
            {
                replace = num1 - num2;
            }
            s = s.Substring(0, j) + replace + s.Substring(k + 1);
            return Calculate(s);
        }

        //读取参数并合并字符串
        static string ReadData(string path)
        {
            Dictionary<string, string> number = new Dictionary<string, string>();
            var str = string.Empty;
            using (StreamReader sr = new StreamReader(path))
            {
                str = sr.ReadLine();
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    var d = s.Split('=');//不考虑数据不合法情况
                    number[d[0]] = d[1];
                }
            }
            foreach (var item in number)
            {
                str = str.Replace(item.Key, item.Value);
            }
            return str;
        }
    }
}
