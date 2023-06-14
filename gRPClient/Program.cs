using Core;
using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using Proto;
using Enum = System.Enum;

using var channel = GrpcChannel.ForAddress("https://localhost:7184");
var client = new Pet.PetClient(channel);
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
                var request = new AddPetRequest();

                Console.WriteLine("Введи имя");
                request.Name = Console.ReadLine()!;

                Console.WriteLine("Введи возраст");
                request.Age = int.Parse(Console.ReadLine()!);

                var response = await client.AddAsync(request);
                Console.WriteLine("Id созданного домашнего животного {0}", response.Id);
                break;
            }
            case Command.Delete:
            {
                var request = new DeletePetRequest();

                Console.WriteLine("Введи Id");
                request.Id = int.Parse(Console.ReadLine()!);

                var response = await client.DeleteAsync(request);
                Console.WriteLine("Id удаленного домашнего животного {0}", response.Id);
                break;
            }
            case Command.Update:
            {
                var request = new UpdatePetRequest();

                Console.WriteLine("Введи Id");
                request.Id = int.Parse(Console.ReadLine()!);

                Console.WriteLine("Введи имя");
                request.Name = Console.ReadLine()!;

                Console.WriteLine("Введи возраст");
                request.Age = int.Parse(Console.ReadLine()!);

                var response = await client.UpdateAsync(request);
                Console.WriteLine("Id обновленного домашнего животного {0}", response.Id);
                break;
            }
            case Command.GetList:
            {
                var request = new Empty();

                var response = await client.GetListAsync(request);
                foreach (var responseItem in response.Items)
                {
                    Console.WriteLine("Id {0}, Name {1}, Age {2}", responseItem.Id, responseItem.Name,
                        responseItem.Age);
                }

                if (!response.Items.Any())
                {
                    Console.WriteLine("Список пустой! Заполни его скорее!!!");
                }

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