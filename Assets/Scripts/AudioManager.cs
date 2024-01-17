using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager main;

    [Header("References")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    [SerializeField] AudioSource gameMusicSource;
    public AudioClip backgroundMusic;
    public AudioClip shootEffect;
    public AudioClip explosionEffect;
    public AudioClip buttonClickEffect;
    public AudioClip gameMusic;

    private void Awake(){
        main = this;
    }

    void Start()
    {
        playBackgroundMusic();
    }

    public void playBackgroundMusic() {
        musicSource.clip = backgroundMusic; 
        musicSource.Play();
    }

    public void playGameMusic() {
        gameMusicSource.clip = gameMusic; 
        gameMusicSource.Play();
    }

    public void stopBacroundMusic() {
        musicSource.clip = backgroundMusic; 
        musicSource.Stop();
    }

    public void stopGameMusic() {
        gameMusicSource.clip = gameMusic; 
        gameMusicSource.Stop();
    }

    public void PlayShootEffect() {
        SFXSource.PlayOneShot(shootEffect);
    }

    public void PlayExplosionEffect() {
        SFXSource.PlayOneShot(explosionEffect);
    }

    public void PlayButtonClickEffect() {
        SFXSource.PlayOneShot(buttonClickEffect);
    }
}
