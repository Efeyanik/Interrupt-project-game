using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    public RockMover rockMover;


    public GameObject rockPrefab; // Oluþturulacak göktaþý (Prefab)
    public float spawnRate = 1f;  // Kaç saniyede bir çýksýn?
    private float nextSpawnTime = 0f;

    public float minY = -3.5f;
    public float maxY = 3.5f;




    void Update()
    {
        //Time.time kullanmadým çünkü oyun ilk sahneye dönse bile time.time 0 olmuyor.
        if(Time.timeSinceLevelLoad >= nextSpawnTime && Time.timeSinceLevelLoad < 5f)
        {
            spawnRock();
            nextSpawnTime = Time.timeSinceLevelLoad + spawnRate;
        }
        else if(Time.timeSinceLevelLoad >= nextSpawnTime && Time.timeSinceLevelLoad > 5f && Time.timeSinceLevelLoad < 10f)
        {
            spawnRock();
            spawnRate = 0.7f;
            nextSpawnTime = Time.timeSinceLevelLoad + spawnRate;
            rockMover.speed = 12f;

        }
        else if (Time.timeSinceLevelLoad >= nextSpawnTime && Time.timeSinceLevelLoad > 10f)
        {
            spawnRock();
            spawnRate = .3f;
            nextSpawnTime = Time.timeSinceLevelLoad + spawnRate;
            rockMover.speed = 14f;
        }
    }

    void spawnRock()
    {
        float RandomY = Random.Range(minY, maxY);
        Vector2 spawnPosition = new Vector2(13, RandomY);
        Instantiate(rockPrefab, spawnPosition, Quaternion.identity);
    }
    


}
