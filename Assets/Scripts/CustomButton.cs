using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Video;

public class CustomButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [Header("References")]
    [SerializeField] private Image img;
    [SerializeField] private Sprite spriteDefault;
    [SerializeField] private Sprite spritePressed;
    [SerializeField] private AudioClip downSound;
    [SerializeField] private AudioClip upSound;
    [SerializeField] private AudioSource source;
    [SerializeField] private Canvas canvasToHide;
    [SerializeField] private Canvas canvasToHide2;
    [SerializeField] private Canvas canvasToShow;
    [SerializeField] private GameObject game;
    [SerializeField] public VideoPlayer videoPlayer;
    
    public void Start() {
        canvasToShow.enabled=false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        img.sprite = spritePressed;
        source.PlayOneShot(downSound);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        img.sprite = spriteDefault;
        source.PlayOneShot(upSound);
    }

    public void OnClick() {
        StartCoroutine(WaitAndLoadScene());
    }

    public void OnClick2() {
        StartCoroutine(WaitAndLoadScene2());
    }

    IEnumerator WaitAndLoadScene() {
        yield return new WaitForSeconds(1f);
        canvasToHide.enabled=false;
        canvasToHide2.enabled=false;
        canvasToShow.enabled=true;
        AudioManager.main.stopBacroundMusic();
        AudioManager.main.playGameMusic();
        game.SetActive(true);
        EnemySpawner.onGameStart.Invoke();
    }

    IEnumerator WaitAndLoadScene2() {
        yield return new WaitForSeconds(1f);
        canvasToHide.enabled=false;
        canvasToHide2.enabled=false;
        canvasToShow.enabled=true;
        AudioManager.main.stopBacroundMusic();
        videoPlayer.Play();
    }
}
