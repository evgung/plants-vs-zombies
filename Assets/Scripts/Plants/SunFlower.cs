using UnityEngine;

public class SunFlower : MonoBehaviour
{
    public float cooldown;
    public float timeToFirstSpawn;
    public GameObject sunPrefab;
    private Transform parentForSuns;

    private void Start()
    {
        parentForSuns = GameObject.Find("RuntimeObjects/Suns").transform;
        InvokeRepeating("SpawnSun", timeToFirstSpawn, cooldown);
    }

    private void SpawnSun()
    {
        Sun sun = Instantiate(sunPrefab, parentForSuns).GetComponent<Sun>();
        sun.transform.position = transform.GetChild(0).position;
        sun.dropYPosition = transform.GetChild(0).position.y - 0.5f;
    }
}
