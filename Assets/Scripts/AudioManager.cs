using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager main;

    [Header("References")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    public AudioClip backgroundMusic;
    public AudioClip shootEffect;
    public AudioClip explosionEffect;
    public AudioClip buttonClickEffect;

    private void Awake(){
        main = this;
    }

    void Start()
    {
       musicSource.clip = backgroundMusic; 
       musicSource.Play();
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
