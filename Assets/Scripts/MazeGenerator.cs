using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem.iOS;

public class MazeGenerator : MonoBehaviour
{
    [Range(5, 500)]
    public int mazeWidth = 5;
    [Range(5, 500)]
    public int mazeHeight = 5;

    public int startX;  // Position where the algo starts
    public int startY;  // 0,0 = bottom left corner of the maze

    private MazeCell[,] maze;

    private Vector2Int currentCell;

    public MazeCell[,] GetMaze()
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

        return maze;
    }

    private List<Directions> GetRandomDirection()
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
        List<Directions> randomDirections = GetRandomDirection();

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
}

public enum Directions
{
    Left,
    Right,
    Up,
    Down
}