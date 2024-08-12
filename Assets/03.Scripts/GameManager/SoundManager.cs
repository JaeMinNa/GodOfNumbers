using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("PlayerAudioSource")]
    [SerializeField] private AudioSource _playerBGMAudioSource;
    [SerializeField] private AudioSource[] _playerSFXAuidoSource;

    //[Header("ETCAudioSource")]
    //[SerializeField] private AudioSource[] _etcSFXAudioSources;

    private Dictionary<string, AudioClip> _bgm;
    private Dictionary<string, AudioClip> _sfx;
    private int _index;
    //[SerializeField] private float _maxDistance = 50f;
    [Range(0f, 1f)] public float StartVolume = 0.1f;

    public void Init()
    {
        // 초기 셋팅
        _bgm = new Dictionary<string, AudioClip>();
        _sfx = new Dictionary<string, AudioClip>();

        _playerBGMAudioSource.loop = true;
        _playerBGMAudioSource.volume = StartVolume;
        
        for (int i = 0; i < _playerSFXAuidoSource.Length; i++)
        {
            _playerSFXAuidoSource[i].playOnAwake = false;
            _playerSFXAuidoSource[i].volume = StartVolume;
        }

        // BGM

        // SFX
        _sfx.Add("DestroyLine", Resources.Load<AudioClip>("Sound/SFX/Block/DestroyLine"));
        _sfx.Add("ClickButton", Resources.Load<AudioClip>("Sound/SFX/UI/ClickButton"));
        _sfx.Add("FailButton", Resources.Load<AudioClip>("Sound/SFX/UI/FailButton"));
        _sfx.Add("SuccessButton", Resources.Load<AudioClip>("Sound/SFX/UI/SuccessButton"));
        _sfx.Add("MissButton", Resources.Load<AudioClip>("Sound/SFX/UI/MissButton"));
        _sfx.Add("ChangeButton", Resources.Load<AudioClip>("Sound/SFX/UI/ChangeButton"));
        _sfx.Add("BuyButton", Resources.Load<AudioClip>("Sound/SFX/UI/BuyButton"));
    }

    // 메모리 해제
    public void Release()
    {

    }

    // 다른 오브젝트에서 출력되는 사운드
    // 2D에서는 Vector2.Distance 사용
    //public void StartSFX(string name, Vector3 position)
    //{
    //    _index = _index % _etcSFXAudioSources.Length;

    //    float distance = Vector3.Distance(position, GameManager.I.PlayerManager.Player.transform.position);
    //    float volume = 1f - (distance / _maxDistance);
    //    _etcSFXAudioSources[_index].volume = Mathf.Clamp01(volume) * StartVolume;
    //    _etcSFXAudioSources[_index].PlayOneShot(_sfx[name]);

    //    _index++;
    //}

    // Player에서 출력되는 사운드
    public void StartSFX(string name)
    {
        _index = _index % _playerSFXAuidoSource.Length;
        _playerSFXAuidoSource[_index].PlayOneShot(_sfx[name]);
        _index++;
    }

    public void StartBGM(string name)
    {
        _playerBGMAudioSource.Stop();
        _playerBGMAudioSource.clip = _bgm[name];
        _playerBGMAudioSource.Play();
    }

    public void StopBGM()
    {
        if (_playerBGMAudioSource != null) _playerBGMAudioSource.Stop();
    }
}