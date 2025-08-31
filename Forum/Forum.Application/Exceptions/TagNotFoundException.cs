namespace Forum.Application.Exceptions
{

    public class TagNotFoundException : Exception
    {
        public TagNotFoundException()
        : base("The specified tag was not found.") { }
    }
}