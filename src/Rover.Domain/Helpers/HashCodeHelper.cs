namespace Rover.Domain.Helpers
{
    // TODO: move it to the infra layer if needed.
    public static class HashCodeHelper
    {
        public static int CombineHashCodes(int h1, int h2)
        {
            return (h1 << 5) + h1 ^ h2;
        }

        public static int CombineHashCodes(int h1, int h2, int h3)
        {
            return CombineHashCodes(CombineHashCodes(h1, h2), h3);
        }
    }
}
