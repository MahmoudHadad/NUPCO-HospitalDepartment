using AnemicModel;
using AppService.DepartmentRequests.MRQueries.RequestsQueryMR;
using AppService.GraphQLQueries.OrderQueries;
using AppService.OrderRequest.MRQueries.OrderRequestsQueryMR;
using AppService.Services;
using Core.Interfaces;
using Core.Interfaces.IOrderRequest;
using Core.Mocking;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IRequestRepoMock, RequestRepoMock>();
builder.Services.AddScoped<IRequestService, RequestService>();
builder.Services.AddScoped<IOrderRequestService, OrderRequestService>();


builder.Services.AddMediatR(typeof(GetAllRequestsQueryMR).Assembly);
builder.Services.AddMediatR(typeof(GetAllOrderRequestsQueryMR).Assembly);
builder.Services.AddMediatR(typeof(Program).Assembly);

builder.Services.AddGraphQLServer().
    AddQueryType<GetAllRequestsQuery>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseRouting();

//app.UsePlayground(new PlaygroundOptions
//{
//    QueryPath = "/graphql",
//    Path = "/playground"
//});

app.UseRouting()
              .UseEndpoints(endpoints =>
              {
                  endpoints.MapGraphQL();
              });

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
