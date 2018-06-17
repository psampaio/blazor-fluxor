using Blazor.Fluxor.UnitTests.SupportFiles;
using Moq;
using Xunit;

namespace Blazor.Fluxor.UnitTests.StoreTests
{
	public partial class StoreTests
	{
		[Fact]
		public void ActivatesMiddleware_WhenPageLoads()
		{
			var browserInteropStub = new BrowserInteropStub();
			var subject = new Store(browserInteropStub);
			var mockMiddleware = new Mock<IMiddleware>();
			subject.AddMiddleware(mockMiddleware.Object);

			browserInteropStub._TriggerPageLoaded();

			mockMiddleware
				.Verify(x => x.Initialize(subject));
		}
	}
}
