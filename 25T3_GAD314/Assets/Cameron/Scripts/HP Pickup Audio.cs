using UnityEngine;
using System.Collections;

public class HPPickupAudio : MonoBehaviour
{
    public GameObject audioManager; 
    
    public GameObject room3Audio; 
    int startingLine = 1;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (SoundManager.instance.soundSource.isPlaying == true)
            {
                startingLine = 0;
                Destroy(room3Audio);
            }

            audioManager.gameObject.GetComponent<HPPickupAudioPlayer>().ManualTrigger(startingLine);
        }
    }
}
