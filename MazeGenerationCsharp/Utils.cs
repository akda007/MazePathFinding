public record GridPoint(int x, int y) {}

public class Node
{
    public GridPoint Position { get; }
    public float GCost { get; set; } // Cost from start to this node
    public float HCost { get; set; } // Heuristic cost from this node to the goal
    public float FCost => GCost + HCost; // Total cost
    public Node Parent { get; set; } // Parent node

    public Node(GridPoint position)
    {
        Position = position;
        GCost = float.MaxValue; // Initialize to max value
        HCost = 0;
        Parent = null;
    }

    public override bool Equals(object obj)
    {
        if (obj is Node other)
            return Position.x == other.Position.x && Position.y == other.Position.y;
        return false;
    }

    public override int GetHashCode()
    {
        return (Position.x, Position.y).GetHashCode();
    }
}