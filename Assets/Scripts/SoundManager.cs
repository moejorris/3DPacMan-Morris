//////////////////////////////////////////////
//Assignment/Lab/Project: 3D Pac-Man Part 1
//Name: Joe Morris
//Section: SGD285.4173
//Instructor: Ven Lewis
//Date: 1/14/2025
/////////////////////////////////////////////

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
    [SerializeField] AudioClip levelStart;
    [SerializeField] AudioClip levelRestart;
    [SerializeField] AudioClip die1;
    [SerializeField] AudioClip die2;

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

    public void PlayLevelStart()
    {
        audioSource.PlayOneShot(levelStart);
    }

    public void PlayLevelRestart()
    {
        audioSource.PlayOneShot(levelRestart);
    }

    public void PlayDeath()
    {
        audioSource.PlayOneShot(die1);
        Invoke("DeathBloop", 2.9f);
    }

    void DeathBloop()
    {
        audioSource.PlayOneShot(die2);
    }

}
