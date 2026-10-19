using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MazeGenerator", menuName = "Scriptable Objects/MazeGenerator")]
public class MazeGenerator : ScriptableObject
{
    [Range(5, 50)]
    public int mazeWidth = 5;
    [Range(5, 50)]
    public int mazeHeight = 5;

    public float cellSize = 5f;

    public int startX;  // Position where the algo starts
    public int startY;  // 0,0 = bottom left corner of the maze

    [SerializeField]
    private GameObject mazeCellPrefab;

    private GameObject mazeParent;

    private MazeCell[,] maze;

    private Vector2Int currentCell;

    public void GenerateMaze()
    {
        ClearMaze();
        mazeParent = new GameObject("Maze");

        GetMaze();

        for (int x = 0; x < mazeWidth; x++)
        {
            for (int y = 0; y < mazeHeight; y++)
            {
                GameObject newCell = Instantiate(mazeCellPrefab, new Vector3((float)x * cellSize, 0f, (float)y * cellSize), Quaternion.identity, mazeParent.transform);

                MazeCellObject mazeCell = newCell.GetComponent<MazeCellObject>();

                bool top = maze[x, y].topWall;
                bool left = maze[x, y].leftWall;

                bool bottom = false;
                bool right = false;

                if (x == mazeWidth - 1)
                {
                    right = true;
                }

                if (y == 0)
                {
                    bottom = true;
                }

                mazeCell.Init(top, bottom, left, right);
            }
        }
    }

    public void ClearMaze()
    {
        if (mazeParent == null)
        {
            mazeParent = GameObject.Find("Maze");
        }

        if (mazeParent != null)
        {
            DestroyImmediate(mazeParent);
        }
    }

    public void GetMaze()
    {
        maze = new MazeCell[mazeWidth, mazeHeight];

        for (int x = 0; x < mazeWidth; x++)
        {
            for (int y = 0; y < mazeHeight; y++)
            {
                maze[x, y] = new MazeCell(x, y);
            }
        }

        CarvePath(startX, startY);
    }

    private List<Directions> GetRandomDirections()
    {
        List<Directions> directions = new List<Directions> { Directions.Left, Directions.Right, Directions.Up, Directions.Down };

        List<Directions> randomDirections = new();

        while (directions.Count > 0)
        {
            int random = UnityEngine.Random.Range(0, directions.Count);
            randomDirections.Add(directions[random]);
            directions.RemoveAt(random);
        }

        return randomDirections;
    }

    private bool IsCellValid(int x, int y)
    {
        if (x < 0 || y < 0 || x > mazeWidth - 1 || y > mazeHeight - 1 || maze[x, y].visited)
        {
            return false;
        }

        return true;
    }

    private Vector2Int CheckNeighbours()
    {
        List<Directions> randomDirections = GetRandomDirections();

        for (int i = 0; i < randomDirections.Count; i++)
        {
            Vector2Int neighbour = currentCell;

            switch (randomDirections[i])
            {
                case Directions.Left:
                    neighbour.x--;
                    break;
                case Directions.Right:
                    neighbour.x++;
                    break;
                case Directions.Up:
                    neighbour.y++;
                    break;
                case Directions.Down:
                    neighbour.y--;
                    break;
            }

            if (IsCellValid(neighbour.x, neighbour.y))
            {
                return neighbour;
            }
        }

        // This means we've reached a dead end
        return currentCell;
    }

    private void BreakWalls(Vector2Int primaryCell, Vector2Int secondaryCell)
    {
        if (primaryCell.x > secondaryCell.x) // Primary cell is to the right of secondary cell
        {
            // Disable primary cell's left wall
            maze[primaryCell.x, primaryCell.y].leftWall = false;
        }
        else if (primaryCell.x < secondaryCell.x) // Primary cell is to the left of secondary cell
        {
            // Disable secondary cell's left wall
            maze[secondaryCell.x, secondaryCell.y].leftWall = false;
        }
        else if (primaryCell.y < secondaryCell.y) // Primary cell is below secondary cell
        {
            // Disable primary cell's top wall
            maze[primaryCell.x, primaryCell.y].topWall = false;
        }
        else if (primaryCell.y > secondaryCell.y) // Primary cell is on top of secondary cell
        {
            // Disable secondary cell's top wall
            maze[secondaryCell.x, secondaryCell.y].topWall = false;
        }
    }

    private void CarvePath(int x, int y)
    {
        if (!IsCellValid(x, y))
        {
            x = 0;
            y = 0;
            Debug.LogWarning("Start position is invalid, setting it to (0,0)");
        }

        currentCell = new Vector2Int(x, y);

        // List to backtrack the path
        List<Vector2Int> path = new();

        bool deadEnd = false;
        while (!deadEnd)
        {
            Vector2Int nextCell = CheckNeighbours();

            // If we hit a dead end
            if (currentCell == nextCell)
            {
                for (int i = path.Count - 1; i >= 0; i--)
                {
                    currentCell = path[i];
                    path.RemoveAt(i);
                    nextCell = CheckNeighbours();

                    if (nextCell != currentCell)
                    {
                        break;
                    }
                }

                if (nextCell == currentCell)
                {
                    deadEnd = true;
                }
            }
            else
            {
                BreakWalls(currentCell, nextCell);
                maze[currentCell.x, currentCell.y].visited = true;
                currentCell = nextCell;
                path.Add(currentCell);
            }
        }
    }

    public Vector2 GetRandomCellPosInMaze()
    {
        var pos = new Vector2((float)UnityEngine.Random.Range(0, mazeHeight - 1) * cellSize, (float)UnityEngine.Random.Range(0, mazeWidth - 1) * cellSize);
        return pos;
    }

    public Vector2Int GetRandomCellInMaze()
    {
        var cell = new Vector2Int(UnityEngine.Random.Range(0, mazeHeight - 1), UnityEngine.Random.Range(0, mazeWidth - 1));
        return cell;
    }

    // Gets a random cell outside radius
    public Vector2 GetRandomCellPosInMaze(Vector2Int center, int radius)
    {
        List<Directions> randomDirections = GetRandomDirections();
        int x = -1;
        int y = -1;

        for (int i = 0; i < randomDirections.Count; i++)
        {
            if (x == -1 && randomDirections[i] == Directions.Right && center.x + radius < mazeWidth)
            {
                x = UnityEngine.Random.Range(center.x + radius, mazeWidth - 1);
            }
            else if (x == -1 && randomDirections[i] == Directions.Left && center.x - radius >= 0)
            {
                x = UnityEngine.Random.Range(0, center.x - radius);
            }

            if (y == -1 && randomDirections[i] == Directions.Up && center.y + radius < mazeHeight)
            {
                y = UnityEngine.Random.Range(center.y + radius, mazeHeight - 1);
            }
            else if (y == -1 && randomDirections[i] == Directions.Down && center.y - radius >= 0)
            {
                y = UnityEngine.Random.Range(0, center.y - radius);
            }

            if (x != -1 && y != -1)
            {
                Vector2 pos = new Vector2(x * cellSize, y * cellSize);
                return pos;
            }
        }

        Debug.LogWarning("Couldn't find valid cell in given constraints. Returning random cell instead.");
        return GetRandomCellPosInMaze();
    }
}

public enum Directions
{
    Left,
    Right,
    Up,
    Down
}