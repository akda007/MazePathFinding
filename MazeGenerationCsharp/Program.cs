using System;
using System.Collections.Generic;

var grid = Maze.GenerateMaze(100, 100);

Maze.RemoveRandomWalls(grid, 500);

// var start = PathFinding.GeneratePointMax(grid, 20, 20);
// var goal = PathFinding.GeneratePointMin(grid, 80, 80);
var start = PathFinding.GeneratePoint(grid);
var goal = PathFinding.GeneratePoint(grid);

var aStar = new AStar();

var path = aStar.FindPath(grid, start, goal);


// Maze.PrintMaze(grid);
foreach(var node in path) {
    Console.WriteLine($"({node.x}:{node.y})");
}

Maze.ExportToText(grid, @"C:\Users\akdag\Desktop\pathFindingVizualization\maze.txt", path);

