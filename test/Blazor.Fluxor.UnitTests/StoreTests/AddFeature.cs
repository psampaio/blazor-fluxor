using Blazor.Fluxor.UnitTests.SupportFiles;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
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

				var subject = new Store(BrowserInteropStub.Create());
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

				var subject = new Store(BrowserInteropStub.Create());
				subject.AddFeature(mockFeature.Object);

				Assert.Throws<ArgumentException>(() =>
				{
					subject.AddFeature(mockFeature.Object);
				});
			}

		}
	}
}
