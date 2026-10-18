using UnityEngine;

public class MazeCell
{
    public bool visited;
    public int x, y;

    public bool topWall;
    public bool leftWall;

    public Vector2Int position
    {
        get
        {
            return new Vector2Int(x, y);
        }
    }

    public MazeCell(int x, int y)
    {
        this.x = x;
        this.y = y;

        // We only need 2 flags because left wall of some cell will be its neighbor's right wall
        // and top wall of some cell will be its neighbor's bottom wall
        topWall = true;
        leftWall = true;
    }
}
