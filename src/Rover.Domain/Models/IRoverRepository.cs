namespace Rover.Domain.Models
{
    public interface IRoverRepository
    {
        bool TryGet(string name, out Location location);
        void Update(Rover rover);
    }
}
