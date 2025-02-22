using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class PlaySoundsOnTagCollision : MonoBehaviour
{
    [SerializeField] private AudioClip _defaultAudioClip;
    [SerializeField] private TagToAudioClip[] _tagsToAudioClips;

    private AudioSource _audioSource;
    private Dictionary<string, AudioClip> _tagToAudioMap;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        InitializeTagToAudioMap();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_tagToAudioMap.TryGetValue(collision.gameObject.tag, out AudioClip clip))
        {
            _audioSource.PlayOneShot(clip);
            return;
        }

        _audioSource.PlayOneShot(_defaultAudioClip);
    }

    private void InitializeTagToAudioMap()
    {
        _tagToAudioMap = new Dictionary<string, AudioClip>();
        foreach (var mapping in _tagsToAudioClips)
        {
            _tagToAudioMap[mapping.Tag] = mapping.AudioClip;
        }
    }
}
