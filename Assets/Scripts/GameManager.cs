using StarterAssets;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Player")]
    public ThirdPersonController playerThirdPersonController;
    public Animator playerAnimator;

    [SerializeField]
    private PlayerStats playerStats;
    [SerializeField]
    private RagdollController ragdollController;
    [SerializeField]
    private GameObject player;

    [Header("Elevator")]
    [SerializeField]
    private GameObject elevatorPrefab;
    [SerializeField]
    private int elevatorSpawnDistance;
    
    [Header("Healing Items")]
    [SerializeField]
    private int healingItemCount;
    [SerializeField]
    private GameObject healingItemPrefab;
    [SerializeField]
    private Transform healingItemsParent;
    [SerializeField]
    private int healingItemsSpawnDistance;
    
    [Header("Enemies")]
    [SerializeField]
    private int enemyCount;
    [SerializeField]
    private GameObject enemyPrefab;
    public Transform enemyParent;
    [SerializeField]
    private int enemySpawnDistance;

    [Header("Traps")]
    [SerializeField]
    private int landmineCount;
    [SerializeField]
    private int fogTrapCount;
    [SerializeField]
    private int mudTrapCount;

    [SerializeField]
    private GameObject landminePrefab;
    [SerializeField]
    private GameObject fogTrapPrefab;
    [SerializeField]
    private GameObject mudTrapPrefab;

    [SerializeField]
    private Transform trapParent;

    [SerializeField]
    private int trapSpawnDistance;

    [Header("Ragdoll")]
    public GameObject ragdoll;
    
    [Header("Maze Data")]
    public MazeGenerator mazeGenerator;

    [Header("UI")]
    [SerializeField]
    private GameObject gameOverScreen;
    [SerializeField]
    private GameObject gameWonScreen;

    private List<GameObject> enemies = new();
    private List<GameObject> traps = new();
    private List<GameObject> healingItems = new();
    private GameObject elevator;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    private void Init()
    {
        // Spawn player
        Vector2Int playerPos = mazeGenerator.GetRandomCellInMaze();
        Vector3 playerSpawnPos = new Vector3(playerPos.x * mazeGenerator.cellSize, 0.5f, playerPos.y * mazeGenerator.cellSize);
        player.transform.position = playerSpawnPos;

        // Spawn escape route
        {
            Vector2 pos = mazeGenerator.GetRandomCellPosInMaze(playerPos, elevatorSpawnDistance);
            Vector3 spawnPos = new Vector3(pos.x, 0.15f, pos.y);
            elevator = Instantiate(elevatorPrefab, spawnPos, Quaternion.identity);
        }

        // Spawn enemies
        for (int i = 0; i < enemyCount; i++)
        {
            Vector2 pos = mazeGenerator.GetRandomCellPosInMaze(playerPos, enemySpawnDistance);
            Vector3 spawnPos = new Vector3(pos.x, 0, pos.y);
            enemies.Add(Instantiate(enemyPrefab, spawnPos, Quaternion.identity, enemyParent));
        }
        
        // Spawn healing items
        for (int i = 0; i < healingItemCount; i++)
        {
            Vector2 pos = mazeGenerator.GetRandomCellPosInMaze(playerPos, healingItemsSpawnDistance);
            Vector3 spawnPos = new Vector3(pos.x, 0, pos.y);
            healingItems.Add(Instantiate(healingItemPrefab, spawnPos, Quaternion.identity, healingItemsParent));
        }

        // Spawn traps
        for (int i = 0; i < landmineCount; i++)
        {
            Vector2 pos = mazeGenerator.GetRandomCellPosInMaze(playerPos, trapSpawnDistance);
            Vector3 spawnPos = new Vector3(pos.x, 0.15f, pos.y);
            traps.Add(Instantiate(landminePrefab, spawnPos, Quaternion.identity, trapParent));
        }

        for (int i = 0; i < fogTrapCount; i++)
        {
            Vector2 pos = mazeGenerator.GetRandomCellPosInMaze(playerPos, trapSpawnDistance);
            Vector3 spawnPos = new Vector3(pos.x, 2.5f, pos.y);
            traps.Add(Instantiate(fogTrapPrefab, spawnPos, Quaternion.identity, trapParent));
        }

        for (int i = 0; i < mudTrapCount; i++)
        {
            Vector2 pos = mazeGenerator.GetRandomCellPosInMaze(playerPos, trapSpawnDistance);
            Vector3 spawnPos = new Vector3(pos.x, 0, pos.y);
            traps.Add(Instantiate(mudTrapPrefab, spawnPos, Quaternion.identity, trapParent));
        }

        Cursor.lockState = CursorLockMode.Locked;

        playerStats.OnRestart();
        ragdollController.OnRespawn();
        playerThirdPersonController.enabled = true;
        playerThirdPersonController.MoveSpeed = 2f;
        playerThirdPersonController.SprintSpeed = 5.335f;
    }

    public void RestartGame()
    {
        foreach (var go in enemies.ToList())
        {
            enemies.Remove(go);
            Destroy(go);
        }

        foreach (var go in traps.ToList())
        {
            traps.Remove(go);
            Destroy(go);
        }
        
        foreach (var go in healingItems.ToList())
        {
            healingItems.Remove(go);
            Destroy(go);
        }

        if (elevator != null)
        {
            Destroy(elevator);
        }

        Init();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
    public void DealDamage(float damage)
    {
        playerStats.DealDamage(damage);
    }
    
    public void Heal(float heal)
    {
        playerStats.Heal(heal);
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);

        playerThirdPersonController.enabled = false;
        playerAnimator.SetFloat("Speed", 0f);
        Cursor.lockState = CursorLockMode.None;

        ragdollController.OnDeath();
    }

    public void GameWon()
    {
        gameWonScreen.SetActive(true);

        playerThirdPersonController.enabled = false;
        playerAnimator.SetFloat("Speed", 0f);
        Cursor.lockState = CursorLockMode.None;
    }
}
