using System;
using static VectorTask.Vector;

namespace VectorTask
{
    public class Program
    {
        public static void Main()
        {
            var vector1 = new Vector(new[] { 3.0, 2, 5 });
            var vector2 = new Vector(6, new double[] { 10, 9, 0, 4 });

            Console.WriteLine("Созданы следующие вектора:");
            Console.WriteLine(vector1);
            Console.WriteLine(vector2);

            vector1.Add(vector2);
            Console.WriteLine("Результат прибавления к вектору второго вектора:");
            Console.WriteLine(vector1);

            vector1.MultiplyByScalar(5);
            Console.WriteLine("Результат умножения вектора на скаляр:");
            Console.WriteLine(vector1);

            vector1.Subtract(vector2);
            Console.WriteLine("Результат вычитания из вектора второго вектора:");
            Console.WriteLine(vector1);

            Console.WriteLine($"Длина вектора равна length = {vector1.GetLength():f1}");

            vector1.SetElement(5, 2);
            Console.WriteLine("Результат установки компонента вектора по индексу 5:");
            Console.WriteLine(vector1);

            var innerProduct = GetScalarProduct(vector1, vector2);
            Console.WriteLine("Результат скалярного произведения векторов:");
            Console.WriteLine(innerProduct);
        }
    }
}