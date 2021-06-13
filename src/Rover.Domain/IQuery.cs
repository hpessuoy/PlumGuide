namespace Rover.Domain
{
    /// <summary>
    /// Represent a query which return a <para name="TResult"/> result from a <para name="TParameter"/> parameter
    /// </summary>
    /// <typeparam name="TResult">The type of the result returned by the query</typeparam>
    /// <typeparam name="TParameter">The type of the parameter used by the query</typeparam>
    public interface IQuery<out TResult, in TParameter>
    {
        /// <summary>
        /// Execute the query
        /// </summary>
        /// <param name="parameter">The parameter of the query</param>
        /// <returns></returns>
        TResult Execute(TParameter parameter);
    }
}
