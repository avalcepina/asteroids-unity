using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float rotationSpeed;
    public float movementSpeed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.W))
            GetComponent<Rigidbody>().AddForce(transform.forward * -movementSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.S))
            GetComponent<Rigidbody>().AddForce(transform.forward * movementSpeed * Time.deltaTime);


        if (Input.GetKey(KeyCode.A))
            transform.Rotate(-Vector3.up * rotationSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.D))
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

    }

}
