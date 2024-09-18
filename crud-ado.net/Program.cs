using System.Data;
using Microsoft.Data.SqlClient;

const string ConnectionString = "Data Source=localhost;Database=crudadonet;User Id=sa;Password=Passw0rd!;TrustServerCertificate=True;";

// PrintDataWithDataReader();
// GetDataWithDataSet();
// GetById(2);
// InsertProduct("Other Product");
// UpdateProduct(1, "First Update Product");
DeleteProductById(3);

static void PrintDataWithDataReader()
{
    var connection = new SqlConnection(ConnectionString);

    var command = "SELECT * FROM Products";
    var sqlCommand = new SqlCommand(command, connection);
    sqlCommand.Connection.Open();

    var reader = sqlCommand.ExecuteReader();


    while (reader.Read())
    {
        Console.WriteLine($"{reader.GetInt32(0)}, {reader.GetString(1)}");
    }

    connection.Close();
}

static void GetDataWithDataSet()
{
    var connection = new SqlConnection(ConnectionString);

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
    var connection = new SqlConnection(ConnectionString);

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
    var connection = new SqlConnection(ConnectionString);
    var commandText = "INSERT INTO Products (Name) VALUES (@Name)";

    var command = new SqlCommand(commandText, connection)
    {
        CommandType = CommandType.Text
    };
    command.Parameters.AddWithValue("@Name", name);
    command.Connection.Open();
    command.ExecuteNonQuery();

    connection.Close();
}

static void UpdateProduct(int id, string name)
{
    var connection = new SqlConnection(ConnectionString);
    var commandText = "UPDATE Products SET Name = @Name WHERE Id = @Id";

    var command = new SqlCommand(commandText, connection)
    {
        CommandType = CommandType.Text
    };
    command.Parameters.AddWithValue("@Id", id);
    command.Parameters.AddWithValue("@Name", name);

    command.Connection.Open();
    command.ExecuteNonQuery();

    connection.Close();
}

static void DeleteProductById(int id)
{
    var connection = new SqlConnection(ConnectionString);
    var commandText = "DELETE FROM Products WHERE Id = @Id";

    var command = new SqlCommand(commandText, connection)
    {
        CommandType = CommandType.Text
    };
    command.Parameters.AddWithValue("@Id", id);

    command.Connection.Open();
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