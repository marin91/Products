namespace Domain.Abstractions
{
    /// <summary>
    /// Defines a contract for mapping an object of type <typeparamref name="TSource"/> to an object of type <typeparamref name="TDestination"/>.
    /// </summary>
    /// <typeparam name="TSource">The source type to be mapped from.</typeparam>
    /// <typeparam name="TDestination">The destination type to be mapped to.</typeparam>
    public interface IMap<TSource, TDestination>
    {
        /// <summary>
        /// Maps an instance of <typeparamref name="TSource"/> to an instance of <typeparamref name="TDestination"/>.
        /// </summary>
        /// <param name="source">The source object to map from.</param>
        /// <returns>The mapped instance of <typeparamref name="TDestination"/>.</returns>
        public TDestination Map(TSource source);
    }
}
