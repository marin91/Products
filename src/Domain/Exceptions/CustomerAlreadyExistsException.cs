namespace Domain.Exceptions
{
    public class CustomerAlreadyExistsException : Exception
    {
        public CustomerAlreadyExistsException() 
        { 
        
        }

        public CustomerAlreadyExistsException(string message) : base(message) 
        { 
        
        }

        public CustomerAlreadyExistsException(string message,  Exception innerException) : base(message, innerException)
        {

        }

        public CustomerAlreadyExistsException(long productId) : base($"The product with {productId} already exists in the system.")
        {

        }
    }
}
