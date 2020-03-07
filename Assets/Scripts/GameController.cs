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

    // Start is called before the first frame update
    void Start()
    {

        Instantiate(player, new Vector3(0, 0, 0), Quaternion.Euler(0, 180, 0));
        screenCenter = Camera.main.ViewportToWorldPoint(new Vector2(Screen.width / 2, Screen.height / 2));
    }

    public Vector3 GetNewPosition(Vector3 position)
    {

        return new Vector3(screenCenter.x - position.x, 0, screenCenter.z - position.z);

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


        time += Time.deltaTime;

        if (time >= spawnInterval)
        {
            time = time - spawnInterval;

            InstantiateRandomAsteroid();
        }

    }

    public struct Range
    {
        public float min;
        public float max;
        public float range { get { return max - min + 1; } }
        public Range(float aMin, float aMax)
        {
            min = aMin; max = aMax;
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
