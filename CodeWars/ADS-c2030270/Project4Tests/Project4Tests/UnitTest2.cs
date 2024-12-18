namespace Project4Tests
{
    [TestFixture]
    public class QuickSortTests
    {
        [Test]
        public void PerformQuickSort_SortsInAscendingOrder()
        {
            // Arrange
            int[] dataset = { 3, 1, 4, 1, 5, 9, 2, 6, 5, 3, 5 };

            // Act
            QuickSort.PerformQuickSort(dataset, 0, dataset.Length - 1);

            // Assert
            Assert.That(dataset, Is.Ordered);
        }

        [Test]
        public void PerformQuickSort_WithEmptyArray_ShouldNotThrowException()
        {
            // Arrange
            int[] dataset = new int[0];

            // Act & Assert
            Assert.DoesNotThrow(() => QuickSort.PerformQuickSort(dataset, 0, dataset.Length - 1));
        }
        
        [Test]
        public void PerformQuickSort_WithDuplicateValues()
        {
            // Arrange
            int[] dataset = { 3, 1, 4, 1, 5, 9, 2, 6, 5, 3, 5 };
            int[] expectedSortedArray = { 1, 1, 2, 3, 3, 4, 5, 5, 5, 6, 9 };

            // Act
            QuickSort.PerformQuickSort(dataset, 0, dataset.Length - 1);

            // Assert
            Assert.That(dataset, Is.EqualTo(expectedSortedArray));
        }
        

        [Test]
        public void PerformQuickSort_WithAlreadySortedArray()
        {
            // Arrange
            int[] dataset = { 1, 2, 3, 4, 5, 6 };

            // Act
            QuickSort.PerformQuickSort(dataset, 0, dataset.Length - 1);

            // Assert
            Assert.That(dataset, Is.Ordered);
        }

    }
}
