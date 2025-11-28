using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager instance;

    public AudioSource soundSource;

    public bool interruptAudio = false; 

    void Awake() // ensure only a single instance of the sound manager exists
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip clip, float soundVolume)
    {
        soundSource.volume = soundVolume; // volume to play out

        soundSource.clip = clip;
        soundSource.Play(); // clip to play
    }

    public void StopClip()
    {
        soundSource.Stop();
        //Debug.Log("stopping clip");
    }
}
