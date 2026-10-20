using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    private static PlayerStats instance;

    [SerializeField]
    private int maxHealth;

    [SerializeField]
    private Slider healthSlider;

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

    public void OnRestart()
    {
        health = maxHealth;
        healthSlider.value = 1;
    }

    public void DealDamage(float damage)
    {
        health -= damage;
        healthSlider.value = Mathf.InverseLerp(0f, maxHealth, health);

        if (health <= 0f)
        {
            GameManager.instance.GameOver();
        }
        else if (health <= 20f)
        {
            healthSlider.fillRect.GetComponent<Image>().color = Color.red;
        }
    }

    public void Heal(float heal)
    {
        health += heal;
        healthSlider.value = Mathf.InverseLerp(0f, maxHealth, health);

        if (health > 20f)
        {
            healthSlider.fillRect.GetComponent<Image>().color = Color.white;
        }

        if (health > 100f)
        {
            health = 100f;
        }
    }
}
