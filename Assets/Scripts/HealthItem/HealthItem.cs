using UnityEngine;

public class HealthItem : MonoBehaviour
{
    [SerializeField]
    private float healthIncrease = 20f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameManager.instance.Heal(healthIncrease);
            Destroy(transform.parent.gameObject);
        }
    }
}
