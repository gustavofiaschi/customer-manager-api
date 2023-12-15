using LiteDB;

public static class CustomerDataService 
{
    const string databaseName = "CustomersData.db";
    public static IEnumerable<Customer> LoadCustomers()
    {
        using(var db = new LiteDatabase(databaseName))
        {
            // Get customer collection
            var col = db.GetCollection<Customer>("customers");
            
            // Create unique index in Name field
            col.EnsureIndex(x => x.id, true);
            
            return col.FindAll().ToList();
        }
    }

    public static void StoreData(IEnumerable<Customer> customers)
    {
        using(var db = new LiteDatabase(databaseName))
        {
            var col = db.GetCollection<Customer>("customers");   
            col.DeleteAll();
            col.InsertBulk(customers);
        }
    }
}