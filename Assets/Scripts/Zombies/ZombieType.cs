using UnityEngine;

[CreateAssetMenu(fileName="NewZombieType", menuName="Zombie")]
public class ZombieType : ScriptableObject
{
    public float speed;
    public int damage;
    public int health;
    public float eatCooldown = 1f;
    public Sprite sprite;
    public float distance = 0f;
    public AudioClip[] hitClips;
}
