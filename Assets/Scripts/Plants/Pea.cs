using UnityEngine;

public class Pea : MonoBehaviour
{
    public float speed;
    public int damage;
    public bool isSnowy;

    private void FixedUpdate()
    {
        transform.Translate(speed * Time.fixedDeltaTime, 0, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Zombie"))
        {
            Zombie zombie = collision.gameObject.GetComponent<Zombie>();

            zombie.GetDamage(damage);
            if (isSnowy) zombie.SlowDown();
            Destroy(gameObject);
        }
    }
}
