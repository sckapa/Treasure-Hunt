using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    private Transform target;

    private NavMeshAgent navMeshAgent;
    private MazeGenerator mazeGenerator;

    private void Start()
    {
        target = new GameObject(transform.name + "Navmesh Destination Point").transform;
        mazeGenerator = FindFirstObjectByType<MazeGenerator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        // Enemy has reached destination
        if (navMeshAgent.remainingDistance == 0)
        {
            // Get new destination
            var pos = mazeGenerator.GetRandomCellInMaze();
            target.position = new Vector3(pos.x, 0, pos.y);

            // Start travelling
            navMeshAgent.destination = target.position;
        }
    }
}