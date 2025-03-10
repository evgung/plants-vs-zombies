using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public ZombieTypeProbability[] types;
    private List<ZombieType> zombiesList = new List<ZombieType>();

    public GameObject zombiePrefab;
    public int timeToFirstSpawn;
    public int timeBetweenSpawns;
    public int decreaseFrequency;
    private Transform parentForZombies;
    private AudioSource audioSource;

    public int maxZombieCount;
    public int zombiesSpawned;
    public Slider progressBar;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        parentForZombies = GameObject.Find("RuntimeObjects/Zombies").transform;
        
        StartCoroutine(SpawnZombie());
        Invoke(nameof(FirstWaveSound), timeToFirstSpawn);
        
        if (decreaseFrequency > 0)
            InvokeRepeating(nameof(DecreaseTimeBetweenSpawns), decreaseFrequency, decreaseFrequency);

        foreach (var zombie in types)
        {
            for (int i = 0; i < zombie.probability; i++)
            {
                zombiesList.Add(zombie.type);
            }
        }

        progressBar.maxValue = maxZombieCount;
    }

    private void Update()
    {
        progressBar.value = zombiesSpawned;
        if (zombiesSpawned == maxZombieCount && parentForZombies?.childCount == 0)
        {
            GameObject.Find("GameManager").GetComponent<LevelManager>().Win();
        }
    }

    private void FirstWaveSound()
    {
        audioSource.Play();
    }

    private void DecreaseTimeBetweenSpawns()
    {
        timeBetweenSpawns--;

        if (timeBetweenSpawns == 3)
        {
            CancelInvoke(nameof(DecreaseTimeBetweenSpawns));
        }
    }

    private IEnumerator SpawnZombie()
    {
        yield return new WaitForSeconds(timeToFirstSpawn);

        while (true) 
        {
            if (zombiesSpawned == maxZombieCount)
                yield break;

            int i = Random.Range(0, spawnPoints.Length);
            GameObject zombie = Instantiate(zombiePrefab, spawnPoints[i].position, Quaternion.identity, parentForZombies);
            zombie.GetComponent<Zombie>().type = zombiesList[Random.Range(0, zombiesList.Count)];
            zombiesSpawned++; 
            
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }

}

[System.Serializable]
public class ZombieTypeProbability
{
    public ZombieType type;
    public int probability;
}
