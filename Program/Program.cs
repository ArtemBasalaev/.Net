using System;
using Shapes;

namespace Program
{
    public class Program
    {
        public static void Main()
        {
            var square = new Square(5);
            Console.WriteLine(square);

            var circle = new Circle(10);
            Console.WriteLine(circle);

            Console.ReadKey();
        }
    }
}