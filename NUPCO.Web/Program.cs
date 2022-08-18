using HotChocolate.AspNetCore.Playground;
using HotChocolate.AspNetCore;
using AppService.GraphQLQueries.OrderQueries;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddGraphQLServer().
    AddQueryType<OrderQuery>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UsePlayground(new PlaygroundOptions
{
    QueryPath = "/graphql",
    Path = "/playground"
});

app.UseRouting()
              .UseEndpoints(endpoints =>
              {
                  endpoints.MapGraphQL();
              });

app.UseAuthorization();

app.MapRazorPages();

app.Run();
