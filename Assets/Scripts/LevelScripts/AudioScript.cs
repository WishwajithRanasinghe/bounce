using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    [SerializeField] private AudioClip _jump,_collectot,_life,_airUp,_airDown,_gameOver,_winAudio,_reSpwn;
    private AudioSource _audio;
    private void Start()
    {
        _audio = GetComponent<AudioSource>();
    }//Start

    private void Update()
    {
        
    }//Update
    public void Jump()
    {
        if(_audio.isPlaying != true || _audio.clip == null)
        {
            _audio.PlayOneShot(_jump);
        }
    }
    public void Collector()
    {
        if(_audio.isPlaying != true || _audio.clip == null)
        {
            _audio.PlayOneShot(_collectot);
        }

    }
    public void Life()
    {
        if(_audio.isPlaying != true || _audio.clip != _life)
        {
            _audio.PlayOneShot(_life);
        }

    }
    public void AirUp()
    {
        if(_audio.isPlaying != true || _audio.clip != _airUp)
        {
            _audio.PlayOneShot(_airUp);
        }
    }
    public void AirDown()
    {
        if(_audio.isPlaying != true || _audio.clip != _airDown)
        {
            _audio.PlayOneShot(_airDown);
        }
    }
    public void GameOver()
    {
        if(_audio.isPlaying != true || _audio.clip != _gameOver)
        {
            _audio.PlayOneShot(_gameOver);
        }
    }
    public void WinGame()
    {
        if(_audio.isPlaying != true || _audio.clip != _winAudio)
        {
            _audio.PlayOneShot(_winAudio);
        }

    }
    public void ReSpawn()
    {
        if(_audio.isPlaying != true || _audio.clip != _reSpwn)
        {
            _audio.PlayOneShot(_reSpwn);
        }

    }
}//Class
