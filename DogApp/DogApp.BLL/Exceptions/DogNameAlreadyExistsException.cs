namespace DogApp.BLL.Exceptions;

public class DogNameAlreadyExistsException : Exception
{
    public DogNameAlreadyExistsException(string name)
        : base($"Dog with name {name} alredy exists")
    {
    }
}
