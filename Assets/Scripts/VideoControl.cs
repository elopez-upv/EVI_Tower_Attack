using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoControl : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public Canvas canvasToHide;
    public Canvas canvasToShow;
    public float delay = 2f;

    void Start()
    {
        // Suscribirse al evento de finalizaci�n del video
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        // Invoca la funci�n para cambiar el canvas despu�s del retraso
        Invoke("ChangeCanvas", delay);
    }

    void ChangeCanvas()
    {
        // Video terminado, desactivar el primer Canvas y activar el segundo Canvas
        canvasToHide.enabled = false;
        canvasToShow.enabled = true;
    }
}
