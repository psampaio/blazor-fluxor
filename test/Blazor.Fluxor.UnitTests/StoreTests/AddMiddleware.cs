using Blazor.Fluxor.UnitTests.SupportFiles;
using Moq;
using Xunit;

namespace Blazor.Fluxor.UnitTests.StoreTests
{
	public partial class StoreTests
	{
		public class AddMiddleware
		{
			[Fact]
			public void ActivatesMiddleware_WhenPageHasAlreadyLoaded()
			{
				var browserInteropStub = new BrowserInteropStub();
				var subject = new Store(browserInteropStub);
				browserInteropStub._TriggerPageLoaded();

				var mockMiddleware = new Mock<IMiddleware>();
				subject.AddMiddleware(mockMiddleware.Object);

				mockMiddleware
					.Verify(x => x.Initialize(subject));
			}

			[Fact]
			public void CallsAfterInitializeAllMiddlewares_WhenPageHasAlreadyLoaded()
			{
				var browserInteropStub = new BrowserInteropStub();
				var subject = new Store(browserInteropStub);
				browserInteropStub._TriggerPageLoaded();

				var mockMiddleware = new Mock<IMiddleware>();
				subject.AddMiddleware(mockMiddleware.Object);

				mockMiddleware
					.Verify(x => x.AfterInitializeAllMiddlewares());
			}
		}
	}
}
