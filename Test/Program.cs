using Monitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("程序开始");
            var operate = new Operate();
            operate.Init();
            Console.ReadLine();
        }
    }
}
