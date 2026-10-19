using UnityEngine;

public class InsideElevator : MonoBehaviour
{
    public delegate void InsideElevatorDelegate();
    public static event InsideElevatorDelegate playerInsideElevator;
    public static event InsideElevatorDelegate playerOutsideElevator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInsideElevator?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerOutsideElevator?.Invoke();
        }
    }
}
