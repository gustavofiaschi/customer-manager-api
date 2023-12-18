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