using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class AudioManager : Singleton<AudioManager>
{
    [Header("Audio Source pool")]
    [SerializeField] private GameObject _audioSourceObj;
    [SerializeField] private int _amountAudioSource = 30;
    private List<AudioSource> _pooledAudioSource;

    protected override void OnAwake()
    {
        this.transform.position = Vector3.zero;
        CreateAudioSourcePool();
    }

    public void PlayAudio(AudioClip audioClip)
    {
        AudioSource pooledAudioSource = GetPooledAudioSource();

        pooledAudioSource.gameObject.SetActive(true);
        pooledAudioSource.PlayOneShot(audioClip);

        StartCoroutine(DeactiveAudioSource(pooledAudioSource, audioClip.length));
    }

    public void PlayAudio(AudioClip audioClip, float pitchChangeRange)
    {
        AudioSource pooledAudioSource = GetPooledAudioSource();

        pooledAudioSource.pitch += (Random.Range(-pitchChangeRange, +pitchChangeRange));
        pooledAudioSource.pitch = Mathf.Clamp(pooledAudioSource.pitch, -3f, 3f);

        pooledAudioSource.gameObject.SetActive(true);
        pooledAudioSource.PlayOneShot(audioClip);

        StartCoroutine(DeactiveAudioSource(pooledAudioSource, audioClip.length));
    }

    #region Audio Source Pool
    private void CreateAudioSourcePool()
    {
        _pooledAudioSource = new List<AudioSource>();
        for (int i = 0; i < _amountAudioSource; i++)
        {
            GameObject obj = Instantiate(_audioSourceObj,this.transform);
            obj.SetActive(false);
            _pooledAudioSource.Add(obj.GetComponent<AudioSource>());
        }
    }

    private AudioSource GetPooledAudioSource()
    {
        for (int i = 0; i < _pooledAudioSource.Count; i++)
        {
            if (!_pooledAudioSource[i].gameObject.activeInHierarchy)
            {
                return _pooledAudioSource[i];
            }
        }
        return null;
    }

    private IEnumerator DeactiveAudioSource(AudioSource audioSource3D, float timeToDeactive)
    {
        yield return new WaitForSeconds(timeToDeactive);
        audioSource3D.pitch = 1;
        audioSource3D.maxDistance = 10;
        audioSource3D.minDistance = 15;
        audioSource3D.volume = 1;
        audioSource3D.gameObject.SetActive(false);
    }
    #endregion 
}