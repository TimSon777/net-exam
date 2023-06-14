using Server.Entities;

namespace Server.GraphQLQueries;

public sealed class PetQuery
{
    private readonly PetsStorage _petsStorage;

    public PetQuery(PetsStorage petsStorage)
    {
        _petsStorage = petsStorage;
    }

    public IQueryable<Pet> GetPets()
    {
        return _petsStorage.Pets;
    }
}