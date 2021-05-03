using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource audioSourceBackgroundMusic;
    public AudioSource audioSourceSFX;
    public AudioClip[] backgroundMusic;
    // Start is called before the first frame update
    void Start()
    {

        AudioClip backgroundMusicMain = backgroundMusic[0];
        audioSourceBackgroundMusic.clip = backgroundMusicMain;
        audioSourceBackgroundMusic.loop = true;
        audioSourceBackgroundMusic.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        
        audioSourceSFX.clip = clip;
        audioSourceSFX.Play();
    }
}
