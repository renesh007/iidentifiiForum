namespace Forum.Application.Exceptions
{
    public class EmailAlreadyExistsException : Exception
    {
        public EmailAlreadyExistsException()
       : base("Unable to complete registration using the email provided") { }
    }
}
