
public class AdjacencyList
{
    private LinkedList<Tuple<int, int>>[] adjacencyList;

    public AdjacencyList(int vertices)
    {
        adjacencyList = new LinkedList<Tuple<int, int>>[vertices];
 
        for (int i = 0; i < adjacencyList.Length; ++i)
        {
            adjacencyList[i] = new LinkedList<Tuple<int, int>>();
        }
    }
    }
}

public class AdjacencyMatrix
{
    
}