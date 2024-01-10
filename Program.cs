using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/test", () => {
    return CustomerService.Test();
});

app.MapGet("/customers", () => CustomerService.GetAll());

app.MapPost("/customers", (Customer[] customers) =>
{
    StringBuilder sb = new StringBuilder();
    foreach (var customer in customers)
    {
        try 
        {
            CustomerService.SortAdd(customer);
        } 
        catch (Exception ex)
        {
            sb.AppendLine(ex.Message);
        }        
    }

    if (sb.Length > 0)
        throw new ArgumentException(sb.ToString());
});

app.Run();


