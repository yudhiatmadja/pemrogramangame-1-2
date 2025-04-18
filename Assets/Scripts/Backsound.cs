using UnityEngine;

public class Backsound : MonoBehaviour
{

    [SerializeField] private AudioClip backgroundMusic; 
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (backgroundMusic != null)
        {
            audioSource.clip = backgroundMusic;
            audioSource.loop = true; 
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("Background music not assigned!");
        }
    }

    
}
