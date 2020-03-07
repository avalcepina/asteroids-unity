using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehaviour : MonoBehaviour
{
    public float speed;

    public int damage = 40;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {

        Debug.Log("Entering collision");

        HealthController healthController = other.gameObject.GetComponent<HealthController>();

        if (healthController != null)
        {
            healthController.DealDamage(damage);
        }

        GameObject.Destroy(gameObject);

    }
}
