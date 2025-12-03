using UnityEngine;
using System.Collections;

public class DefaultNarratorScript : MonoBehaviour
{

    public float soundLevel = 0.5f; // volume level 0-1, 1 = 100% volume
    public AudioClip[] voiceLines;

    bool hasTriggered;

    IEnumerator narration;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        narration = playAudioSequentially(); 
    }

    // Update is called once per frame
    void Update()
    {
        if(SoundManager.instance.interruptAudio == true)
        {
            ManualStop(); 
            SoundManager.instance.interruptAudio = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && hasTriggered == false)
        {
            SoundManager.instance.interruptAudio = true;
            SoundManager.instance.StopClip();

            StartCoroutine(narration); 
            hasTriggered = true;
        }
    }

    public void ManualTrigger()
    {
        StartCoroutine(narration);
    }

    public void ManualStop()
    {
        StopCoroutine(narration);
        Debug.Log("stopping coroutine now");
    }

    IEnumerator playAudioSequentially()
    {
        //SoundManager.instance.interruptAudio = false;

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
