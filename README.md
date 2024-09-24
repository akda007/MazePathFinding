# Maze Generation and Pathfinding Visualization

## Overview

This project combines maze generation using Prim's algorithm (with modifications) and pathfinding using the A* algorithm implemented in C#. The generated maze and the calculated path between points are then visualized using Python.

## Project Structure

```
/MazeProject
│
├── /CSharpMazeGenerator
│   ├── MazeGenerator.cs        # C# code for maze generation and pathfinding
│   ├── maze.txt                # Output file containing maze and path data
│   └── ...
│
├── /PythonVisualizer
│   ├── visualize.py            # Python code for visualizing the maze and path
│   └── ...
└── README.md                   # This README file
```

## Prerequisites

### C# Environment
- .NET Core SDK (version 3.1 or higher)

### Python Environment
- Python (version 3.6 or higher)
- Required libraries:
  ```bash
  pip install matplotlib watchdog numpy
  ```

## C# Maze Generator

### Overview
The C# component is responsible for generating the maze and finding the path using Prim's algorithm and A* algorithm. The results are saved in `maze.txt`, which will be read by the Python visualizer.

### Usage
1. Open the `CSharpMazeGenerator` project in your C# IDE.
2. Build and run the project. The maze will be generated and saved in `maze.txt`.

### Maze File Format
The output file `maze.txt` contains the maze structure and the path coordinates:
```
0 1 1 0 0
0 0 1 0 1
1 0 0 0 1
0 0 1 1 0
1 0 0 0 0
(0, 0)
(1, 0)
(1, 1)
...
```
- `0` indicates open space, and `1` indicates walls.
- Path coordinates are listed at the end, enclosed in parentheses.

## Python Visualizer

### Overview
The Python visualizer reads the `maze.txt` file, displays the maze, and highlights the path found between the start and goal points.

### Usage
1. Ensure the `maze.txt` file is located in the `PythonVisualizer` directory.
2. Run the visualizer:
   ```bash
   python visualize.py
   ```

### Functionality
- The program monitors `maze.txt` for changes and updates the visualization accordingly.
- It uses Matplotlib to render the maze and path.

### Code Explanation
- The `read_maze_file` function reads the maze and path from the file.
- The `draw_maze` function visualizes the maze and highlights the path.
- The `MazeFileHandler` class listens for changes in the `maze.txt` file to refresh the display.

## Conclusion

This project demonstrates the integration of C# and Python for maze generation and visualization, providing insights into algorithm implementation and cross-language interaction. For further improvements, consider adding command-line arguments for customization, enhancing the maze generation algorithm, or improving the visualization features.
