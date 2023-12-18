using LiteDB;

/// <summary>
/// This class is responsible to provide the business logic for the customer.
/// </summary>
public static class CustomerService
{        
    private static List<Customer> customers = CustomerDataService.GetCustomers().ToList();

    private static void Validate(Customer customer)
    {
        if (string.IsNullOrWhiteSpace(customer.firstName))
            throw new ArgumentNullException(nameof(customer.firstName));

        if (string.IsNullOrWhiteSpace(customer.lastName))
            throw new ArgumentNullException(nameof(customer.lastName));

        if (customer.age < 18)
            throw new ArgumentOutOfRangeException(nameof(customer.age));
        
        if (customer.id <= 0)
            throw new ArgumentOutOfRangeException(nameof(customer.id));
            
        if (customers.Any(c => c.id == customer.id))
            throw new ArgumentException ($"Customer #{customer.id} already exists");
    }

    public static List<Customer> GetAll() => CustomerDataService.GetCustomers().ToList();
    
    public static void SortAdd(Customer customer)
    {
        Validate(customer);
        
        if(customers.Count == 0)
        {
            customers.Add(customer);
            CustomerDataService.SaveData(customers);
            return;
        }

        for (int i = 0; i < customers.Count; i++)
        {
            var orderLast = String.Compare(customers[i].lastName, customer.lastName);
            var orderFirst = String.Compare(customers[i].firstName, customer.firstName);

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