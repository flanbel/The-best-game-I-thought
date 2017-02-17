using UnityEngine;
using System.Collections;
[RequireComponent(typeof(AudioSource))]
public class PlaySound : MonoBehaviour {
    AudioSource Audio;
    

    void PlayOneShot(AudioClip clip)
    {
        //ないなら取得
        if (!Audio)
            Audio = GetComponent<AudioSource>();
        Audio.PlayOneShot(clip);
    }
}
