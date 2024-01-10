using LiteDB;

/// <summary>
/// This class is responsible to provide the business logic for the customer.
/// </summary>
public static class CustomerService
{        
    private static List<Customer> customers = CustomerDataService.GetCustomers().ToList();

    private static void Validate(Customer customer)
    {
        if (string.IsNullOrWhiteSpace(customer.FirstName))
            throw new ArgumentNullException(nameof(customer.FirstName));

        if (string.IsNullOrWhiteSpace(customer.LastName))
            throw new ArgumentNullException(nameof(customer.LastName));

        if (customer.Age < 18)
            throw new ArgumentOutOfRangeException(nameof(customer.Age));
        
        if (customer.Id <= 0)
            throw new ArgumentOutOfRangeException(nameof(customer.Id));
            
        if (customers.Any(c => c.Id == customer.Id))
            throw new ArgumentException ($"Customer #{customer.Id} already exists");
    }

    public static string Test() => CustomerDataService.Test();

    public static List<Customer> GetAll() => CustomerDataService.GetCustomers().ToList();
    
    public static void SortAdd(Customer customer)
    {
        customers = CustomerDataService.GetCustomers().ToList();
        
        Validate(customer);
        
        if(customers.Count == 0)
        {
            customers.Add(customer);
            CustomerDataService.SaveData(customers);
            return;
        }

        for (int i = 0; i < customers.Count; i++)
        {
            var orderLast = String.Compare(customers[i].LastName, customer.LastName);
            var orderFirst = String.Compare(customers[i].FirstName, customer.FirstName);

            if(orderLast < 0)
            {
                customers.Add(customer);
                break;
            }
            else if(orderLast > 0)
            {
                customers.Insert(i, customer);
                break;
            }
            else 
            {
                if(orderFirst < 0)
                {
                    customers.Add(customer);
                    break;
                }
                else if(orderFirst > 0)
                {
                    customers.Insert(i, customer);
                    break;
                }
                else
                {
                    customers.Add(customer);
                }
            }
        }

        CustomerDataService.SaveData(customers);
    }

    
}