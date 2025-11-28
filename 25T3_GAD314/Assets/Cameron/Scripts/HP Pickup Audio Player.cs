using UnityEngine;
using System.Collections;

public class HPPickupAudioPlayer : MonoBehaviour
{
    public float soundLevel = 0.5f; // volume level 0-1, 1 = 100% volume
    public AudioClip[] voiceLines;

    int firstLine;

    IEnumerator narration;

    void Start()
    {
        narration = playAudioSequentially();
    }

    void Update()
    {
        if (SoundManager.instance.interruptAudio == true)
        {
            ManualStop();
        }
    }

    public void ManualTrigger(int startingNumber)
    {
        firstLine = startingNumber;

        SoundManager.instance.StopClip();
        SoundManager.instance.interruptAudio = true;

        StartCoroutine(narration);
    }

    public void ManualStop()
    {
        StopCoroutine(narration);
    }

    IEnumerator playAudioSequentially()
    {
        yield return null;

        for (int i = firstLine; i < voiceLines.Length; i++)
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
