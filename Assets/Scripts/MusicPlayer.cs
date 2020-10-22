using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicPlayer : MonoBehaviour
{

    [SerializeField] AudioSource backgroundAudio;
    Text text;


    void Awake()
    {
        int numMusicPlayers = FindObjectsOfType<MusicPlayer>().Length;
       
         if (numMusicPlayers > 1)
            Destroy(gameObject);

        else
            DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        UIController.soundControl += SoundController;
    }

    void Start()
    {
        backgroundAudio.Stop();
    }

    public void SoundController()
    {
        Debug.Log("backgroundAudio: "+ backgroundAudio);
        if (backgroundAudio.isPlaying)
            backgroundAudio.Pause();
        else
            backgroundAudio.Play();

    }

    void OnDisable()
    {
        UIController.soundControl -= SoundController;
    }


}
