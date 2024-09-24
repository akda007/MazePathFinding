public interface IPathFindingAlgorithm
{
    public List<GridPoint> FindPath(int[,] grid, GridPoint start, GridPoint goal);
}