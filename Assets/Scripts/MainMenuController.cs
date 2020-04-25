using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public float maxRange = 20f;
    public float minRange = 10f;
    public GameObject asteroid;

    public float maximumScale = 10f;
    public float minimumScale = 5f;

    public float spawnInterval = 5f;
    float time = 0.0f;
    private float minimumY;
    private float maximumY;
    private float minimumX;
    private float maximumX;

    void Start()
    {

        this.minimumY = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, -Camera.main.transform.position.z)).y;
        this.maximumY = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, -Camera.main.transform.position.z)).y;
        this.minimumX = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, -Camera.main.transform.position.z)).x;
        this.maximumX = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, -Camera.main.transform.position.z)).x;

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

    void InstantiateRandomAsteroid()
    {


        bool targetPending = true;

        float spawnX = 0;
        float spawnY = 0;

        while (targetPending)
        {

            spawnX = UnityEngine.Random.Range(maximumX + minRange, maximumX + maxRange);
            spawnY = UnityEngine.Random.Range(minimumY, maximumY);

            // Avoiding spawning 2 asteroids ont op of each other
            Collider[] colliders = Physics.OverlapBox(new Vector3(spawnX, spawnY, 0), new Vector3(1, 1, 1));

            targetPending = colliders.Length > 0;

        }

        GameObject asteroidObject = Instantiate(asteroid, new Vector3(spawnX, spawnY, 0), Quaternion.Euler(0, 0, 0));
        float scale = UnityEngine.Random.Range(minimumScale, maximumScale);

        asteroidObject.transform.localScale = new Vector3(scale, scale, scale);

        asteroidObject.GetComponent<Rigidbody>().AddForce(-asteroidObject.transform.right * 100f);

    }

    public void StartGame()
    {

        SceneManager.LoadScene("GameScene");

    }

    public void QuitGame()
    {

        Application.Quit();

    }
}
