using System;
using NLog;
using Shapes;

namespace ShapesCreator
{
    public class Program
    {
        public static void Main()
        {
            var logger = LogManager.GetCurrentClassLogger();

            logger.Info("Programm start");

            try
            {
                var sideLength = 5;

                logger.Debug($"В конструктор Square() передано значение: {sideLength}");

                var square = new Square(sideLength);
                Console.WriteLine(square);

                var circle = new Circle(-10);
                Console.WriteLine(circle);
            }
            catch(ArgumentException e) 
            {
                logger.Error(e, "Ошибка ввода");
            }

            Console.ReadKey();
        }
    }
}