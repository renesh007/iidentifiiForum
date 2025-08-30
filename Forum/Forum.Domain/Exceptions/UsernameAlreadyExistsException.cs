namespace Forum.Domain.Exceptions
{
    public class UsernameAlreadyExistsException : Exception
    {
        public UsernameAlreadyExistsException(string name)
       : base($"User with name '{name}' already exists.") { }
    }
}
