namespace Domain.Exceptions
{
    public class ProductDoesNotExistException : Exception
    {

        public ProductDoesNotExistException() 
        { 
        
        }

        public ProductDoesNotExistException(string message) : base(message) 
        {

        }

        public ProductDoesNotExistException(string message, Exception innerException) : base(message, innerException)
        {

        }

        public ProductDoesNotExistException(long productId): base($"The product with Id:({productId}) does not exist in the system.")
        {

        }
    }
}
