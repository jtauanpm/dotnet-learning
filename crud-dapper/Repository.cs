using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;

namespace crud_dapper;

public class Repository
{
    const string ConnectionString = "Data Source=localhost;Database=CrudProducts;User Id=sa;Password=Passw0rd!;TrustServerCertificate=True;";
    public static void ListProducts()
    {
        using var connection = new SqlConnection(ConnectionString);
        var products = connection.GetAll<Product>().ToList();
        foreach (var product in products)
        {
            Console.WriteLine($"Id: {product.Id}\tName: {product.Name}");
        }
    }

    public static void GetById(int id)
    {
        using var connection = new SqlConnection(ConnectionString);
        var product = connection.Get<Product>(id);
        if (product != null)
        {
            Console.WriteLine($"Id: {product.Id}\tName: {product.Name}");
        }
        else
        {
            Console.WriteLine("Product not found!");
        }
    }

    public static void InsertProduct(string name)
    {
        using var connection = new SqlConnection(ConnectionString);
        connection.Insert(new Product { Name = name });
        Console.WriteLine("Product inserted successfully!");
    }

    public static void UpdateProduct(int id, string newName)
    {
        using var connection = new SqlConnection(ConnectionString);
        var product = connection.Get<Product>(id);
        if (product != null)
        {
            product.Name = newName;
            connection.Update(product);
            Console.WriteLine("Product updated successfully!");
        }
        else
        {
            Console.WriteLine("Product not found!");
        }
    }

    public static void DeleteProductById(int id)
    {
        using var connection = new SqlConnection(ConnectionString);
        var product = connection.Get<Product>(id);
        if (product != null)
        {
            connection.Delete(product);
            Console.WriteLine("Product deleted successfully!");
        }
        else
        {
            Console.WriteLine("Product not found!");
        }
    }
}
