using System;
using System.Linq;
using System.Text;

// ReSharper disable NonReadonlyMemberInGetHashCode

namespace VectorTask
{
    public class Vector
    {
        private double[] _elements;

        public int Size => _elements.Length;

        public Vector(int elementsCount)
        {
            if (elementsCount <= 0)
            {
                throw new ArgumentException($"Размерность вектора не может быть <= 0, передано значение: {elementsCount}", nameof(elementsCount));
            }

            _elements = new double[elementsCount];
        }

        public Vector(Vector vector)
        {
            if (vector == null)
            {
                throw new ArgumentNullException(nameof(vector), "Передана пустая ссылка");
            }

            _elements = (double[])vector._elements.Clone();
        }

        public Vector(double[] array)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array), "Передана пустая ссылка");
            }

            if (array.Length == 0)
            {
                throw new ArgumentException("Передан массив нулевой длины", nameof(array));
            }

            _elements = (double[])array.Clone();
        }

        public Vector(int elementsCount, double[] array)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array), "Передана пустая ссылка");
            }

            if (elementsCount <= 0)
            {
                throw new ArgumentException($"Размерность вектора не может быть <= 0, передано значение: {elementsCount}", nameof(elementsCount));
            }

            _elements = new double[elementsCount];

            if (elementsCount < array.Length)
            {
                Array.Copy(array, _elements, elementsCount);
            }
            else
            {
                array.CopyTo(_elements, 0);
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append("{ ")
              .AppendJoin(", ", _elements)
              .Append(" }");

            return sb.ToString();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }

            var vector = (Vector)obj;

            return _elements.SequenceEqual(vector._elements);
        }

        public override int GetHashCode()
        {
            const int prime = 37;
            var hash = 1;

            foreach (var e in _elements)
            {
                hash = prime * hash + e.GetHashCode();
            }

            return hash;
        }

        public void Add(Vector vector)
        {
            if (vector == null)
            {
                throw new ArgumentNullException(nameof(vector), "Передана пустая ссылка");
            }

            if (_elements.Length < vector._elements.Length)
            {
                Array.Resize(ref _elements, vector._elements.Length);
            }

            for (var i = 0; i < vector._elements.Length; i++)
            {
                _elements[i] += vector._elements[i];
            }
        }

        public void Subtract(Vector vector)
        {
            if (vector == null)
            {
                throw new ArgumentNullException(nameof(vector), "Передана пустая ссылка");
            }

            if (_elements.Length < vector._elements.Length)
            {
                Array.Resize(ref _elements, vector._elements.Length);
            }

            for (var i = 0; i < vector._elements.Length; i++)
            {
                _elements[i] -= vector._elements[i];
            }
        }

        public void MultiplyByScalar(double scalar)
        {
            for (var i = 0; i < _elements.Length; i++)
            {
                _elements[i] *= scalar;
            }
        }

        public void Reverse()
        {
            MultiplyByScalar(-1);
        }

        public double GetLength()
        {
            var sum = 0.0;

            foreach (var e in _elements)
            {
                sum += e * e;
            }

            return Math.Sqrt(sum);
        }

        public double GetElement(int index)
        {
            CheckIndex(index);

            return _elements[index];
        }

        public void SetElement(int index, double value)
        {
            CheckIndex(index);

            _elements[index] = value;
        }

        private void CheckIndex(int index)
        {
            if (index < 0 || index >= _elements.Length)
            {
                throw new IndexOutOfRangeException($"Допустимый диапазон индекса 0 <= index < {_elements.Length}" +
                                                   $" передано значение index: {index}");
            }
        }

        public static Vector GetSum(Vector vector1, Vector vector2)
        {
            if (vector1 == null)
            {
                throw new ArgumentNullException(nameof(vector1), "Передана пустая ссылка");
            }

            if (vector2 == null)
            {
                throw new ArgumentNullException(nameof(vector2), "Передана пустая ссылка");
            }

            var result = new Vector(vector1);
            result.Add(vector2);

            return result;
        }

        public static Vector GetDifference(Vector vector1, Vector vector2)
        {
            if (vector1 == null)
            {
                throw new ArgumentNullException(nameof(vector1), "Передана пустая ссылка");
            }

            if (vector2 == null)
            {
                throw new ArgumentNullException(nameof(vector2), "Передана пустая ссылка");
            }

            var result = new Vector(vector1);
            result.Subtract(vector2);

            return result;
        }

        public static double GetScalarProduct(Vector vector1, Vector vector2)
        {
            if (vector1 == null)
            {
                throw new ArgumentNullException(nameof(vector1), "Передана пустая ссылка");
            }

            if (vector2 == null)
            {
                throw new ArgumentNullException(nameof(vector2), "Передана пустая ссылка");
            }

            var minElementsCount = Math.Min(vector1._elements.Length, vector2._elements.Length);
            var result = 0.0;

            for (var i = 0; i < minElementsCount; i++)
            {
                result += vector1._elements[i] * vector2._elements[i];
            }

            return result;
        }
    }
}