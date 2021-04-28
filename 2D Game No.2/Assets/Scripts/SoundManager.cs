using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioDefinition
{
    public string name;
    public AudioClip clip;
    public float cooldownTime = 0f;
}

public class SoundManager : Manager<SoundManager>
{
    public AudioDefinition[] audioClips;

    public Dictionary<string, float> audioTimeCounter;

    private GameObject player;

    private void Start()
    {
        InitializeDictionary();
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PlaySound("Audio_PlayerHit1", player.transform.position);
        }
    }

    private void InitializeDictionary()
    {
        audioTimeCounter = new Dictionary<string, float>();
        foreach (AudioDefinition audio in audioClips)
        {
            audioTimeCounter.Add(audio.name, 0f);
        }
    }

    public void PlaySound(string name)
    {
        AudioDefinition audio = GetAudioDefinition(name);
        if (audio == null) return;
        if (!CanPlaySound(name)) return;

        GameObject soundOject = new GameObject();
        AudioSource audioSource = soundOject.AddComponent<AudioSource>();
        audioSource.clip = audio.clip;

        // TODO: set up AudioSource properties like is loop or not, volume,...
        audioSource.Play();

        Object.Destroy(soundOject, audio.clip.length);
    }

    public void PlaySound(string name, Vector3 _position)
    {
        AudioDefinition audio = GetAudioDefinition(name);
        if (audio == null) return;
        if (!CanPlaySound(name)) return;

        GameObject soundOject = new GameObject();
        soundOject.transform.position = _position;
        AudioSource audioSource = soundOject.AddComponent<AudioSource>();
        audioSource.clip = audio.clip;

        // TODO: set up AudioSource properties like is loop or not, volume,...
        audioSource.Play();

        Object.Destroy(soundOject, audio.clip.length);
    }

    private bool CanPlaySound(string name)
    {
        if (audioTimeCounter.ContainsKey(name))
        {
            if(Time.time > GetAudioDefinition(name).cooldownTime + audioTimeCounter[name])
            {
                audioTimeCounter[name] = Time.time;
                return true;
            }
            return false;
        }
        return false;
    }

    private AudioDefinition GetAudioDefinition(string _name)
    {
        foreach (AudioDefinition audioClip in audioClips) 
        {
            if(audioClip.name == _name)
            {
                return audioClip;
            }
        }

        Debug.LogError("Cannot find clip with name: " + _name);
        return null;
    }
}
