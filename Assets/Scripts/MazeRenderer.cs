using UnityEngine;

public class MazeRenderer : MonoBehaviour
{
    [SerializeField]
    private MazeGenerator mazeGenerator;
    [SerializeField]
    private GameObject mazeCellPrefab;
    [SerializeField]
    private float cellSize = 5f;

    private void Start()
    {
        var maze = mazeGenerator.GetMaze();

        for (int x = 0; x < mazeGenerator.mazeWidth; x++)
        {
            for (int y = 0; y < mazeGenerator.mazeHeight; y++)
            {
                GameObject newCell = Instantiate(mazeCellPrefab, new Vector3(x * cellSize, 0, y * cellSize), Quaternion.identity, transform);

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
}
