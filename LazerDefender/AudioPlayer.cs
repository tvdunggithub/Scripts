using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Chứa các phương thức để chơi nhạc
//AudioPlayer là emptyObject chứa AudioSource(loop và play on awake), đồng thời là singleton
public class AudioPlayer : MonoBehaviour
{
    public AudioSource gameMusic;
    public AudioSource [] audioSFX;
    static AudioPlayer instance;

    void Awake()
    {
        ManageSingleton();
    }

    void ManageSingleton()
    {
        if(instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }


    public void PlayShootingClip()
    {
        audioSFX[0].Play();
    }
    
    public void PlayDamageClip()
    {
        audioSFX[1].Play();
    }

    public void PlayGameMusic()
    {
        gameMusic.Play();
    }

}

