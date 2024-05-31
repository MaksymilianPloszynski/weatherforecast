namespace InterviewConsoleApp;

// Problem: Given a matrix of M x N, find all of the islands.
// Definition: An island is any collection of land/"1" cells connected with its neighbors (all 8 surrounding cells, vertical/horizontal/diagonal)
// Assumption: The matrix is surrounded by water/"0" cells, i.e. no islands wrap
public static class FindTheIslands
{
    public static readonly int[,] OtherMap =
        {{0, 0, 1, 1, 0, 1, 0, 1},
        {0, 0, 1, 0, 0, 1, 1, 0},
        {1, 0, 0, 0, 0, 0, 0, 0},
        {1, 1, 0, 1, 0, 1, 0, 1},
        {0, 1, 0, 0, 0, 1, 0, 0},
        {0, 0, 0, 1, 0, 1, 0, 0}};
    
    public static void PrintAnswer()
    {
        Console.WriteLine($"{CountIslands(OtherMap)} Islands Found!");
    }

    public static int CountIslands(int[,] map)
    {
        var islandCount = 0;
        
        int rows = map.GetLength(0);
        var cols = map.GetLength(1);

        bool[,] visited = new bool[rows, cols];
        
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                var number = OtherMap[i, j];

                if (number == 1 && !visited[i,j])
                {
                    islandCount++;
                    
                    int x 
                    
                    
                    
                    
                }

                visited[i, j] = true;
            }
        }
        

        
        // Solve

        return islandCount;
    }
}