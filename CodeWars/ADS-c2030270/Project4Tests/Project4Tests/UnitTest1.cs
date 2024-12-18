namespace Project4Tests
{
    [TestFixture]
    public class BubbleSort2Tests
    {
        [Test]
        public void PerformBubbleSort_SortsInAscendingOrder()
        {
            // Arrange
            int[] dataset = { 3, 1, 4, 1, 5, 9, 2, 6, 5, 3, 5 };
            int[] expectedSortedArray = { 1, 1, 2, 3, 3, 4, 5, 5, 5, 6, 9 };

            // Act
            BubbleSort2.PerformBubbleSort(dataset);

            // Assert
            Assert.That(dataset, Is.EqualTo(expectedSortedArray));
        }

        [Test]
        public void PerformBubbleSort_WithEmptyArray_ShouldNotThrowException()
        {
            // Arrange
            int[] dataset = new int[0];

            // Act & Assert
            Assert.DoesNotThrow(() => BubbleSort2.PerformBubbleSort(dataset));
        }

        

        [Test]
        public void PerformBubbleSort_SortsInDescendingOrder()
        {
            // Arrange
            int[] dataset = { 5, 4, 3, 2, 1 };
            int[] expectedSortedArray = { 1, 2, 3, 4, 5 };

            // Act
            BubbleSort2.PerformBubbleSort(dataset);

            // Assert
            Assert.That(dataset, Is.EqualTo(expectedSortedArray));
        }

        [Test]
        public void PerformBubbleSort_WithDuplicateValues()
        {
            // Arrange
            int[] dataset = { 3, 1, 4, 1, 5, 9, 2, 6, 5, 3, 5 };
            int[] expectedSortedArray = { 1, 1, 2, 3, 3, 4, 5, 5, 5, 6, 9 };

            // Act
            BubbleSort2.PerformBubbleSort(dataset);

            // Assert
            Assert.That(dataset, Is.EqualTo(expectedSortedArray));
        }

        [Test]
        public void PerformBubbleSort_WithAlreadySortedArray()
        {
            // Arrange
            int[] dataset = { 1, 2, 3, 4, 5 };
            int[] expectedSortedArray = { 1, 2, 3, 4, 5 };

            // Act
            BubbleSort2.PerformBubbleSort(dataset);

            // Assert
            Assert.That(dataset, Is.EqualTo(expectedSortedArray));
        }

        // Add more test cases to cover different scenarios
    }
}
