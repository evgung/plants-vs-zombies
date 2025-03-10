using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject currentPlant;
    public Sprite currentPlantSprite;
    public int currentPlantPrice;
    public PlantSlot currentPlantSlot;

    public LayerMask gridMask;
    public LayerMask sunMask;
    private Transform parentForPlants;
    public SunsManager sunsManager;
    private AudioSource audioSource;

    public void TakePlant(PlantSlot plantSlot)
    {
        currentPlantSlot = plantSlot;
        currentPlant = plantSlot?.plantObject;
        currentPlantSprite = plantSlot?.plantSprite;
        currentPlantPrice = (plantSlot != null) ? plantSlot.price : 0;
    }

    private void Start()
    {
        parentForPlants = GameObject.Find("RuntimeObjects/Plants").transform;
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    private void LateUpdate()
    {
        if (currentPlant != null && Input.GetMouseButtonDown(1))
        {
            TakePlant(null);
            return;
        }

        RaycastHit2D cellHit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, gridMask);

        if (currentPlant != null && cellHit.collider != null && !cellHit.collider.GetComponent<Cell>().hasPlant)
        {
            cellHit.collider.GetComponent<Cell>().SetSprite(currentPlantSprite);

            if (Input.GetMouseButtonDown(0))
            {
                Plant(cellHit.collider.gameObject);
            }
        }

        RaycastHit2D sunHit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, sunMask);

        if (sunHit.collider != null && Input.GetMouseButtonDown(0))
        {
            sunsManager.CollectSun();
            Destroy(sunHit.collider.gameObject);
        }
    }

    private void Plant(GameObject hit)
    {
        GameObject plant = Instantiate(currentPlant, hit.transform.position, Quaternion.identity, parentForPlants);
        plant.GetComponent<Plant>().SetCell(hit.GetComponent<Cell>());
        sunsManager.BuyPlant(currentPlantPrice);
        currentPlantSlot.BuyPlant();
        TakePlant(null);
        audioSource.Play();
    }
}
