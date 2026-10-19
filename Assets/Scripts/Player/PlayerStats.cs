using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private static PlayerStats instance;

    [SerializeField]
    private int maxHealth;

    private float health;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        health = maxHealth;
    }

    public void DealDamage(float damage)
    {
        health -= damage;

        if (health <= 0f)
        {
            GameManager.instance.GameOver();
        }
    }

    public void Heal(float heal)
    {
        health += heal;

        if (health > 100f)
        {
            health = 100f;
        }
    }
}
