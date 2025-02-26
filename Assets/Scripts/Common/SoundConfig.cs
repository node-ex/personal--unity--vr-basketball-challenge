using UnityEngine;

[System.Serializable]
public class SoundConfig
{
    public string name;
    public AudioClip clip;

    [Range(0, 1)]
    public float volume = 1;

    [Range(-3, 3)]
    public float pitch = 1;

    public bool loop = false;
    public bool playOnAwake = false;
    public AudioSource source;

    public SoundConfig()
    {
        volume = 1;
        pitch = 1;
        loop = false;
    }
}
