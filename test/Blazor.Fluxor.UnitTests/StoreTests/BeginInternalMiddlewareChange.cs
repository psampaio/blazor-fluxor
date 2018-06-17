﻿using Moq;
using Xunit;

namespace Blazor.Fluxor.UnitTests.StoreTests
{
	public partial class StoreTests
	{
		public class BeginInternalMiddlewareChange
		{
			[Fact]
			public void ExecutesOnAllRegisteredMiddlewares()
			{
				int disposeCount = 0;
				var mockMiddleware = new Mock<IMiddleware>();
				mockMiddleware
					.Setup(x => x.BeginInternalMiddlewareChange())
					.Returns(new DisposableCallback(() => disposeCount++));

				var subject = new Store();
				subject.AddMiddleware(mockMiddleware.Object);

				var disposable1 = subject.BeginInternalMiddlewareChange();
				var disposable2 = subject.BeginInternalMiddlewareChange();
				disposable1.Dispose();
				disposable2.Dispose();

				Assert.Equal(1, disposeCount);
			}
		}
	}
}
