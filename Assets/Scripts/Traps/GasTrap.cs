using UnityEngine;

public class GasTrap : MonoBehaviour
{
    [SerializeField]
    private float tickDuration = 1f;
    [SerializeField]
    private float damagePerTick = 5.5f;

    private float tick;

    private void OnTriggerEnter(Collider other)
    {
        tick = tickDuration;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            tick -= Time.deltaTime;

            if (tick <= 0)
            {
                // Damage player
                GameManager.instance.DealDamage(damagePerTick);

                tick = tickDuration;
            }
        }
    }
}