namespace Domain.Exceptions
{
    public class ProductAlreadyExistsException : Exception
    {
        public ProductAlreadyExistsException() 
        { 
        
        }

        public ProductAlreadyExistsException(string message) : base(message) 
        { 
        
        }

        public ProductAlreadyExistsException(string message,  Exception innerException) : base(message, innerException)
        {

        }

        public ProductAlreadyExistsException(long productId) : base($"The product with {productId} already exists in the system.")
        {

        }
    }
}
