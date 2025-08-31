namespace Forum.Application.Exceptions
{

    public class DuplicateTagException : Exception
    {
        public DuplicateTagException()
        : base("The tag has already been applied to this post.") { }
    }
}