using System;

namespace Shapes
{
    public class Circle
    {
        private double _radius;

        public double Radius
        {
            get => _radius;

            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException($"Значение радиуса должно быть положительным числом, переданное значение: {value:f1}", nameof(Radius));
                }

                _radius = value;
            }
        }

        public Circle(double radius)
        {
            Radius = radius;
        }

        public override string ToString()
        {
            return $"Окружность с радиусом {_radius}";
        }
    }
}