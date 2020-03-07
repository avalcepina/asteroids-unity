using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{

    public int damage;
    public float damageCooldown = 0.5f;
    public float currentTime;

    void Start()
    {

        transform.LookAt(new Vector3(0, 0, 0));
        GetComponent<Rigidbody>().AddForce(transform.forward * 100f);

        Vector3 torque;
        torque.x = Random.Range(-200, 200);
        torque.y = Random.Range(-200, 200);
        torque.z = Random.Range(-200, 200);

        GetComponent<Rigidbody>().AddTorque(torque);

    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnCollisionStay(Collision other)
    {

        if (other.gameObject.tag == "Player")
        {

            if (currentTime < damageCooldown)
            {

                currentTime += Time.deltaTime;

            }
            else
            {

                currentTime = 0f;

                HealthController healthController = other.gameObject.GetComponent<HealthController>();

                if (healthController != null)
                {
                    healthController.DealDamage(damage);
                }

            }

        }

    }

}
