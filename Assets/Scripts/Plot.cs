using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Plot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;

    public GameObject towerObj;
    public Turret turret;
    private Color startColor;
    // private float offSetPositionY = -0.25f;

    private void Start() {
        startColor = sr.color;
    }

    private void OnMouseEnter() {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        sr.color = hoverColor;
    }

    private void OnMouseExit() {
        sr.color = startColor;
    }

    private void OnMouseDown() {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        if(UIManager.main.IsHoveringUI()) return;

        AudioManager.main.PlayButtonClickEffect();

        if (towerObj != null) {
            turret.OpenUpgradeUI();
            return;
        }

        Tower towerToBuild = BuildManager.main.GetSelectedTower();

        if (towerToBuild.cost > LevelManager.main.currency) {
            Debug.Log("Not enough currency");
            return;
        }

        LevelManager.main.SpendCurrency(towerToBuild.cost);

        Vector3 newPosition;

        newPosition = transform.position;

        towerObj = Instantiate(towerToBuild.prefab, newPosition, Quaternion.identity);
        turret = towerObj.GetComponent<Turret>();
    }
}
