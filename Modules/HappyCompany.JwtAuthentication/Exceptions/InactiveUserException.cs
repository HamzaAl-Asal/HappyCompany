namespace HappyCompany.JwtAuthentication.Exceptions
{
    public class InactiveUserException : Exception
    {
        public InactiveUserException(string message) : base(message) { }
    }
}