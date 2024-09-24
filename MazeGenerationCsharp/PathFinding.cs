class PathFinding {
    public static GridPoint GeneratePoint(int[,] grid) {
        (int h, int w) = (grid.GetLength(0), grid.GetLength(1));

        Random rd = new Random();

        var point = new GridPoint(rd.Next(0, h), rd.Next(0, w));
        
        while (grid[point.x, point.y] == 1) {
            point = new GridPoint(rd.Next(0, h), rd.Next(0, w));    
        }

        return point;
    }

    public static GridPoint GeneratePointMin(int[,] grid, int minX, int minY) {
        (int h, int w) = (grid.GetLength(0), grid.GetLength(1));

        Random rd = new Random();

        var point = new GridPoint(rd.Next(minX, h), rd.Next(minY, w));

        while (grid[point.x, point.y] == 1) {
            point = new GridPoint(rd.Next(minX, h), rd.Next(minY, w));
        }

        return point;
    }

    public static GridPoint GeneratePointMax(int[,] grid, int maxX, int maxY) {
        (int h, int w) = (grid.GetLength(0), grid.GetLength(1));

        Random rd = new Random();

        var point = new GridPoint(rd.Next(0, maxX), rd.Next(0, maxY));

        while (grid[point.x, point.y] == 1) {
            point = new GridPoint(rd.Next(0, maxX), rd.Next(0, maxY));            
        }

        return point;
    }
}