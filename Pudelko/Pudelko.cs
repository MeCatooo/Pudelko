﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PudelkoLib
{
    public class Pudelko : IFormattable, IEquatable<Pudelko>, IEnumerable
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
            get => Math.Round(a, 3);
            init
            {
                if (value <= 0 || value > 10000)
                    throw new ArgumentOutOfRangeException();
                else
                    a = value;
            }
        }
        public double B
        {
            get => Math.Round(b, 3);
            init
            {
                if (value <= 0 || value > 10000)
                    throw new ArgumentOutOfRangeException();
                else
                    b = value;
            }
        }
        public double C
        {
            get => Math.Round(c, 3);
            init
            {
                if (value <= 0 || value > 10000)
                    throw new ArgumentOutOfRangeException();
                else
                    c = value;
            }
        }
        public string UnitOfMeasurement { get { return unitOfMeasure.ToString(); } }
        public double Objetosc { get => Math.Round(a * b * c, 9); }
        public double Pole { get => Math.Round(2 * a * b + 2 * a * c + 2 * b * c, 6); }
        public Pudelko(double a = 0.1, double b = 0.1, double c = 0.1, UnitOfMeasure unit = UnitOfMeasure.meter)
        {
            if ((unit == UnitOfMeasure.milimeter) && (a > 10000 || b > 10000 || c > 10000))
                throw new ArgumentOutOfRangeException();
            if ((unit == UnitOfMeasure.centimeter) && (a > 1000 || b > 1000 || c > 1000))
                throw new ArgumentOutOfRangeException();
            if ((unit == UnitOfMeasure.meter) && (a > 10 || b > 10 || c > 10))
                throw new ArgumentOutOfRangeException();
            this.A = a; this.B = b; this.C = c; this.unitOfMeasure = unit;
            //double[] dimensions = this.FormatUnits(unit);
            //this.A = dimensions[0]; this.B = dimensions[1]; this.C = dimensions[2];
        }
        public override string ToString()
        {
            switch (unitOfMeasure)
            {
                case UnitOfMeasure.meter:
                    return $"{Math.Round(A, 3):0.000} {"m"} × {Math.Round(B, 3):0.000} {"m"} × {Math.Round(C, 3):0.000} {"m"}";
                case UnitOfMeasure.centimeter:
                    return $"{Math.Round(A, 3):0.0} {"cm"} × {Math.Round(B, 3):0.0} {"cm"} × {Math.Round(C, 3):0.0} {"cm"}";
                default:
                    return $"{Math.Round(A, 3)} {"mm"} × {Math.Round(B, 3)} {"mm"} × {Math.Round(C, 3)} {"mm"}";
            }
        }
        private double[] FormatUnits(UnitOfMeasure format)
        {
            double[] tmp = { a, b, c };
            if ((int)this.unitOfMeasure < (int)format)
            {
                for (int i = 0; i != (int)format - (int)this.unitOfMeasure; i++)
                {
                    tmp = new double[] { tmp[0] * 10, tmp[1] * 10, tmp[2] * 10 };
                }
            }
            else if ((int)this.unitOfMeasure > (int)format)
            {
                for (int i = 0; i != (int)this.unitOfMeasure - (int)format; i++)
                {
                    tmp = new double[] { tmp[0] / 10, tmp[1] / 10, tmp[2] / 10 };
                }
            }
            return tmp;
        }

        public string ToString(string? format, IFormatProvider? formatProvider = null)
        {
            if (String.IsNullOrEmpty(format))
                return this.ToString();
            double[] dimensions;
            switch (format.ToLower())
            {
                case "m":
                    dimensions = this.FormatUnits(UnitOfMeasure.meter);
                    return new Pudelko(dimensions[0], dimensions[1], dimensions[2], UnitOfMeasure.meter).ToString();
                case "cm":
                    dimensions = this.FormatUnits(UnitOfMeasure.centimeter);
                    return new Pudelko(dimensions[0], dimensions[1], dimensions[2], UnitOfMeasure.centimeter).ToString();
                case "mm":
                    dimensions = this.FormatUnits(UnitOfMeasure.milimeter);
                    return new Pudelko(dimensions[0], dimensions[1], dimensions[2], UnitOfMeasure.milimeter).ToString();
                default:
                    return this.ToString();
            }
        }

        public bool Equals(Pudelko? other)
        {
            if (other is null)
                throw new ArgumentNullException();
            if (unitOfMeasure != other.unitOfMeasure)
                return false;
            double[] dimensions = { A, B, C };
            double[] dimensionsOther = { other.A, other.B, other.C };
            bool check = false;
            for (int i = 0; i != 3; i++)
            {
                for (int j = 0; j != 3; j++)
                {
                    if (dimensions[i] == dimensionsOther[j])
                        check = true;
                }
                if (check == false)
                    return false;
            }
            return true;
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
            return !a.Equals(b);
        }
        public static Pudelko operator +(Pudelko a, Pudelko b)
        {
            double[] dimensionsA = a.FormatUnits(UnitOfMeasure.centimeter);
            double[] dimensionsB = b.FormatUnits(UnitOfMeasure.centimeter);
            return new Pudelko(dimensionsA[0] + dimensionsB[0], Math.Max(dimensionsA[1], dimensionsA[1]), Math.Max(dimensionsA[2], dimensionsA[2]));
        }

        public override bool Equals(object? obj)
        {
            if (!(obj is Pudelko))
                return false;
            return this.Equals(obj as Pudelko);
        }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public static explicit operator double[](Pudelko pudelko)
        {
            return pudelko.FormatUnits(pudelko.unitOfMeasure);
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
    }
}