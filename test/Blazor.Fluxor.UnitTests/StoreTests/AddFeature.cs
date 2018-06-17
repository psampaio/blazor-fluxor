﻿using Blazor.Fluxor.UnitTests.SupportFiles;
using Moq;
using System;
using Xunit;

namespace Blazor.Fluxor.UnitTests.StoreTests
{
	public partial class StoreTests
	{
		public class AddFeature
		{
			[Fact]
			public void AddsFeatureToFeaturesDictionary()
			{
				const string featureName = "123";
				var mockFeature = new Mock<IFeature>();
				mockFeature
					.Setup(x => x.GetName())
					.Returns(featureName);

				var subject = new Store(new BrowserInteropStub());
				subject.AddFeature(mockFeature.Object);

				Assert.Same(mockFeature.Object, subject.Features[featureName]);
			}

			[Fact]
			public void ThrowsArgumentException_WhenFeatureWithSameNameAlreadyExists()
			{
				const string featureName = "1234";
				var mockFeature = new Mock<IFeature>();
				mockFeature
					.Setup(x => x.GetName())
					.Returns(featureName);

				var subject = new Store(new BrowserInteropStub());
				subject.AddFeature(mockFeature.Object);

				Assert.Throws<ArgumentException>(() =>
				{
					subject.AddFeature(mockFeature.Object);
				});
			}

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
