using TMPro;
using UnityEngine;

public class SunsManager : MonoBehaviour
{
    public int suns;
    public TextMeshProUGUI sunsText;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        sunsText.text = suns.ToString();
    }

    public void BuyPlant(int price)
    {
        suns -= price;
    }

    public void CollectSun()
    {
        suns += 25;
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.Play();
    }
}
