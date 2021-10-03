using System;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Xunit;

namespace helper.tests
{
    public class ArrayTests
    {
        [Fact]
        public void Array_should_have_stringify_method()
        {

            // Arrange
            var arr = new int[] {1,2,3};
            // Act
            arr.ClonePush(1);

            // Assert
            var expectedFinalArr = new int[] {1,2,3,1}; 
            // Assert.Equal(expectedFinalArr, arr);
            Assert.Equal(expectedFinalArr.ToString(), arr.ToString());
        }

        [Fact]
        public void find_should_be_true_if_predicate_is_confirm()
        {
            // Arrange
            var arr = new int[] {1,2,3};

            // Act
            var result = arr.Find<int>( val => val == 2);

            // Assert
            Assert.True(result,"Arr.Find method should be true if predicated is ok");
        }

        [Fact]
        public void find_should_be_false_if_predicate_is_confirmed()
        {
            // Arrange
            var arr = new int[] {1,2,3};

            // Act
            var result = arr.Find<int>( val => val == 4);

            // Assert
            Assert.False(result,"Arr.Find method should be false if predicated is ko");
        }
    }
}
