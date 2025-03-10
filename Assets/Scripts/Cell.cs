using UnityEngine;

public class Cell : MonoBehaviour
{
    public bool hasPlant;
    public Sprite plantSprite;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        spriteRenderer.enabled = false;
    }

    public void SetSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
        spriteRenderer.enabled = true;
    }
}
