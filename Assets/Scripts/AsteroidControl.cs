using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidControl : MonoBehaviour
{
    // Start is called before the first frame update
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

    // Update is called once per frame
    void Update()
    {

    }
}
