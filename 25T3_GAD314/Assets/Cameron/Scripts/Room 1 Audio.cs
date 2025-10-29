using UnityEngine;
using System.Collections;

public class Room1Audio : MonoBehaviour
{

    public float soundLevel = 0.5f; // volume level 0-1, 1 = 100% volume
    public AudioClip[] voiceLines;

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
        if (other.gameObject.tag == "Player" && SoundManager.instance.soundSource.isPlaying == false)
        {
            StartCoroutine(playAudioSequentially()); 
        }
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
