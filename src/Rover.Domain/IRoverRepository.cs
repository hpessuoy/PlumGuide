namespace Rover.Domain
{
    public interface IRoverRepository
    {
        bool TryGet(string name, out Location location);
    }
}
