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

    //public class DomainCustomization : CompositeCustomization
    //{
    //    public DomainCustomization() : this(true) { }

    //    public DomainCustomization(bool autoConfigureMoq)
    //        : base(
    //                new DefaultCustomization(autoConfigureMoq)
    //            )
    //    {
    //    }
    //}

    //public class DefaultCustomization : CompositeCustomization
    //{
    //    public DefaultCustomization() : this(true) { }

    //    public DefaultCustomization(bool autoConfigureMoq)
    //        : base(new AutoMoqCustomization() { ConfigureMembers = autoConfigureMoq })
    //    {
    //    }
    //}

    //public class DomainAutoDataAttribute : AutoDataAttribute
    //{
    //    public DomainAutoDataAttribute(bool autoConfigureMoq = true)
    //        : base(() => new Fixture().Customize(new DomainCustomization(autoConfigureMoq)))
    //    {
    //    }
    //}

    //public class DomainInlineAutoDataAttribute : InlineAutoDataAttribute
    //{
    //    public DomainInlineAutoDataAttribute(params object[] values)
    //        : base(() => new Fixture().Customize(new DomainCustomization()), values)
    //    {
    //    }
    //}
}
