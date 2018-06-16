using System;
using Xunit;

namespace Blazor.Fluxor.UnitTests
{
	public class DisposableCallbackTests
	{
		[Fact]
		public void Dispose_CallsActionPassedInConstructor()
		{
			bool wasCalled = false;
			Action action = () => wasCalled = true;
			var subject = new DisposableCallback(action);

			Assert.False(wasCalled);
			subject.Dispose();
			Assert.True(wasCalled);
		}
	}
}
