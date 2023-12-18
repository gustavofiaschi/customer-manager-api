using LiteDB;

/// <summary>
/// This class is responsible for storing and retrieving data from the database.
/// </summary>
public static class CustomerDataService 
{
    const string databaseName = "CustomersData.db"; //move to appSettings.json
    public static IEnumerable<Customer> GetCustomers()
    {
        using(var db = new LiteDatabase(databaseName))
        {            
            var col = db.GetCollection<Customer>("customers");                        
            return col.FindAll().ToList();
        }
    }

    public static void SaveData(IEnumerable<Customer> customers)
    {
        using(var db = new LiteDatabase(databaseName))
        {
            var col = db.GetCollection<Customer>("customers");   
            col.DeleteAll();
            col.InsertBulk(customers);
        }
    }
}