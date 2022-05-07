using System.Collections;

namespace PudelkoLib
{
    public sealed class Pudelko : IFormattable, IEquatable<Pudelko>, IEnumerable<double>
    {
        public enum UnitOfMeasure
        {
            milimeter = 3,
            centimeter = 2,
            meter = 0
        }
        private readonly double a;
        private readonly double b;
        private readonly double c;
        private readonly UnitOfMeasure unitOfMeasure;
        public double A
        {
            get => Math.Truncate(a * 1000) / 1000;
            init
            {
                if (value <= 0.001 || value > 10)
                    throw new ArgumentOutOfRangeException();
                else
                    a = value;
            }
        }
        public double B
        {
            get => Math.Truncate(b * 1000) / 1000;
            init
            {
                if (value <= 0.001 || value > 10)
                    throw new ArgumentOutOfRangeException();
                else
                    b = value;
            }
        }
        public double C
        {
            get => Math.Truncate(c * 1000) / 1000;
            init
            {
                if (value <= 0.001 || value > 10)
                    throw new ArgumentOutOfRangeException();
                else
                    c = value;
            }
        }
        public UnitOfMeasure UnitOfMeasurement { get { return unitOfMeasure; } }
        public double Objetosc { get => Math.Round(a * b * c, 9); }
        public double Pole { get => Math.Round(2 * a * b + 2 * a * c + 2 * b * c, 6); }
        //Parametry przyjmują wartość NaN aby móc zachować dowolność ilości argumentów, a jednocześnie konstruktor wie,
        //których paramterów brakuje poprzez uzupełnianie unikalną wartością
        public Pudelko(double a = double.NaN, double b = double.NaN, double c = double.NaN, UnitOfMeasure unit = UnitOfMeasure.meter)
        {
            double def;
            switch (unit)
            {
                default: def = 0.1; break;
                case UnitOfMeasure.centimeter: def = 10; break;
                case UnitOfMeasure.milimeter: def = 100; break;
            }
            if (Double.IsNaN(a))
                a = def;
            if (Double.IsNaN(b))
                b = def;
            if (Double.IsNaN(c))
                c = def;
            if (unit == UnitOfMeasure.centimeter)
            { a /= 100; b /= 100; c /= 100; }
            if (unit == UnitOfMeasure.milimeter)
            { a /= 1000; b /= 1000; c /= 1000; }
            this.A = a; this.B = b; this.C = c; this.unitOfMeasure = unit;
        }
        public override string ToString()
        {
            return $"{A:0.000} {"m"} × {B:0.000} {"m"} × {C:0.000} {"m"}";
        }
        public string ToString(string format)
        {
            return this.ToString(format, null);
        }
        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            if (String.IsNullOrEmpty(format))
                return this.ToString();
            switch (format.ToLower())
            {
                case "m":
                    return this.ToString();
                case "cm":
                    return $"{A * 100:0.0} {"cm"} × {B * 100:0.0} {"cm"} × {C * 100:0.0} {"cm"}";
                case "mm":
                    return $"{A * 1000} {"mm"} × {B * 1000} {"mm"} × {C * 1000} {"mm"}";
                default:
                    throw new FormatException();
            }

        }

        public bool Equals(Pudelko? other)
        {
            if (other is null)
                throw new ArgumentNullException();
            double[] dimensions = { A, B, C };
            double[] dimensionsOther = { other.A, other.B, other.C };
            bool[] check = { false, false, false }; 
            Console.WriteLine($"{dimensions[1]}  {dimensionsOther[1]}");
            for (int i = 0; i != 3; i++)
            {
                for (int j = 0; j != 3; j++)
                {
                    
                    if (dimensions[i] == dimensionsOther[j])
                    {
                        check[i] = true;
                        dimensionsOther[j] = 9999999;
                        break;
                    }
                }
            }
            if (check.All(value => value == true))
                return true;
            else
                return false;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(A, B, C, unitOfMeasure);
        }

        public static bool operator ==(Pudelko a, Pudelko b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(Pudelko a, Pudelko b)
        {
            return !(a.Equals(b));
        }
        public static Pudelko operator +(Pudelko a, Pudelko b)
        {
            if (a.unitOfMeasure != b.unitOfMeasure)
                throw new ArgumentException();
            double[] dimensionsA = { a[0], a[1], a[2] };
            double[] dimensionsB = { b[0], b[1], b[2] };
            return new Pudelko(dimensionsA[0] + dimensionsB[0], Math.Max(dimensionsA[1], dimensionsB[1]), Math.Max(dimensionsA[2], dimensionsB[2]));
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Pudelko)
                return false;
            return this.Equals(obj as Pudelko);
        }
        public IEnumerator<double> GetEnumerator()
        {
            double[] tmp = { this[0], this[1], this[2] };
            foreach (double value in tmp)
                yield return value;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static explicit operator double[](Pudelko pudelko)
        {
            return new double[] { pudelko[0], pudelko[1], pudelko[2] };
        }
        public static implicit operator Pudelko(ValueTuple<int, int, int> dane)
        {
            return new Pudelko(dane.Item1, dane.Item2, dane.Item3, UnitOfMeasure.milimeter);
        }
        public double this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return A;
                    case 1:
                        return B;
                    case 2:
                        return C;
                    default:
                        return A;
                }
            }
        }
        public static Pudelko Parse(string data)
        {
            UnitOfMeasure unit;
            string[] dane = data.Split(" ");
            if (!(dane[1] == dane[4] && dane[4] == dane[7]))
            {
                throw new ArgumentException();
            }
            switch (dane[1].ToLower())
            {
                case "m": unit = UnitOfMeasure.meter; break;
                case "cm": unit = UnitOfMeasure.centimeter; break;
                case "mm": unit = UnitOfMeasure.milimeter; break;
                default: throw new ArgumentException();
            }
            return new Pudelko(Double.Parse(dane[0]), Double.Parse(dane[3]), Double.Parse(dane[6]), unit);
        }
    }
}
