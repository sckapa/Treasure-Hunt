using UnityEngine;

public class MudTrap : MonoBehaviour
{
    [SerializeField]
    private float moveSpeedSlowFactor = 2f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameManager.instance.thirdPersonController.MoveSpeed /= moveSpeedSlowFactor;
            GameManager.instance.thirdPersonController.SprintSpeed /= moveSpeedSlowFactor;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameManager.instance.thirdPersonController.MoveSpeed *= moveSpeedSlowFactor;
            GameManager.instance.thirdPersonController.SprintSpeed *= moveSpeedSlowFactor;
        }
    }
}