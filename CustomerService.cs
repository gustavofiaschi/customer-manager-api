public static class CustomerService
{
    private static List<Customer> customers = new List<Customer>();

    public static List<Customer> GetAll() => customers;
    
    public static void SortAdd(Customer customer)
    {
        if (customers.Any(c => c.id == customer.id))
            throw new ArgumentException ($"Customer #{customer.id} already exists");

        if(customers.Count == 0)
        {
            customers.Add(customer);
            return;
        }

        for (int i = 0; i < customers.Count; i++)
        {
            var orderLast = String.Compare(customers[i].lastName, customer.lastName);
            var orderFirst = String.Compare(customers[i].firstName, customer.firstName);

            if(orderLast < 0)
            {
                customers.Add(customer);
                return;
            }
            else if(orderLast > 0)
            {
                customers.Insert(i, customer);
                return;
            }
            else 
            {
                if(orderFirst < 0)
                {
                    customers.Add(customer);
                    return;
                }
                else if(orderFirst > 0)
                {
                    customers.Insert(i, customer);
                    return;
                }
                else
                {
                    customers.Add(customer);
                }
            }
        }
    }
}