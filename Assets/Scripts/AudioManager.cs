using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public SoundConfig[] SoundConfigs;

    private Dictionary<string, SoundConfig> _soundConfigMap;

    public void Play(string name)
    {
        if (!_soundConfigMap.TryGetValue(name, out var soundConfig))
        {
            PrintSoundNotFoundWarningToConsole(name);
            return;
        }
        ConfigureAudioSource(soundConfig);

        soundConfig.source.Play();
    }

    public void Stop(string name)
    {
        if (!_soundConfigMap.TryGetValue(name, out var soundConfig))
        {
            PrintSoundNotFoundWarningToConsole(name);
            return;
        }

        soundConfig.source.Stop();
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        InitializeSoundConfigMap();
        InitializeAudioSources();
    }

    private void InitializeSoundConfigMap()
    {
        _soundConfigMap = new Dictionary<string, SoundConfig>();
        foreach (var soundConfig in SoundConfigs)
        {
            _soundConfigMap[soundConfig.name] = soundConfig;
        }
    }

    private void InitializeAudioSources()
    {
        foreach (var soundConfig in SoundConfigs)
        {
            ConfigureAudioSource(soundConfig);
        }
    }

    private void ConfigureAudioSource(SoundConfig soundConfig)
    {
        if (soundConfig.source == null)
        {
            soundConfig.source = gameObject.AddComponent<AudioSource>();
        }

        AudioSource source = soundConfig.source;
        source.clip = soundConfig.clip;
        source.volume = soundConfig.volume;
        source.pitch = soundConfig.pitch;
        source.loop = soundConfig.loop;
        source.playOnAwake = soundConfig.playOnAwake;

        if (soundConfig.playOnAwake)
        {
            Debug.Log("Playing sound on awake: " + soundConfig.name);
            source.Play();
        }
    }

    private void PrintSoundNotFoundWarningToConsole(string name)
    {
        Debug.LogWarning("Sound: " + name + " not found");
    }
}
