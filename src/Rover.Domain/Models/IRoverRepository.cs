namespace Rover.Domain.Models
{
    /// <summary>
    /// Rover repository.
    /// </summary>
    public interface IRoverRepository
    {
        /// <summary>
        /// Retrieves a rover by name.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        bool TryGetByName(string name, out Location location);

        /// <summary>
        /// Saves the given rover state.
        /// </summary>
        /// <param name="rover"></param>
        void Update(Rover rover);
    }
}
