namespace Exception_Library
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string message) : base(message)
        {
        }
    }

    public class ExpenseNotFoundException : Exception
    {
        public ExpenseNotFoundException(string message) : base(message)
        {
        }
    }
}
