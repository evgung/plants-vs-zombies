using UnityEngine;
using UnityEngine.SceneManagement;

public class Defeat : MonoBehaviour
{
    public Animator defeatAnimation;
    private AudioSource audioSource;
    public AudioSource musicPlayer;
    public AudioClip defeatMusic;
    public AudioClip defeatScream;
    private GameObject runtimeObjects;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        runtimeObjects = GameObject.Find("RuntimeObjects");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Zombie")) return;
        
        Destroy(runtimeObjects);

        defeatAnimation.Play("Defeat");
        musicPlayer.Stop();
        audioSource.PlayOneShot(defeatMusic);
        audioSource.PlayOneShot(defeatScream);

        Invoke(nameof(RestartScene), 5);
    }

    private void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
