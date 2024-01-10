using System.Text;
using LiteDB;

/// <summary>
/// This class is responsible for storing and retrieving data from the database.
/// </summary>
public static class CustomerDataService 
{
    const string databaseName = "CustomersData.db"; //move to appSettings.json

    public static string Test()
    {
        StringBuilder sb = new StringBuilder();
        try
        {
            sb.AppendLine($"File {databaseName} exists: {System.IO.File.Exists(databaseName)}");

            if(!System.IO.File.Exists(databaseName)) 
            {
                sb.AppendLine("Creating database...");
                using(var db = new LiteDatabase(databaseName))
                {            
                    sb.AppendLine($"db.UserVersion: {db.UserVersion}");
                }
            }
        }
        catch (System.Exception ex)
        {
            sb.AppendLine($"Erro: {ex.Message}");
        }     

        return sb.ToString();   
    }

    public static IEnumerable<Customer> GetCustomers()
    {
        using(var db = new LiteDatabase(databaseName))
        {            
            var col = db.GetCollection<CustomerEntry>("customers");                        
            var entries = col.FindAll().ToList();
            foreach (var entry in entries)
            {
                yield return entry.Customer;
            }
        }
    }

    public static void SaveData(IEnumerable<Customer> customers)
    {
        List<CustomerEntry> customersEntry = new List<CustomerEntry>();
        foreach (var customer in customers)
        {
            customersEntry.Add(new CustomerEntry(customer));
        }

        using(var db = new LiteDatabase(databaseName))
        {
            var col = db.GetCollection<CustomerEntry>("customers");   
            col.DeleteAll();
            col.InsertBulk(customersEntry);
        }
    }
}