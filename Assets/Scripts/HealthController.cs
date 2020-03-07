using UnityEngine;

public class HealthController : MonoBehaviour
{

    public int health = 100;
    private int currentHealth = 0;
    public GameObject explosion;
    private bool exploding = false;

    void Start()
    {

        currentHealth = health;

    }

    public void DealDamage(int damage)
    {
        currentHealth = currentHealth - damage;

        if (currentHealth <= 0 && !exploding)
        {
            exploding = true;
            GameObject explosionGo = Instantiate(explosion, transform.position, Quaternion.Euler(0, 0, 0));
            Destroy(gameObject, 0.5f);
            Destroy(explosionGo, 1f);

        }
    }

}