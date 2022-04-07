using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PudelkoLib
{
    public class Pudelko : IFormattable
    {
        public enum UnitOfMeasure
        {
            mm,
            cm,
            m
        }

        private readonly decimal a;
        private readonly decimal b;
        private readonly decimal c;
        private readonly UnitOfMeasure unitOfMeasure;
        public decimal A
        {
            get => decimal.Round(a, 3); init
            {
                if (value <= 0 || value > 10)
                    throw new ArgumentOutOfRangeException();
                else
                    value = a;
            }
        }
        public decimal B
        {
            get => decimal.Round(b, 3); init
            {
                if (value <= 0 || value > 10)
                    throw new ArgumentOutOfRangeException();
                else
                    value = b;
            }
        }
        public decimal C
        {
            get => decimal.Round(c, 3); init
            {
                if (value <= 0 || value > 10)
                    throw new ArgumentOutOfRangeException();
                else
                    value = c;
            }
        }
        public Pudelko(decimal a = 10, decimal b = 10, decimal c = 10, UnitOfMeasure unitOfMeasure = UnitOfMeasure.cm)
        {
            this.a = a; this.b = b; this.c = c; this.unitOfMeasure = unitOfMeasure;
        }
        public override string ToString()
        {
            return $"{A} {unitOfMeasure} \times {B} {unitOfMeasure} \0x00D7 {C} {unitOfMeasure}";
        }

        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            throw new NotImplementedException();
        }
    }
}
