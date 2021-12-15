using System;

namespace Shapes
{
    public class Square
    {
        private double _sideLength;

        public double SideLength
        {
            get => _sideLength;

            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException($"Длина стороны квадрата должна быть положительным числом, переданное значение: {value}", nameof(SideLength));
                }

                _sideLength = value;
            }
        }

        public Square(double sideLength)
        {
            SideLength = sideLength;
        }

        public override string ToString()
        {
            return $"Квадрат со стороной {_sideLength:f1}";
        }
    }
}