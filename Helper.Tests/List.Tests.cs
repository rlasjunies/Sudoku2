using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace helper.tests
{
    public class ListTests
    {

        private readonly ITestOutputHelper output;

        public ListTests(ITestOutputHelper outputHelper)
        {
            output = outputHelper;
        }

        /**
        * revenir sur ce post pour logger durant les test
        * http://grbd.github.io/posts/2017/01/25/xunit-unit-tests-and-logging-csproj/
        */

        [Fact]
        public void List_should_have_shuffle_method()
        {

            // Arrange
            var list = new List<int>() { 1, 2, 3, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };
            
            // Act
            list.Shuffle_knuthfisheryates2();

            // Assert
            var expectedFinalList = new List<int>() { 1, 2, 3, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };
            output.WriteLine($"expected:{expectedFinalList} - current: {list}");
            Assert.NotEqual(expectedFinalList, list);
        }
    }
}
