using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource audioSource;
    public AudioClip hurtSound;
    public AudioClip jumpSound;
    public AudioClip healSound;
    public AudioClip winSound;
    public AudioClip loseSound;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void PlaySound(string soundName)
    {
        switch (soundName)
        {
            case "heal":
                audioSource.PlayOneShot(healSound);
                break;
            case "hurt":
                audioSource.PlayOneShot(hurtSound);
                break;
            case "jump":
                audioSource.PlayOneShot(jumpSound);
                break;            
            case "win":
                audioSource.PlayOneShot(winSound);
                break;      
            case "lose":
                audioSource.PlayOneShot(loseSound);
                break;      
            
        }
    }
}
