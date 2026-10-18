using UnityEngine;

public class GasTrap : MonoBehaviour
{
    [SerializeField]
    private float tickDuration = 1f;

    private float tick;

    private void OnTriggerEnter(Collider other)
    {
        tick = tickDuration;
    }

    private void OnTriggerStay(Collider other)
    {
        tick -= Time.deltaTime;

        if (tick <= 0)
        {
            // Damage player

            tick = tickDuration;
        }
    }
}
