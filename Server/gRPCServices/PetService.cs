using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Proto;

namespace Server.gRPCServices;

public sealed class PetService : Pet.PetBase
{
    private readonly PetsStorage _petsStorage;

    public PetService(PetsStorage petsStorage)
    {
        _petsStorage = petsStorage;
    }

    public override async Task<AddPetResponse> Add(AddPetRequest request, ServerCallContext context)
    {
        var pet = new Entities.Pet
        {
            Age = request.Age,
            Name = request.Name
        };
        
        await _petsStorage.AddAsync(pet);

        return new AddPetResponse
        {
            Age = pet.Age,
            Id = pet.Id,
            Name = pet.Name
        };
    }

    public override async Task<DeletePetResponse> Delete(DeletePetRequest request, ServerCallContext context)
    {
        await _petsStorage.DeleteAsync(request.Id);
        return new DeletePetResponse
        {
            Id = request.Id
        };
    }

    public override async Task<UpdatePetResponse> Update(UpdatePetRequest request, ServerCallContext context)
    {
        var pet = new Entities.Pet
        {
            Age = request.Age,
            Name = request.Name
        };
        
        await _petsStorage.UpdateAsync(request.Id, pet);

        return new UpdatePetResponse
        {
            Id = request.Id
        };
    }

    public override Task<PetListResponse> GetList(Empty request, ServerCallContext context)
    {
        var response = new PetListResponse();
        response.Items.AddRange(_petsStorage.Pets.ToList().Select(p => new PetItem
        {
            Age = p.Age,
            Id = p.Id,
            Name = p.Name
        }));

        return Task.FromResult(response);
    }
}