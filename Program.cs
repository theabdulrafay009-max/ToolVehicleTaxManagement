//C# task

using System;
using System.Collections.Generic;

abstract class ToolVehicle
{
    public int VehicleID { get; set; }
    public string RegNo { get; set; }
    public string Model { get; set; }
    public string Brand { get; set; }
    public decimal BasePrice { get; set; }
    public string VehicleType { get; set; }

    public static int TotalVehicles { get; private set; }
    public static int TotalTaxPayingVehicles { get; private set; }
    public static int TotalNonTaxPayingVehicles { get; private set; }
    public static decimal TotalTaxCollected { get; private set; }

    private bool IsProcessed = false;

    public ToolVehicle(int id, string regNo, string model,
        string brand, decimal price, string type)
    {
        VehicleID = id;
        RegNo = regNo;
        Model = model;
        Brand = brand;
        BasePrice = price;
        VehicleType = type;

        TotalVehicles++;
    }

    public abstract void PayTax();

    protected void CollectTax(decimal tax)
    {
        if (IsProcessed)
        {
            Console.WriteLine("This vehicle is already processed.");
            return;
        }

        TotalTaxCollected += tax;
        TotalTaxPayingVehicles++;
        IsProcessed = true;

        Console.WriteLine("Tax paid successfully: $" + tax);
    }

    public void PassWithoutPaying()
    {
        if (IsProcessed)
        {
            Console.WriteLine("This vehicle is already processed.");
            return;
        }

        TotalNonTaxPayingVehicles++;
        IsProcessed = true;

        Console.WriteLine("Vehicle passed without paying tax.");
    }

    public void ShowDetails()
    {
        Console.WriteLine("Vehicle ID: " + VehicleID);
        Console.WriteLine("Registration No: " + RegNo);
        Console.WriteLine("Model: " + Model);
        Console.WriteLine("Brand: " + Brand);
        Console.WriteLine("Base Price: $" + BasePrice);
        Console.WriteLine("Vehicle Type: " + VehicleType);
    }
}

class Car : ToolVehicle
{
    public Car(int id, string regNo, string model,
        string brand, decimal price)
        : base(id, regNo, model, brand, price, "Car")
    {
    }

    public override void PayTax()
    {
        CollectTax(2m);
    }
}

class Bike : ToolVehicle
{
    public Bike(int id, string regNo, string model,
        string brand, decimal price)
        : base(id, regNo, model, brand, price, "Bike")
    {
    }

    public override void PayTax()
    {
        CollectTax(1m);
    }
}

class HeavyVehicle : ToolVehicle
{
    public HeavyVehicle(int id, string regNo, string model,
        string brand, decimal price)
        : base(id, regNo, model, brand, price, "Heavy Vehicle")
    {
    }

    public override void PayTax()
    {
        CollectTax(4m);
    }
}

class Program
{
    static List<ToolVehicle> vehicles = new List<ToolVehicle>();

    static void Main()
    {
        int choice;

        do
        {
            Console.WriteLine("\n===== TOOL VEHICLE TAX SYSTEM =====");
            Console.WriteLine("1. Add Car");
            Console.WriteLine("2. Add Bike");
            Console.WriteLine("3. Add Heavy Vehicle");
            Console.WriteLine("4. Pay Tax");
            Console.WriteLine("5. Pass Without Paying");
            Console.WriteLine("6. Show All Vehicles");
            Console.WriteLine("7. Show Tax Report");
            Console.WriteLine("8. Exit");
            Console.Write("Enter choice: ");

            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Invalid input.");
                continue;
            }

            if (choice >= 1 && choice <= 3)
            {
                Console.Write("Enter Vehicle ID: ");
                int id;

                if (!int.TryParse(Console.ReadLine(), out id))
                {
                    Console.WriteLine("Invalid Vehicle ID.");
                    continue;
                }

                bool exists = false;

                foreach (ToolVehicle v in vehicles)
                {
                    if (v.VehicleID == id)
                    {
                        exists = true;
                        break;
                    }
                }

                if (exists)
                {
                    Console.WriteLine("Vehicle ID already exists.");
                    continue;
                }

                Console.Write("Enter Registration No: ");
                string regNo = Console.ReadLine() ?? "";

                Console.Write("Enter Model: ");
                string model = Console.ReadLine() ?? "";

                Console.Write("Enter Brand: ");
                string brand = Console.ReadLine() ?? "";

                Console.Write("Enter Base Price: ");
                decimal price;

                if (!decimal.TryParse(Console.ReadLine(), out price)
                    || price < 0)
                {
                    Console.WriteLine("Invalid price.");
                    continue;
                }

                ToolVehicle vehicle;

                if (choice == 1)
                    vehicle = new Car(id, regNo, model, brand, price);
                else if (choice == 2)
                    vehicle = new Bike(id, regNo, model, brand, price);
                else
                    vehicle = new HeavyVehicle(id, regNo, model, brand, price);

                vehicles.Add(vehicle);
                Console.WriteLine("Vehicle added successfully.");
            }
            else if (choice == 4 || choice == 5)
            {
                Console.Write("Enter Vehicle ID: ");
                int id;

                if (!int.TryParse(Console.ReadLine(), out id))
                {
                    Console.WriteLine("Invalid Vehicle ID.");
                    continue;
                }

                bool found = false;

                foreach (ToolVehicle v in vehicles)
                {
                    if (v.VehicleID == id)
                    {
                        found = true;

                        if (choice == 7)
                            v.PayTax();
                        else
                            v.PassWithoutPaying();

                        break;
                    }
                }

                if (!found)
                    Console.WriteLine("Vehicle not found.");
            }
            else if (choice == 10)
            {
                if (vehicles.Count == 0)
                {
                    Console.WriteLine("No vehicles added.");
                }
                else
                {
                    foreach (ToolVehicle v in vehicles)
                    {
                        Console.WriteLine("\n--------------------");
                        v.ShowDetails();
                    }
                }
            }
            else if (choice == 7)
            {
                Console.WriteLine("\n========== TAX REPORT ==========");
                Console.WriteLine("Total Vehicles: "
                    + ToolVehicle.TotalVehicles);
                Console.WriteLine("Tax Paying Vehicles: "
                    + ToolVehicle.TotalTaxPayingVehicles);
                Console.WriteLine("Non-Tax Paying Vehicles: "
                    + ToolVehicle.TotalNonTaxPayingVehicles);
                Console.WriteLine("Total Tax Collected: $"
                    + ToolVehicle.TotalTaxCollected);
                Console.WriteLine("================================");
            }
            else if (choice == 8)
            {
                Console.WriteLine("Program closed.");
            }
            else
            {
                Console.WriteLine("Invalid choice.");
            }

        } while (choice != 8);
    }
}

