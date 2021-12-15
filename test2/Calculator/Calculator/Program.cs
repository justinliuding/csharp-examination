using System;
using System.IO;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {

            var filePath = args[2];
            var s = "";
            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string txt = sr.ReadToEnd();
                    Console.WriteLine(txt);
                    Console.WriteLine("-------------");
                    var sArry = txt.Split("\r\n");
                    s = sArry[0];
                    for (int i = 1; i < sArry.Length; i++)
                    {
                        var p = sArry[i].Split("=");
                        s = s.Replace(p[0], p[1]);
                    }

                }
            }
            catch (Exception e)
            {
                // 向用户显示出错消息
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            Console.WriteLine(s);
            Console.WriteLine("-------------");
            var r = CalculatorDomain.Calculate(s);
            Console.WriteLine("result：" + r);
            Console.WriteLine("Hello World!");
        }
    }
}
