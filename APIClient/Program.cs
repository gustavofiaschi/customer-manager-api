namespace APIClient;

using System.ComponentModel;
using System.Text;
using System.Text.Json;

class Program
{
    static string[] firstNames = ["Leia", "Sadie", "Jose", "Sara", "Frank", "Dewey", "Tomas", "Joel", "Lukas", "Carlos" ];
    static string[] lastNames = ["Liberty", "Ray", "Harrison", "Ronan", "Drew", "Powell", "Larsen", "Chan", "Anderson", "Lane"]; 
    static void Main(string[] args)
    {
        RunPostAsync().Wait();
        RunGetAsync().Wait();
        RunPostAsync(6).Wait();
        RunGetAsync().Wait();
    }

    static async Task RunGetAsync()
    {
        var client = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5225/")
        };
        var response = await client.GetAsync("customers");        
        Console.WriteLine($"{(int)response.StatusCode} ({response.ReasonPhrase})");
        Console.WriteLine($"Content: {response.Content}");        
    }
    
    static async Task RunPostAsync(int initialId = 0)
    {
        var client = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5225/")
        };
        var customers = generateCustomers(initialId);
        var payload = JsonSerializer.Serialize(customers);
        var content = new StringContent(payload, Encoding.UTF8, "application/json");

        var response = await client.PostAsync("customers", content);
        Console.WriteLine($"{(int)response.StatusCode} ({response.ReasonPhrase})");
    }

    private static List<CustomerClient> generateCustomers(int initialId)
    {
        var random = new Random();
        var firstNamesChoices = random.GetItems(firstNames, 6);
        var lastNamesChoices = random.GetItems(lastNames, 6);

        var listCustmors = new List<CustomerClient>();

        for (int i = 0; i < firstNamesChoices.Length; i++)
        {
            listCustmors.Add(new CustomerClient(firstNamesChoices[i], lastNamesChoices[i], random.Next(10, 90), initialId++));
        }

        return listCustmors;
    }
}
