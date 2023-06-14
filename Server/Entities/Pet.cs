namespace Server.Entities;

public sealed class Pet
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public int Age { get; set; }
}