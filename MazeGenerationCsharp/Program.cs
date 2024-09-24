using System;
using System.Collections.Generic;

var grid = Maze.GenerateMaze(2000, 2000);

Maze.RemoveRandomWalls(grid, 2000);

// var start = PathFinding.GeneratePointMax(grid, 20, 20);
// var goal = PathFinding.GeneratePointMin(grid, 80, 80);
var start = PathFinding.GeneratePoint(grid);
var goal = PathFinding.GeneratePoint(grid);

var aStar = new AStar();

var path = aStar.FindPath(grid, start, goal);


// Maze.PrintMaze(grid);
// foreach(var node in path) {
//     Console.WriteLine($"({node.x}:{node.y})");
// }

Maze.ExportToText(grid, @"C:\Users\disrct\Desktop\MazePathFinding\MazeVizualizationPython\maze.txt", path);

