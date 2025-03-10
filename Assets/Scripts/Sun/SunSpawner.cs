using UnityEngine;

public class SunSpawner : MonoBehaviour
{
    public GameObject sunPrefab;
    public int timeToFirstSpawn;
    public int timeBetweenSpawns;
    private Transform parentForSuns;

    private void Start()
    {
        parentForSuns = GameObject.Find("RuntimeObjects/Suns").transform;
        InvokeRepeating("SpawnSun", timeToFirstSpawn, timeBetweenSpawns);
    }

    private void SpawnSun()
    {
        Sun sun = Instantiate(sunPrefab, parentForSuns).GetComponent<Sun>();
        sun.transform.position = new Vector3(Random.Range(-8.3f, 3.3f), 6.1f);
        sun.dropYPosition = Random.Range(-3.2f, 3.2f);
    }
}
