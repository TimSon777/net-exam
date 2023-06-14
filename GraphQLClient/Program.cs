using System.Text;
using ApiClient;
using Core;

using var client = new HttpClient
{
    BaseAddress = new Uri("https://localhost:7184/")
};

var commands = Enum.GetNames<Command>();
var commandsString = string.Join(", ", commands);

try
{
    while (true)
    {
        Console.WriteLine($"Введи операцию. Доступны: {commandsString}");
        var command = Enum.Parse<Command>(Console.ReadLine()!, true);

        switch (command)
        {
            case Command.Add:
            {
                Console.WriteLine("Введи имя");
                var name = Console.ReadLine()!;

                Console.WriteLine("Введи возраст");
                var age = int.Parse(Console.ReadLine()!);

                var query = new PetMutationQueryBuilder()
                    .WithAddPet(new PetQueryBuilder().WithAllScalarFields(), new AddPetInput
                    {
                        Age = age,
                        Name = name
                    })
                    .Build();

                var response = await MakeRequest(query);
                Console.WriteLine("Response {0}", response);
                break;
            }
            case Command.Delete:
            {


                Console.WriteLine("Введи Id");
                var id = int.Parse(Console.ReadLine()!);

                var query = new PetMutationQueryBuilder()
                    .WithDeletePet(new DeletePetInput
                    {
                        Id = id
                    })
                    .Build();
                
                var response = await MakeRequest(query);
                Console.WriteLine("Response {0}", response);
                break;
            }
            case Command.Update:
            {


                Console.WriteLine("Введи Id");
                var id = int.Parse(Console.ReadLine()!);

                Console.WriteLine("Введи имя");
                var name = Console.ReadLine()!;

                Console.WriteLine("Введи возраст");
                var age = int.Parse(Console.ReadLine()!);

                var query = new PetMutationQueryBuilder()
                    .WithUpdatePet(new UpdatePetInput
                    {
                        Id = id,
                        Age = age,
                        Name = name
                    })
                    .Build();
                
                var response = await MakeRequest(query);
                Console.WriteLine("Response {0}", response);
                break;
            }
            case Command.GetList:
            {
                var query = new PetQueryQueryBuilder()
                    .WithPets(new PetQueryBuilder()
                        .WithAge()
                        .WithName()
                        .WithId())
                    .Build();

                var response = await MakeRequest(query);
                Console.WriteLine("Response {0}", response);
                break;
            }
            case Command.Exit:
            {
                Console.WriteLine("Ну вот и все. До встречи!");
                return 0;
            }
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
catch (Exception)
{
    Console.Error.WriteLine("Что-то пошло не так!");
    throw;
}

async Task<string> MakeRequest(string query)
{
    var json = "{\"query\":\"" + query.Replace("\"", "\\\"") + "\"}";
    var queryData = new StringContent(json, Encoding.UTF8, "application/json");
    var queryResponse = await client.PostAsync("/graphql", queryData);
    return await queryResponse.Content.ReadAsStringAsync()!;
}
