// See https://aka.ms/new-console-template for more information
using PudelkoLib;
using static PudelkoLib.Pudelko;

Pudelko pudelko = new Pudelko(1, unit: Pudelko.UnitOfMeasure.meter);
//pudelko.A = 10;
Pudelko pudelko1 = new Pudelko(10, 10, 10, Pudelko.UnitOfMeasure.milimeter);
//Console.WriteLine(pudelko.ToString());
//Console.WriteLine(pudelko1.ToString("m"));
//Console.WriteLine(pudelko.A);
//Console.WriteLine(pudelko.B);
//Console.WriteLine(pudelko.C);
//(1, 1, 1, UnitOfMeasure.meter, 1, 2, 1)
Pudelko test = new Pudelko(1, 1, 1, UnitOfMeasure.meter);
Pudelko test1 = new Pudelko(1, 1, 1, UnitOfMeasure.meter);

Console.WriteLine(test.Equals(test1));
List<Pudelko> list = new List<Pudelko>();
list.Add(pudelko1);
list.Add(pudelko);
list.Sort(PudelkoSort);
foreach(Pudelko item in list)
{
    Console.WriteLine(item.ToString());
}
//double[] vs = (double[])pudelko;
//foreach (var item in vs)
//{
//    Console.WriteLine(item);
//}
//Console.WriteLine(pudelko + pudelko1);
static int PudelkoSort(Pudelko a, Pudelko b)
{
    //if (other is null || other is not Pudelko)
    //    throw new ArgumentException();
    if (a.Objetosc > b.Objetosc) { return -1; }
    else if (a.Objetosc < b.Objetosc) { return 1; }
    else if (a.Pole > b.Pole) { return -1; }
    else if (a.Pole < b.Pole) { return 1; }
    else if (a.A + a.B + a.C > b.A + b.B + b.C) { return -1; }
    else if (a.A + a.B + a.C < b.A + b.B + b.C) { return 1; }
    else { return 0; }
}