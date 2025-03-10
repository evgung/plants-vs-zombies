using UnityEngine;

public class LawnMower : MonoBehaviour
{
    [SerializeField] private float speed;
    private bool isMoving;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Zombie")) return;
        
        if (!isMoving)
        {
            isMoving = true;
            audioSource.Play();
        }

        Destroy(collision.gameObject);
    }

    private void Update()
    {
        if (isMoving)
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }
    }
}
