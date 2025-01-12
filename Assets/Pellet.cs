using UnityEngine;

public class Pellet : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            GameManager.Instance.GotPellet();
            SoundManager.Instance.PlayPellet();
            Destroy(gameObject);
        }
    }
}
