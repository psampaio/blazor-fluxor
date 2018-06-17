using Blazor.Fluxor.Services;
using Moq;

namespace Blazor.Fluxor.UnitTests.SupportFiles
{
	public static class BrowserInteropStub
	{
		public static IBrowserInteropService Create()
		{
			return new Mock<IBrowserInteropService>().Object;
		}
	}
}
