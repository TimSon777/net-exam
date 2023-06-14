using Server.Entities;

namespace Server;

public sealed class PetsStorage
{
    private int _increment = 1;

    public IQueryable<Pet> Pets => _pets.AsQueryable();

    private readonly List<Pet> _pets = new();

    public Task AddAsync(Pet pet)
    {
        pet.Id = _increment++;
        _pets.Add(pet);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        var pet = _pets.First(p => p.Id == id);
        _pets.Remove(pet);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(int id, Pet pet)
    {
        var otherPet = _pets.First(p => p.Id == id);
        otherPet.Age = pet.Age;
        otherPet.Name = pet.Name;
        return Task.CompletedTask;
    }
}