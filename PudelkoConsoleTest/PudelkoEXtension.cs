using PudelkoLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PudelkoConsoleTest
{
    internal static class PudelkoEXtension
    {
        public static Pudelko Kompresuj(this Pudelko pudelko)
        {
            return new Pudelko(Math.Pow(pudelko.Objetosc, 1 / 3), unit: pudelko.UnitOfMeasurement);
        }
    }
}
