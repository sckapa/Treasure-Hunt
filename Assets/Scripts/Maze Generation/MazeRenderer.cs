using UnityEngine;

public class MazeRenderer : MonoBehaviour
{
    [SerializeField]
    private MazeGenerator mazeGenerator;
    [SerializeField]
    private GameObject mazeCellPrefab;
    [SerializeField]
    private float cellSize = 5f;

    public void GenerateMaze()
    {
        var maze = mazeGenerator.GetMaze();

        for (int x = 0; x < mazeGenerator.mazeWidth; x++)
        {
            for (int y = 0; y < mazeGenerator.mazeHeight; y++)
            {
                GameObject newCell = Instantiate(mazeCellPrefab, new Vector3((float)x * cellSize, 0f, (float)y * cellSize), Quaternion.identity, transform);

                MazeCellObject mazeCell = newCell.GetComponent<MazeCellObject>();

                bool top = maze[x, y].topWall;
                bool left = maze[x, y].leftWall;

                bool bottom = false;
                bool right = false;

                if (x == mazeGenerator.mazeWidth - 1)
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
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }
}
