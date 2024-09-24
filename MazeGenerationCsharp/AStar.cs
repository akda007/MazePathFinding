class AStar : IPathFindingAlgorithm {
    private List<GridPoint> GetNeighbors(GridPoint point, int[,] grid) {
        var neighbors = new List<GridPoint>();

        int[] dx = { 0, 1, 0, -1 };
        int[] dy = { 1, 0, -1, 0 };

        for (int i = 0; i < dx.Length; i++) {
            int newX = point.x + dx[i];
            int newY = point.y + dy[i];
            
            if (newX >= 0 && newX < grid.GetLength(0) && newY >= 0 && newY < grid.GetLength(1) && grid[newX, newY] == 0)
                neighbors.Add(new GridPoint(newX, newY));

        }

        return neighbors;
    }

    private float Heuristic(GridPoint a, GridPoint b) {
        return Math.Abs(a.x - a.x) + Math.Abs(a.y - b.y);
    }

   public List<GridPoint> FindPath(int[,] grid, GridPoint start, GridPoint goal)
{
    (int h, int w) = (grid.GetLength(0), grid.GetLength(1));

    var openList = new List<Node>();
    var closedList = new HashSet<Node>(); // Use HashSet for faster lookups

    var startNode = new Node(start) { GCost = 0, HCost = Heuristic(start, goal) };
    openList.Add(startNode);

    while (openList.Count > 0)
    {
        Node currentNode = openList[0];

        foreach (var node in openList)
        {
            if (node.FCost < currentNode.FCost || (node.FCost == currentNode.FCost && node.HCost < currentNode.HCost))
                currentNode = node;
        }

        // If we reached the goal
        if (currentNode.Position.Equals(goal))
            return RetracePath(currentNode);

        openList.Remove(currentNode);
        closedList.Add(currentNode); // Move current node to closed list

        foreach (var neighborPos in GetNeighbors(currentNode.Position, grid))
        {
            // Check if this neighbor is already in the closed list
            var neighborNode = new Node(neighborPos);

            if (closedList.Contains(neighborNode))
                continue;

            float newCost = currentNode.GCost + 1; // Assuming cost between nodes is 1

            // Check if the neighbor is in the open list
            var existingNeighborNode = openList.Find(n => n.Position.Equals(neighborNode.Position));

            if (existingNeighborNode == null)
            {
                // New neighbor, initialize costs
                neighborNode.GCost = newCost;
                neighborNode.HCost = Heuristic(neighborPos, goal);
                neighborNode.Parent = currentNode;
                openList.Add(neighborNode);
            }
            else
            {
                // If this path is better, update the existing node
                if (newCost < existingNeighborNode.GCost)
                {
                    existingNeighborNode.GCost = newCost;
                    existingNeighborNode.HCost = Heuristic(neighborPos, goal);
                    existingNeighborNode.Parent = currentNode;
                }
            }
        }
    }

    return new List<GridPoint>(); // Return an empty path if no path is found
}
    

    private List<GridPoint> RetracePath(Node endNode) {
        var path = new List<GridPoint>();

        Node currentNode = endNode;

        while (currentNode != null) {
            path.Add(currentNode.Position);
            
            if (currentNode.Parent is null) {
                // Console.Error.WriteLine("Empty parent!");
                // Environment.Exit(-1);
            }

            currentNode = currentNode.Parent;
        }

        path.Reverse();
        return path;
    }
}