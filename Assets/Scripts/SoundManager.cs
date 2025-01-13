using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    void Awake()
    {
        Instance = this;
    }
    AudioSource audioSource;
    [SerializeField] AudioClip pellet0;
    [SerializeField] AudioClip pellet1;

    bool altPellet;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayPellet()
    {
        AudioClip clip;
        altPellet = !altPellet;
        if (altPellet)
        {
            clip = pellet1;
        }
        else
        {
            clip = pellet0;
        }

        audioSource.PlayOneShot(clip);
    }

}
