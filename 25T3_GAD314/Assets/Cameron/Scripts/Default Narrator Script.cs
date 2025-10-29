using UnityEngine;
using System.Collections;

public class DefaultNarratorScript : MonoBehaviour
{

    public float soundLevel = 0.5f; // volume level 0-1, 1 = 100% volume
    public AudioClip[] voiceLines;

    bool hasTriggered;

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
        if (other.gameObject.tag == "Player" && hasTriggered == false && SoundManager.instance.soundSource.isPlaying == false)
        {
            StartCoroutine(playAudioSequentially()); 
            hasTriggered = true;         
        }
    }

    public void ManualTrigger()
    {
        StartCoroutine(playAudioSequentially());
    }

    IEnumerator playAudioSequentially()
    {
        yield return null;

        for (int i = 0; i < voiceLines.Length; i++)
        {
            SoundManager.instance.PlaySound(voiceLines[i], soundLevel);

            while (SoundManager.instance.soundSource.isPlaying)
            {
                yield return null;
            }
        }

        Destroy(gameObject);
    }
}
