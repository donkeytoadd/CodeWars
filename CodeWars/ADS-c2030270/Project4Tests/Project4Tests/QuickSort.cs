namespace Project4Tests
{

    public class QuickSort
    {
        //quick sort algorithm based on the pseudocode given to us by Zairul
        //takes integer array/dataset, as well as two other integer values that represent the values to be compared
        public static void PerformQuickSort(int[] dataset, int low, int high)
        {
            //low = 0;
            //high = n - 1;
            
            //if statement that checks if the 'low' value is lower than the 'high' value
            //if true then that means we keep performing our sort, using recursion to call the PerformQuickSort function again
            if (low < high)
            {
                var pi = Partition(dataset, low, high);
                PerformQuickSort(dataset, low, pi - 1);
                PerformQuickSort(dataset, pi + 1, high);
            }

        }

        //swap function used with the Partition function
        //takes integer dataset/array as well as two other values from the dataset to be swapped
        private static void Swap(int[] dataset, int i, int j)
        {
            (dataset[i], dataset[j]) = (dataset[j], dataset[i]);
        }

        //Partition function that is used to determine the current pivot point of the sort
        private static int Partition(int[] dataset, int low, int high)
        {
            //low = 0;
            //int high = n - 1;

            //sets our pivot
            var pivot = dataset[high];
            int i = low - 1;
            
            //iterates as long as our 'low' value is smaller than our 'high' value
            for (var j = low; j < high; j++)
            {
                //checks if dataset[low]/dataset[j] is smaller than our pivot/dataset[high]
                //if true then increment i by one and call our Swap function to perform the swap needed
                if (dataset[j] < pivot)
                {
                    i++;
                    Swap(dataset, i, j);
                }

            }
            
            //call Swap function one last time
            Swap(dataset, i + 1, high);

            //return integer value that is used in the PerformQuickSort function
            return i + 1;

        }
    }
}