using UnityEngine;

public class PeaShooter : MonoBehaviour
{
    public float cooldown;
    public GameObject peaPrefab;
    private Transform parentForPeas;
    public LayerMask layerMask;
    private AudioSource audioSource;
    public AudioClip[] shootClips;
    public float shootVolume;

    private void Start()
    {
        parentForPeas = GameObject.Find("RuntimeObjects/Peas").transform;
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 15, layerMask);
        if (hit.collider != null)
        {
            if (!IsInvoking("Shoot"))
            {
                InvokeRepeating("Shoot", 1f, cooldown);
            }
        }
        else
        {
            CancelInvoke("Shoot");
        }
    }

    private void Shoot()
    {
        audioSource.PlayOneShot(shootClips[Random.Range(0, shootClips.Length)], shootVolume);
        Instantiate(peaPrefab, transform.GetChild(0).position, Quaternion.identity, parentForPeas);
    }
}
