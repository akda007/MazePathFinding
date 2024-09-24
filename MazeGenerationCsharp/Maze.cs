using System.Security.Cryptography.X509Certificates;

class Maze {
    static public int[,] GenerateMaze(int height, int width) {
        int[,] grid = new int[height, width];
        
        for (int i = 0; i < height; i++) {
            for (int j = 0; j < width; j++) {
                grid[i, j] = 1;
            }
        }

        Random rd = new Random();
        int startX = rd.Next(height);
        int startY = rd.Next(width);
        grid[startX, startY] = 0; 

        List<(int x, int y)> walls = new List<(int x, int y)>();
        AddWalls(startX, startY, walls, grid);

        while (walls.Count > 0) {
            int wallIndex = rd.Next(walls.Count);
            var wall = walls[wallIndex];
            walls.RemoveAt(wallIndex);
            
            int wallX = wall.x;
            int wallY = wall.y;

            int neighbors = 0;
            foreach (var (dx, dy) in new (int dx, int dy)[] { (1, 0), (0, 1), (-1, 0), (0, -1) }) {
                int nx = wallX + dx;
                int ny = wallY + dy;

                if (nx >= 0 && nx < height && ny >= 0 && ny < width)
                {
                    if (grid[nx, ny] == 0) neighbors++;
                }
            }

            if (neighbors == 1) {
                grid[wallX, wallY] = 0;
                AddWalls(wallX, wallY, walls, grid);
            }
        }

        return grid;
    }

    static public void RemoveRandomWalls(int[,] grid, int count) {
        (int height, int width) = (grid.GetLength(0), grid.GetLength(1));

        Random rd = new Random();
 
        do {
            (int x, int y) = (rd.Next(0, height), rd.Next(0, width));

            if (grid[x, y] == 0)
                continue;

            grid[x, y] = 0;
            count--;
        } while (count > 0);
    }

    static public void ExportToText(int[,] grid, string filename, List<GridPoint> path) {
        (int height, int width) = (grid.GetLength(0), grid.GetLength(1));
        
        string text = "";

        for (int x = 0; x < height; x++) {
            for (int y = 0; y < width; y++) {
                text += grid[x, y];
            }
            text += "\r\n";
        }

        foreach (var node in path) {
            text += $"({node.x}, {node.y})\n";
        }

        File.WriteAllText(filename, text);
    }
    static public void PrintMaze(int[,] grid) {
        for (int x = 0; x < grid.GetLength(0); x++) {
            for (int y = 0; y < grid.GetLength(1); y++) {
                Console.Write(grid[x, y] == 1 ? "#" : " "); 
            }
            Console.WriteLine();
        }
    }

    static public void AddWalls(int x, int y, List<(int x, int y)> walls, int[,] grid) {
        foreach (var (dx, dy) in new (int dx, int dy)[] { (1, 0), (0, 1), (-1, 0), (0, -1) }) {
            int wallX = x + dx;
            int wallY = y + dy;

            if (wallX >= 0 && wallX < grid.GetLength(0) 
                && wallY >= 0 && wallY < grid.GetLength(1) 
                && grid[wallX, wallY] == 1) {

                walls.Add((wallX, wallY)); 
            }
        }
    }
}