using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoAsteroidController : MonoBehaviour
{

    void Start()
    {

        GetComponent<Rigidbody>().AddForce(-transform.right * 100f);

        // Vector3 torque;
        // torque.x = Random.Range(-200, 200);
        // torque.y = Random.Range(-200, 200);
        // torque.z = Random.Range(-200, 200);

        // GetComponent<Rigidbody>().AddTorque(torque);

    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
