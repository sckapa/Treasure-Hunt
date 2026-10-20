using UnityEngine;

public class LandmineTrap : MonoBehaviour
{
    [SerializeField]
    private GameObject explosionEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(Instantiate(explosionEffect, transform.position, Quaternion.identity), 2f);
            GetComponent<AudioSource>().Play();
            GameManager.instance.GameOver();
        }
    }
}
