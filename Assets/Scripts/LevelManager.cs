using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int baseCurrency = 100;

    public static LevelManager main;

    public Transform startPoint;
    public Transform[] path;

    public int currency;

    private void Awake(){
        main = this;
    }

    private void Start() {
        currency = baseCurrency;
    }

    public void IncreaseCurrency(int amount) {
        currency += amount;
    }

    public bool SpendCurrency(int amount) {
        if (amount <= currency) {
            currency -= amount;
            return true;
        } else {
            Debug.Log("Not enough currency!");
            return false;
        }
    }
    public void SetCurrency(int amount) {
        currency = amount;
    }
}
