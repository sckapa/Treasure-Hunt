using log4net.Util;
using UnityEditor;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class EditorMazeGenerator : EditorWindow
{
    [MenuItem("Tools/Maze Generator")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(EditorMazeGenerator));
    }

    MazeRenderer mazeRenderer;

    private void Awake()
    {
        mazeRenderer = FindAnyObjectByType<MazeRenderer>();
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Generate Maze"))
        {
            mazeRenderer.ClearMaze();
            mazeRenderer.GenerateMaze();
        }

        if (GUILayout.Button("Clear Maze"))
        {
            mazeRenderer.ClearMaze();
        }
    }
}
