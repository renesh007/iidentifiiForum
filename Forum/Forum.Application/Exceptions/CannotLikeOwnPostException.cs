namespace Forum.Application.Exceptions
{
    public class CannotLikeOwnPostException : Exception
    {
        public CannotLikeOwnPostException()
      : base("User cannot like their own post.") { }
    }
}