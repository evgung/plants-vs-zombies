using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlantSlot : MonoBehaviour
{
    public Sprite plantSprite;
    public GameObject plantObject;
    public Image icon;
    public Image background;
    public int price;
    public TextMeshProUGUI priceText;
    public Slider reloadSlider;

    private GameManager gameManager;
    private Button button;
    private bool isReloading;
    private float reloadTime;
    private float currentReloadTime;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        button = GetComponent<Button>();
        button.onClick.AddListener(TakePlant);
        reloadTime = plantObject.GetComponent<Plant>().reloadTime;
        SetReady();
    }

    private void TakePlant()
    {
        gameManager.TakePlant(this);
    }

    public void BuyPlant()
    {
        reloadSlider.value = 1;
        isReloading = true;
        Invoke(nameof(SetReady), reloadTime);
    }

    private void Update()
    {
        if (isReloading)
        {
            currentReloadTime += Time.deltaTime;
            reloadSlider.value = 1 - (currentReloadTime / reloadTime);
        }

        if (gameManager.GetComponent<GameManager>().sunsManager.suns < price 
            || isReloading)
        {
            SetActive(false);
        }
        else
        {
            SetActive(true);
        }
    }

    private void SetActive(bool isActive)
    {
        button.enabled = isActive;
        icon.color = (isActive) ? Color.white : Color.gray;
        background.color = (isActive) ? Color.white : Color.gray;
    }

    private void SetReady()
    {
        isReloading = false;
        reloadSlider.value = 0;
        currentReloadTime = 0;
    }

    private void OnValidate()
    {
        icon.sprite = plantSprite;
        icon.enabled = true;
        priceText.text = price.ToString();
    }
}
