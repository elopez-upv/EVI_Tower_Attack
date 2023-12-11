using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager main;
    private bool isHoveringUI;

    private void Awake() {
        main = this;
    }

    public void SetHoveringState(bool state) {
        this.isHoveringUI = state;
    }

    public bool IsHoveringUI() {
        return isHoveringUI;
    }
}
