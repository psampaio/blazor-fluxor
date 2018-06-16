using Blazor.Fluxor.UnitTests.SupportFiles;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Blazor.Fluxor.UnitTests
{
	public class MiddlewareTests
	{
		[Fact]
		public void IsInsideMiddlewareChange_ShouldBeTrue_UntilBeginInternalMiddlewareChangeIsDisposed()
		{
			var subject = new MiddlewareWithExposedMembers();
			var disposables = new Queue<IDisposable>();

			Assert.Equal(0, subject._BeginMiddlewareChangeCount);
			Assert.False(subject._IsInsideMiddlewareChange);

			int expectedChangeCount = 0;
			for(int i = 1; i <= 10; i++)
			{
				expectedChangeCount = i;
				IDisposable disposable = subject._BeginInternalMiddlewareChange();
				disposables.Enqueue(disposable);

				Assert.Equal(expectedChangeCount, subject._BeginMiddlewareChangeCount);
				Assert.True(subject._IsInsideMiddlewareChange);
			}
			do
			{
				Assert.Equal(expectedChangeCount, subject._BeginMiddlewareChangeCount);
				Assert.True(subject._IsInsideMiddlewareChange);

				disposables.Dequeue().Dispose();
				expectedChangeCount--;
			} while (disposables.Count > 0);

			Assert.Equal(0, subject._BeginMiddlewareChangeCount);
			Assert.False(subject._IsInsideMiddlewareChange);
		}
	}
}
