using UnityEngine;





// dont use this script - purely example




public class TestSound : MonoBehaviour
{
    public float soundLevel = 0.5f; // volume level 0-1, 1 = 100% volume
    public AudioClip testClip; // any audio clip


    void Start()
    {
        SoundManager.instance.PlaySound(testClip, soundLevel); // how to play sound, put wherever u want
    }
}
