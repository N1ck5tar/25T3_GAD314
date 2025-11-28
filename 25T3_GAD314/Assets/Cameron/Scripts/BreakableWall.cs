using UnityEngine;
using System.Collections;

public class BreakableWall : MonoBehaviour
{
    public int WallHP;

    public float soundLevel = 0.5f; // volume level 0-1, 1 = 100% volume
    public AudioClip[] voiceLines;

    bool hasTriggered;

    public SpriteRenderer sprite;
    public BoxCollider2D collision; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (WallHP <= 0)
        {
            sprite.enabled = false;
            collision.enabled = false;
        }

    }

    public void TakeDamage(int damage)
    {
        WallHP -= damage;

        if(hasTriggered == false)
        {
            if(SoundManager.instance.soundSource.isPlaying == false)
            {
                SoundManager.instance.StopClip();
                SoundManager.instance.interruptAudio = true;

                StartCoroutine(playAudioSequentially());
            }

            hasTriggered = true;
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
    }
}
