using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameController : MonoBehaviour
{

    public GameObject player;
    public GameObject asteroid;

    float maxRange = 20f;
    float minRange = 10f;
    float maximumScale = 10f;
    float minimumScale = 5f;
    float spawnInterval = 5f;
    float time = 0.0f;
    public Vector3 screenCenter;
    public float teleportingCooldown = 0.5f;
    private float teleportingTimer;

    void Start()
    {

        player = Instantiate(player, new Vector3(0, 0, 0), Quaternion.Euler(0, 180, 0));
        screenCenter = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width / 2, Screen.height / 2));

    }

    public Vector3 GetNewPosition(Vector3 position)
    {

        return new Vector3(screenCenter.x - position.x, 0, screenCenter.z - position.z);

    }

    bool FindPlayer()
    {

        Collider[] colliders = player.GetComponents<Collider>();

        Collider collider;

        if (colliders[0].isTrigger)
        {
            collider = colliders[0];
        }
        else
        {
            collider = colliders[1];
        }


        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        if (GeometryUtility.TestPlanesAABB(planes, collider.bounds))
            return true;
        else
            return false;

    }

    void InstantiateRandomAsteroid()
    {


        bool targetPending = true;

        float spawnX = 0;
        float spawnZ = 0;

        while (targetPending)
        {

            float minimumZ = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).z;
            float maximumZ = Camera.main.ViewportToWorldPoint(new Vector2(0, Screen.height)).z;


            float minimumX = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).x;
            float maximumX = Camera.main.ViewportToWorldPoint(new Vector2(0, Screen.height)).x;

            if (UnityEngine.Random.value > 0.5f)
            {

                Range[] rangesX = new Range[] { new Range(minimumX - maxRange, minimumX - minRange), new Range(maximumX + minRange, maximumX + maxRange) };
                spawnX = RandomValueFromRanges(rangesX);
                spawnZ = UnityEngine.Random.Range(minimumZ - maxRange, maximumZ + maxRange);
            }
            else
            {

                Range[] rangesZ = new Range[] { new Range(minimumZ - maxRange, minimumZ - minRange), new Range(maximumZ + minRange, maximumZ + maxRange) };
                spawnX = UnityEngine.Random.Range(minimumX - maxRange, maximumX + maxRange);
                spawnZ = RandomValueFromRanges(rangesZ);
            }

            // Avoiding spawning 2 asteroids ont op of each other
            Collider[] colliders = Physics.OverlapBox(new Vector3(spawnX, 0, spawnZ), new Vector3(1, 1, 1));

            targetPending = colliders.Length > 0;

        }

        GameObject asteroidObject = Instantiate(asteroid, new Vector3(spawnX, 0, spawnZ), Quaternion.Euler(0, 0, 0));
        float scale = UnityEngine.Random.Range(minimumScale, maximumScale);

        asteroidObject.transform.localScale = new Vector3(scale, scale, scale);

    }

    // Update is called once per frame
    void Update()
    {

        if (!FindPlayer())
        {

            player.transform.position = GetNewPosition(player.transform.position);

        }


        time += Time.deltaTime;

        if (time >= spawnInterval)
        {
            time = time - spawnInterval;

            InstantiateRandomAsteroid();
        }

    }

    public static float RandomValueFromRanges(Range[] ranges)
    {
        if (ranges.Length == 0)
            return 0;
        float count = 0;
        foreach (Range r in ranges)
            count += r.range;
        float sel = UnityEngine.Random.Range(0, count);
        foreach (Range r in ranges)
        {
            if (sel < r.range)
            {
                return r.min + sel;
            }
            sel -= r.range;
        }
        throw new Exception("This should never happen");
    }
}
