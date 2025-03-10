using UnityEngine;

public class Zombie : MonoBehaviour
{
    private float speed;
    private int damage;
    private int health;
    private Plant attackedPlant;
    private bool isEating;
    private bool isSlownDown = false;
    private AudioSource hitAndGroanAudioSource;
    private AudioSource eatingAudioSource;

    public LayerMask layerMask;
    public ZombieType type;
    public float volume;
    public float timeBetweenGroans;
    public AudioClip[] groanSound;

    private void Start()
    {
        health = type.health;
        damage = type.damage;
        speed = type.speed;
        eatingAudioSource = GetComponent<AudioSource>();
        hitAndGroanAudioSource = transform.GetChild(0).GetComponent<AudioSource>();
        GetComponent<SpriteRenderer>().sprite = type.sprite;
        InvokeRepeating("Groan", 5, timeBetweenGroans);
    }

    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, type.distance, layerMask);

        if (hit.collider != null)
        {
            if (isEating) return;
            isEating = true;
            attackedPlant = hit.collider.GetComponent<Plant>();
            InvokeRepeating("Eat", 0, type.eatCooldown);
            eatingAudioSource.Play();
        }
        else
        {
            isEating = false;
            CancelInvoke("Eat");
            eatingAudioSource.Stop();
        }

        if (!isEating)
        {
            transform.Translate(-speed * Time.fixedDeltaTime, 0, 0);
        }
    }

    public void Groan()
    {
        hitAndGroanAudioSource.PlayOneShot(groanSound[Random.Range(0, groanSound.Length)], volume);
    }

    public void Eat()
    {
        attackedPlant.GetDamage(damage);
    }

    public void GetDamage(int damage)
    {
        hitAndGroanAudioSource.PlayOneShot(type.hitClips[Random.Range(0, type.hitClips.Length)], volume);
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void SlowDown()
    {
        CancelInvoke("ResetSlowDown");
        Invoke("ResetSlowDown", 10);

        if (isSlownDown) return;

        speed /= 2;
        isSlownDown = true;
        GetComponent<SpriteRenderer>().color = new Color32(39, 131, 221, 255);
    }

    private void ResetSlowDown()
    {
        speed *= 2;
        isSlownDown = false;
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}
