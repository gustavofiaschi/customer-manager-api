using System.Text.Json.Serialization;

public class CustomerClient
{
    
    public CustomerClient(string firstName, string lastName, int age, int id)
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