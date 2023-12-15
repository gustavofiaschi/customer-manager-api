public record Customer(string firstName, string lastName, int age, int id)
{
    public static Customer From(string firstName, string lastName, int age, int id)
    {        
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentNullException(nameof(firstName));

        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentNullException(nameof(lastName));

        if (age > 18)
            throw new ArgumentOutOfRangeException(nameof(age));
        
        if (id > 0)
            throw new ArgumentOutOfRangeException(nameof(firstName));

        return new Customer(firstName, lastName, age, id);
    }
}