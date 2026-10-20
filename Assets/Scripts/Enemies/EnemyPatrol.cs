using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    [HideInInspector]
    public Transform target;

    private NavMeshAgent navMeshAgent;
    private MazeGenerator mazeGenerator;

    private void Start()
    {
        // Create a destination point
        target = new GameObject(transform.name + "NavMesh Destination Point").transform;
        target.parent = GameManager.instance.enemyParent;

        mazeGenerator = GameManager.instance.mazeGenerator;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        // Enemy has reached destination
        if (navMeshAgent.remainingDistance == 0)
        {
            // Get new destination
            var pos = mazeGenerator.GetRandomCellPosInMaze();
            target.position = new Vector3(pos.x, 0, pos.y);

            // Start travelling
            navMeshAgent.destination = target.position;
        }
    }
}