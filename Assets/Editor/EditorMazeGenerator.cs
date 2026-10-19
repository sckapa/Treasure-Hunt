using System.Reflection;
using UnityEditor;
using UnityEngine;

public class EditorMazeGenerator : EditorWindow
{
    [MenuItem("Tools/Maze Generator")]
    public static void ShowWindow()
    {
        GetWindow(typeof(EditorMazeGenerator));
    }

    public MazeGenerator mazeGenerator;

    private void Awake()
    {
        mazeGenerator = AssetDatabase.LoadAssetAtPath<MazeGenerator>("Assets/Data/MazeGenerator.asset");
    }

    private void OnGUI()
    {
        if (mazeGenerator == null)
        {
            mazeGenerator = (MazeGenerator)EditorGUILayout.ObjectField(mazeGenerator, typeof(ScriptableObject), false);
        }
        else
        {
            // Maze width slider
            FieldInfo mazeWidthField = mazeGenerator.GetType().GetField(nameof(mazeGenerator.mazeWidth), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            RangeAttribute mazeWidthRangeAttr = mazeWidthField.GetCustomAttribute<RangeAttribute>();

            EditorGUILayout.LabelField("Maze Width");
            mazeGenerator.mazeWidth = EditorGUILayout.IntSlider(mazeGenerator.mazeWidth, (int)mazeWidthRangeAttr.min, (int)mazeWidthRangeAttr.max);

            // Maze height slider
            FieldInfo mazeHeightField = mazeGenerator.GetType().GetField(nameof(mazeGenerator.mazeHeight), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            RangeAttribute mazeHeightRangeAttr = mazeHeightField.GetCustomAttribute<RangeAttribute>();

            EditorGUILayout.LabelField("Maze Height");
            mazeGenerator.mazeHeight = EditorGUILayout.IntSlider(mazeGenerator.mazeHeight, (int)mazeHeightRangeAttr.min, (int)mazeHeightRangeAttr.max);

            // Cell size
            EditorGUILayout.LabelField("Cell Size");
            mazeGenerator.cellSize = EditorGUILayout.FloatField(mazeGenerator.cellSize);

            // Start pos for algo
            EditorGUILayout.LabelField("Algorithm Start X");
            mazeGenerator.startX = EditorGUILayout.IntField(mazeGenerator.startX);
            EditorGUILayout.LabelField("Algorithm Start Y");
            mazeGenerator.startY = EditorGUILayout.IntField(mazeGenerator.startY);
        }

        if (GUILayout.Button("Generate Maze"))
        {
            mazeGenerator.ClearMaze();
            mazeGenerator.GenerateMaze();
        }

        if (GUILayout.Button("Clear Maze"))
        {
            mazeGenerator.ClearMaze();
        }
    }
}
