using UnityEngine;

public class NearElevator : MonoBehaviour
{
    public delegate void NearElevatorDelegate();
    public static event NearElevatorDelegate playerNearElevator;
    public static event NearElevatorDelegate playerFarFromElevator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerNearElevator?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerFarFromElevator?.Invoke();
        }
    }
}
