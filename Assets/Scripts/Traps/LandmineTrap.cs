using UnityEngine;

public class LandmineTrap : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            // Game over
            Debug.Log("Game Over");
        }
    }
}
