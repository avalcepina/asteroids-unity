using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public GameObject player;
    public GameObject asteroid;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(player, new Vector3(0, 0, 0), Quaternion.Euler(0, 180, 0));
        Instantiate(asteroid, new Vector3(16, 0, 0), Quaternion.Euler(0, 0, 0));
        Instantiate(asteroid, new Vector3(-16, 0, 0), Quaternion.Euler(0, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
