using UnityEngine;

public class ElevatorManager : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();

        NearElevator.playerNearElevator += PlayerNearElevator;
        NearElevator.playerFarFromElevator += PlayerFarFromElevator;

        InsideElevator.playerInsideElevator += PlayerInsideElevator;
        InsideElevator.playerOutsideElevator += PlayerOutsideElevator;
    }

    private void OnDestroy()
    {
        NearElevator.playerNearElevator -= PlayerNearElevator;
        NearElevator.playerFarFromElevator -= PlayerFarFromElevator;

        InsideElevator.playerInsideElevator -= PlayerInsideElevator;
        InsideElevator.playerOutsideElevator -= PlayerOutsideElevator;
    }

    private void PlayerNearElevator()
    {
        GetComponent<AudioSource>().Play();
        animator.SetTrigger("DoorOpen");
    }

    private void PlayerFarFromElevator()
    {
        animator.SetTrigger("DoorClose");
    }

    private void PlayerInsideElevator()
    {
        animator.SetTrigger("DoorClose");
    }

    private void PlayerOutsideElevator()
    {
        GetComponent<AudioSource>().Play();
        animator.SetTrigger("DoorOpen");
    }
}
