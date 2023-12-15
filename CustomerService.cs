using LiteDB;

public static class CustomerService
{        
    public static List<Customer> GetAll() => CustomerDataService.LoadCustomers().ToList();
    
    public static void SortAdd(Customer customer)
    {
        var customers = CustomerDataService.LoadCustomers().ToList();

        if (customers.Any(c => c.id == customer.id))
            throw new ArgumentException ($"Customer #{customer.id} already exists");

        if(customers.Count == 0)
        {
            customers.Add(customer);
            CustomerDataService.StoreData(customers);
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
        
        CustomerDataService.StoreData(customers);
    }

    
}