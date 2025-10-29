using UnityEngine;
using System.Collections;

public class HPPickupAudioPlayer : MonoBehaviour
{
    public float soundLevel = 0.5f; // volume level 0-1, 1 = 100% volume
    public AudioClip[] voiceLines;

    public void ManualTrigger(int startingNumber)
    {
        StartCoroutine(playAudioSequentially(startingNumber));
    }

    IEnumerator playAudioSequentially(int startingLine)
    {
        yield return null;

        for (int i = startingLine; i < voiceLines.Length; i++)
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
