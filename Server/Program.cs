using Server;
using Server.GraphQLMutations;
using Server.GraphQLQueries;
using Server.gRPCServices;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddSingleton<PetsStorage>();
services.AddScoped<PetMutation>();
services.AddGrpc();
services.AddGraphQLServer()
    .AddQueryType<PetQuery>()
    .AddMutationType<PetMutation>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapBananaCakePop();
app.MapGraphQL();
app.MapGrpcService<PetService>();
app.Run();