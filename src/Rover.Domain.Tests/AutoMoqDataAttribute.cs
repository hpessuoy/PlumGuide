using AutoFixture.NUnit3;
using AutoFixture.AutoMoq;
using AutoFixture;

namespace Rover.Domain.Tests
{
    public class AutoMoqDataAttribute : AutoDataAttribute
    {
        public AutoMoqDataAttribute() :
            base(() => new Fixture().Customize(new AutoMoqCustomization()))
        { }
    }

    public class InlineAutoMoqDataAttribute : InlineAutoDataAttribute
    {
        public InlineAutoMoqDataAttribute(params object[] arguments) :
            base(() => new Fixture().Customize(new AutoMoqCustomization()), arguments)
        { }
    }
}
