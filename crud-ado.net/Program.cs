using System.Data;
using Microsoft.Data.SqlClient;

const string ConnectionString = "Data Source=localhost;Database=crudadonet;User Id=sa;Password=Passw0rd!;TrustServerCertificate=True;";

Menu();

static void Menu()
{
    while (true)
    {
        Console.WriteLine("\nChoose an option:");
        Console.WriteLine("1. List products (DataReader)");
        Console.WriteLine("2. List products (DataSet)");
        Console.WriteLine("3. Get product by ID");
        Console.WriteLine("4. Insert new product");
        Console.WriteLine("5. Update product");
        Console.WriteLine("6. Delete product");
        Console.WriteLine("0. Exit");

        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                GetDataWithDataReader();
                break;
            case "2":
                GetDataWithDataSet();
                break;
            case "3":
                Console.Write("Enter the product ID: ");
                int idGet = int.Parse(Console.ReadLine());
                GetById(idGet);
                break;
            case "4":
                Console.Write("Enter the name of the new product: ");
                string productName = Console.ReadLine();
                InsertProduct(productName);
                break;
            case "5":
                Console.Write("Enter the ID of the product to update: ");
                int idUpdate = int.Parse(Console.ReadLine());
                Console.Write("Enter the new product name: ");
                string newProductName = Console.ReadLine();
                UpdateProduct(idUpdate, newProductName);
                break;
            case "6":
                Console.Write("Enter the ID of the product to delete: ");
                int idDelete = int.Parse(Console.ReadLine());
                DeleteProductById(idDelete);
                break;
            case "0":
                return;
            default:
                Console.WriteLine("Invalid option! Please try again.");
                break;
        }
    }
}

static SqlConnection GetConnection()
{
    return new SqlConnection(ConnectionString);
}

static void GetDataWithDataReader()
{
    using var connection = GetConnection();
    var command = "SELECT * FROM Products";
    var sqlCommand = new SqlCommand(command, connection);

    connection.Open();
    var reader = sqlCommand.ExecuteReader();

    while (reader.Read())
    {
        Console.WriteLine($"{reader.GetInt32(0)}, {reader.GetString(1)}");
    }

    connection.Close();
}

static void GetDataWithDataSet()
{
    using var connection = GetConnection();
    connection.Open();

    var dataAdapter = new SqlDataAdapter("SELECT * FROM Products", connection);
    var dataSet = new DataSet();
    dataAdapter.Fill(dataSet);

    var productsTable = dataSet.Tables[0];
    PrintColumnNames(productsTable.Columns);

    foreach (DataRow row in productsTable.Rows)
    {
        foreach (var item in row.ItemArray)
        {
            Console.Write($"{item}\t");
        }
        Console.WriteLine();
    }

    connection.Close();
}

static void GetById(int id)
{
    using var connection = GetConnection();
    connection.Open();

    var query = "SELECT * FROM Products WHERE Id = @Id";
    var dataAdapter = new SqlDataAdapter(query, connection);
    dataAdapter.SelectCommand.Parameters.AddWithValue("@Id", id);

    var dataSet = new DataSet();
    dataAdapter.Fill(dataSet);

    var productsTable = dataSet.Tables[0];
    PrintColumnNames(productsTable.Columns);

    DataRow row = productsTable.Rows[0];
    foreach (var item in row.ItemArray)
    {
        Console.Write($"{item}\t");
    }
    Console.WriteLine();

    connection.Close();
}

static void InsertProduct(string name)
{
    using var connection = GetConnection();
    var commandText = "INSERT INTO Products (Name) VALUES (@Name)";
    var command = new SqlCommand(commandText, connection);

    command.Parameters.AddWithValue("@Name", name);
    connection.Open();
    command.ExecuteNonQuery();

    connection.Close();
}

static void UpdateProduct(int id, string name)
{
    using var connection = GetConnection();
    var commandText = "UPDATE Products SET Name = @Name WHERE Id = @Id";
    var command = new SqlCommand(commandText, connection);

    command.Parameters.AddWithValue("@Id", id);
    command.Parameters.AddWithValue("@Name", name);

    connection.Open();
    command.ExecuteNonQuery();
    connection.Close();
}

static void DeleteProductById(int id)
{
    using var connection = GetConnection();
    var commandText = "DELETE FROM Products WHERE Id = @Id";
    var command = new SqlCommand(commandText, connection);

    command.Parameters.AddWithValue("@Id", id);

    connection.Open();
    command.ExecuteNonQuery();
    connection.Close();
}

static void PrintColumnNames(DataColumnCollection columns)
{
    foreach (DataColumn column in columns)
    {
        Console.Write($"{column.ColumnName}\t");
    }
    Console.WriteLine();
}
