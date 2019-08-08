using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource[] music;
    public AudioSource[] sfx;

    public static AudioManager instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlaySfx(int position)
    {
        if (position < sfx.Length)
        {
            sfx[position].Play();
        }
    }

    public void PlayMusic(int position)
    {
        if (!music[position].isPlaying)
        {
            StopMusic();

            if (position < music.Length)
            {
                music[position].Play();
            }
        }
    }

    public void StopMusic()
    {
        for (int i = 0; i < music.Length; i++)
        {
            music[i].Stop();
        }
    }
}
