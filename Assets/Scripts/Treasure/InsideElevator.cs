using UnityEngine;

public class InsideElevator : MonoBehaviour
{
    [SerializeField]
    private float timeBeforeWin = 2f;
    private float timeRemaining;

    public delegate void InsideElevatorDelegate();
    public static event InsideElevatorDelegate playerInsideElevator;
    public static event InsideElevatorDelegate playerOutsideElevator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            timeRemaining = timeBeforeWin;
            playerInsideElevator?.Invoke();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (timeRemaining <= 0f)
            {
                GameManager.instance.GameWon();
            }
            else
            {
                timeRemaining -= Time.deltaTime;
            }
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
