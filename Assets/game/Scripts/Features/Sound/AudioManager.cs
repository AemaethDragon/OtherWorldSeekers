using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public AudioManager instance;

    private bool _loop;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        foreach (Sound gameSound in sounds)
        {
            gameSound.source = gameObject.AddComponent<AudioSource>();
            gameSound.source.clip = gameSound.clip;
            gameSound.source.pitch = 1f;
            gameSound.source.loop = gameSound.loop;
            gameSound.source.volume = 0.20f;
        }
    }
    public void PlayAudio(string name)
    {
        Sound gameSound = Array.Find(sounds, sound => sound.name == name);
        if (gameSound == null)
        {
            return;
        }
        foreach (Sound bgSound in sounds)
        {
            if (name == "gameBackground")
            {
                gameSound.source.volume = 0.14f;
                gameSound.source.pitch = 1f;
            }

            if (name == "mainMenu")
            {
                gameSound.source.volume = 0.08f;
                gameSound.source.pitch = 1f;
            }
        }
        gameSound.source.Play();
    }
    
    public void StopAudio (string name)
    {
        Sound gameSound = Array.Find(sounds, sound => sound.name == name);
        if (gameSound == null)
        {
            return;
        }
        gameSound.source.volume = gameSound.volume * (1f + UnityEngine.Random.Range(-gameSound.volume / 2f, gameSound.volume/ 2f));
        gameSound.source.pitch = gameSound.pitch * (1f + UnityEngine.Random.Range(-gameSound.pitch / 2f, gameSound.pitch / 2f));
        gameSound.source.Stop ();
    }
    
    
    //use this codes to call a specific game sound by the name
    /*Private void Start()
    {
        Start a audio File
        FindObjectOfType<AudioManager>().PlayAudio(("NameOfSound"));
        
        Stop a audio File (in case of loop activated)
        FindObjectOfType<AudioManager>().StopAudio(("NameOfSound"));
    }*/
    
}