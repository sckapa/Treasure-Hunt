using System.Collections;
using UnityEngine;

public class MudTrap : MonoBehaviour
{
    [SerializeField]
    private float moveSpeedSlowFactor = 2f;
    [SerializeField]
    private float timeBeforeSpeedReset = 1.5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.instance.playerThirdPersonController.MoveSpeed /= moveSpeedSlowFactor;
            GameManager.instance.playerThirdPersonController.SprintSpeed /= moveSpeedSlowFactor;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(ResetMovementSpeed());
        }
    }

    private IEnumerator ResetMovementSpeed()
    {
        yield return new WaitForSeconds(timeBeforeSpeedReset);

        GameManager.instance.playerThirdPersonController.MoveSpeed *= moveSpeedSlowFactor;
        GameManager.instance.playerThirdPersonController.SprintSpeed *= moveSpeedSlowFactor;
    }
}