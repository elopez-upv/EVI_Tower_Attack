using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Video;

public class HistoryButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [Header("References")]
    [SerializeField] public Image img;
    [SerializeField] public Sprite spriteDefault;
    [SerializeField] public Sprite spritePressed;
    [SerializeField] public AudioClip downSound;
    [SerializeField] public AudioClip upSound;
    [SerializeField] public AudioSource source;
    [SerializeField] public Canvas canvasToHide;
    [SerializeField] public Canvas canvasToShow;
    [SerializeField] public float delay;
    [SerializeField] public VideoPlayer videoPlayer;

    public void Start()
    {
        canvasToShow.enabled = false;
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

    public void OnClick()
    {
        StartCoroutine(WaitAndLoadScene());
    }

    IEnumerator WaitAndLoadScene()
    {
        yield return new WaitForSeconds(delay);
        canvasToHide.enabled = false;
        canvasToShow.enabled = true;
        videoPlayer.Play();
    }
}