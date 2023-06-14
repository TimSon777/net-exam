using Server.Entities;

namespace Server.GraphQLMutations;

public sealed class PetMutation
{
    private readonly PetsStorage _petsStorage;

    public PetMutation(PetsStorage petsStorage)
    {
        _petsStorage = petsStorage;
    }

    public async Task<Pet> AddPetAsync(AddPet input)
    {
        var pet = new Pet
        {
            Name = input.Name,
            Age = input.Age
        };

        await _petsStorage.AddAsync(pet);

        return new Pet
        {
            Id = pet.Id,
            Name = pet.Name,
            Age = pet.Age
        };
    }

    public async Task<int> DeletePetAsync(DeletePet input)
    {
        await _petsStorage.DeleteAsync(input.Id);
        return input.Id;
    }
    
    public async Task<int> UpdatePetAsync(UpdatePet input)
    {
        var pet = new Pet
        {
            Id = input.Id,
            Age = input.Age,
            Name = input.Name
        };
        
        await _petsStorage.UpdateAsync(input.Id, pet);
        return input.Id;
    }
}

public sealed class AddPet
{
    public string Name { get; set; } = default!;
    public int Age { get; set; }
}

public sealed class UpdatePet
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public int Age { get; set; }
}

public sealed class DeletePet
{
    public int Id { get; set; }
}
