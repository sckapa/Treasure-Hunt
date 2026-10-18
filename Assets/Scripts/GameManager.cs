using StarterAssets;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public ThirdPersonController thirdPersonController;

    [HideInInspector]
    public List<GameObject> enemies;

    [SerializeField]
    private int enemyCount;
    [SerializeField]
    private GameObject enemyPrefab;
    public Transform enemyParent;
    
    [SerializeField]
    private int landmineCount;
    [SerializeField]
    private GameObject landminePrefab;
    
    [SerializeField]
    private int fogTrapCount;
    [SerializeField]
    private GameObject fogTrapPrefab;
    
    [SerializeField]
    private int mudTrapCount;
    [SerializeField]
    private GameObject mudTrapPrefab;

    [SerializeField]
    private Transform trapParent;

    private MazeGenerator mazeGenerator;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        mazeGenerator = FindFirstObjectByType<MazeGenerator>();
    }

    private void Start()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            Vector2 pos = mazeGenerator.GetRandomCellInMaze();
            Vector3 spawnPos = new Vector3(pos.x, 0, pos.y);
            enemies.Add(Instantiate(enemyPrefab, spawnPos, Quaternion.identity, enemyParent));
        }

        for (int i = 0; i < landmineCount; i++)
        {
            Vector2 pos = mazeGenerator.GetRandomCellInMaze();
            Vector3 spawnPos = new Vector3(pos.x, 0.15f, pos.y);
            Instantiate(landminePrefab, spawnPos, Quaternion.identity, trapParent);
        }

        for (int i = 0; i < fogTrapCount; i++)
        {
            Vector2 pos = mazeGenerator.GetRandomCellInMaze();
            Vector3 spawnPos = new Vector3(pos.x, 2.5f, pos.y);
            Instantiate(fogTrapPrefab, spawnPos, Quaternion.identity, trapParent);
        }

        for (int i = 0; i < mudTrapCount; i++)
        {
            Vector2 pos = mazeGenerator.GetRandomCellInMaze();
            Vector3 spawnPos = new Vector3(pos.x, 0, pos.y);
            Instantiate(mudTrapPrefab, spawnPos, Quaternion.identity, trapParent);
        }
    }
}
