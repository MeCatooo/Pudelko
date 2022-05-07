// See https://aka.ms/new-console-template for more information
using PudelkoConsoleTest;
using PudelkoLib;

Pudelko pudelko = new Pudelko(1, unit: Pudelko.UnitOfMeasure.meter);
Pudelko pudelko1 = new Pudelko(120, 120, 210, Pudelko.UnitOfMeasure.milimeter);
Pudelko pudelko2 = new Pudelko(10, 10, 10, Pudelko.UnitOfMeasure.milimeter);
Pudelko pudelko3 = new();
Console.WriteLine(pudelko3.ToString());
Console.WriteLine(pudelko3.ToString("m"));
Console.WriteLine(pudelko3.ToString("cm"));
Console.WriteLine(pudelko3.ToString("mm"));
Console.WriteLine("======================");
Console.WriteLine("Objetosc: " + pudelko3.Objetosc);
Console.WriteLine("Pole: " + pudelko3.Pole);
Console.WriteLine("======================");
Console.WriteLine(Pudelko.Parse(pudelko3.ToString()).ToString());
Console.WriteLine("======================");
Pudelko test = new Pudelko(1,2,3);
Console.WriteLine(test.ToString());
Console.WriteLine("Skompresowane: " + PudelkoEXtension.Kompresuj(test).ToString())
List <Pudelko> list = new List<Pudelko>();
list.Add(pudelko1);
list.Add(pudelko);
list.Add(pudelko2);
foreach (Pudelko item in list)
{
    Console.WriteLine(item.ToString());
}
list.Sort(PudelkoSort);
Console.WriteLine("======================");
foreach(Pudelko item in list)
{
    Console.WriteLine(item.ToString());
}
static int PudelkoSort(Pudelko a, Pudelko b)
{
    if (a.Objetosc > b.Objetosc) { return -1; }
    else if (a.Objetosc < b.Objetosc) { return 1; }
    else if (a.Pole > b.Pole) { return -1; }
    else if (a.Pole < b.Pole) { return 1; }
    else if (a.A + a.B + a.C > b.A + b.B + b.C) { return -1; }
    else if (a.A + a.B + a.C < b.A + b.B + b.C) { return 1; }
    else { return 0; }
}