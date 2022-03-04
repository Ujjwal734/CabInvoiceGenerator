// See https://aka.ms/new-console-template for more information
using CabInvoiceGenerator;
Console.WriteLine("Hello, Welcome To Cab Invoice");
InvoiceGenerator invoiceGenerator = new InvoiceGenerator(RideTypes.NORMAL);
InvoiceGenerator invoiceGenerator1 = new InvoiceGenerator(RideTypes.PREMIUM);
double fare = invoiceGenerator.CalculateFare(2.0, 5);
double fare1 = invoiceGenerator1.CalculateFare(2.0, 5);
Console.WriteLine($"Total Fare : {fare}");
Console.WriteLine($"Total Fare : {fare1}");