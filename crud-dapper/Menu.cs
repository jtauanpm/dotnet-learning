using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crud_dapper;

public static class Menu
{
    public static void Run()
    {
        while (true)
        {
            Console.WriteLine("\nChoose an option:");
            Console.WriteLine("1. List products");
            Console.WriteLine("2. Get product by ID");
            Console.WriteLine("3. Insert new product");
            Console.WriteLine("4. Update product");
            Console.WriteLine("5. Delete product");
            Console.WriteLine("0. Exit");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Repository.ListProducts();
                    break;
                case "2":
                    Console.Write("Enter the product ID: ");
                    int idGet = int.Parse(Console.ReadLine());
                    Repository.GetById(idGet);
                    break;
                case "3":
                    Console.Write("Enter the name of the new product: ");
                    string productName = Console.ReadLine();
                    Repository.InsertProduct(productName);
                    break;
                case "4":
                    Console.Write("Enter the ID of the product to update: ");
                    int idUpdate = int.Parse(Console.ReadLine());
                    Console.Write("Enter the new product name: ");
                    string newProductName = Console.ReadLine();
                    Repository.UpdateProduct(idUpdate, newProductName);
                    break;
                case "5":
                    Console.Write("Enter the ID of the product to delete: ");
                    int idDelete = int.Parse(Console.ReadLine());
                    Repository.DeleteProductById(idDelete);
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Invalid option! Please try again.");
                    break;
            }
        }
    }
}
