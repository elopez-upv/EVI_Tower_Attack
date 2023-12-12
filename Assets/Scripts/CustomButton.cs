using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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
    [SerializeField] private Canvas canvasToShow;
    
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
        canvasToHide.enabled=false;
        canvasToShow.enabled=true;
        EnemySpawner.onGameStart.Invoke();
    }
}
