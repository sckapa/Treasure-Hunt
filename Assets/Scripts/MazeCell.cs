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

        topWall = true;
        leftWall = true;
    }
}
