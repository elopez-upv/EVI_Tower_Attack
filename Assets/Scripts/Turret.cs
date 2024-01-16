using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class Turret : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    [SerializeField] private GameObject upgradeUI;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private GameObject rangeArea;

    [Header("Attributes")]
    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float bulletsPerSecond = 1f;
    [SerializeField] private int baseUpgradeCost = 100;

    private float bulletsPerSecondBase;
    private float targetingRangeBase;

    private Transform target;
    private float timeUntilFire;

    private int level = 1;

    private void Start() {
        bulletsPerSecondBase = bulletsPerSecond;
        targetingRangeBase = targetingRange;

        upgradeButton.onClick.AddListener(Upgrade);
        rangeArea.SetActive(true);
        RefreshVisualRange();
    }

    private void Update() {
        if (target == null) {
            FindTarget();
            return;
        } 

        RotateTowardsTarget();
        RefreshVisualRange();
        
        if (!CheckTargetIsInRange()) {
            target = null;
        } else {
            timeUntilFire += Time.deltaTime;
            
            if (timeUntilFire >= 1f / bulletsPerSecond) {
                Shoot();
                timeUntilFire = 0f;
            }
        }
    }

    private void Shoot() {
        GameObject bulletObject = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);

        Bullet bulletScript = bulletObject.GetComponent<Bullet>();
        bulletScript.SetTarget(target);
        AudioManager.main.PlayShootEffect();
    }

    private void FindTarget() {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyMask);

        if (hits.Length > 0) {
            target = hits[0].transform;
        }
    }

    private bool CheckTargetIsInRange() {
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }

    private void RotateTowardsTarget() {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg + 180f;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    public void OpenUpgradeUI() {
        upgradeUI.SetActive(true);
    }

    public void CloseUpgradeUI() {
        upgradeUI.SetActive(false);
        UIManager.main.SetHoveringState(false);
    }

    public void Upgrade() {
        AudioManager.main.PlayButtonClickEffect();
        if (CalculateCost() > LevelManager.main.currency) return;

        LevelManager.main.SpendCurrency(CalculateCost());

        level++;

        bulletsPerSecond = CalculateBulletsPerSecond();
        targetingRange = CalculateRange();
        CloseUpgradeUI();
        Debug.Log("New BPS: " + bulletsPerSecond);
        Debug.Log("New Range: " + targetingRange);
        Debug.Log("New Cost: " + CalculateCost());
    }

    private int CalculateCost() {
        return Mathf.RoundToInt(baseUpgradeCost * Mathf.Pow(level, 0.8f));
    }

    private float CalculateBulletsPerSecond() {
        return bulletsPerSecondBase * Mathf.Pow(level, 0.6f);
    }

    private float CalculateRange() {
        return targetingRangeBase * Mathf.Pow(level, 0.4f);
    }

    // private void OnDrawGizmosSelected()
    // {
    //     Handles.color = Color.cyan;
    //     Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    // }

    private void RefreshVisualRange() {
        float scale = targetingRange / 2.5f;
        rangeArea.transform.localScale = Vector3.one * scale;
    }
}
