using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Manager<SoundManager>
{
    public AudioClip sceneTheme;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PlayerSound();
        }
    }

    public void PlayerSound()
    {
        GameObject soundOject = new GameObject();
        AudioSource audioSource = soundOject.AddComponent<AudioSource>();
        audioSource.PlayOneShot(sceneTheme);
    }
}
