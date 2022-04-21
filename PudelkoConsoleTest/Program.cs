// See https://aka.ms/new-console-template for more information
using PudelkoLib;
Pudelko pudelko = new Pudelko(1,unit:Pudelko.UnitOfMeasure.centimeter);
Pudelko pudelko1 = new Pudelko(10,10,10, Pudelko.UnitOfMeasure.milimeter);
Console.WriteLine(pudelko.ToString("mm"));
Console.WriteLine(pudelko1.ToString("m"));
Console.WriteLine(pudelko.A);
Console.WriteLine(pudelko.B);
Console.WriteLine(pudelko.C);
//Console.WriteLine(pudelko + pudelko1);