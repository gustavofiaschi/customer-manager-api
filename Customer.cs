using System.Text.Json.Serialization;

public class CustomerEntry 
{
    public CustomerEntry()
    {
        
    }
    public CustomerEntry(Customer customer)
    {
        Customer = customer;
    }

    [JsonPropertyName("customers")]
    public Customer Customer { get; set; }

}

public class Customer
{
    public Customer(string firstName, string lastName, int age, int id)
    {
        FirstName = firstName;
        LastName = lastName;
        Age = age;
        Id = id;
    }

    [JsonPropertyName("firstName")]
    public string FirstName { get; set; }

    [JsonPropertyName("lastName")]
    public string LastName { get; set; }

    [JsonPropertyName("age")]
    public int Age { get; set; }
    
    [JsonPropertyName("id")]
    public int Id { get; set; }    
}