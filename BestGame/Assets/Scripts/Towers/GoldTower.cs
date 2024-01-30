using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldTower : MonoBehaviour
{
    //Header for inspector gold variables
    [Header("Gold Variables")]
    public int gold;
    public int goldPerSecond = 1;
    private float goldTimer; // time since last gold payment
    [SerializeField] private float goldInterval = 5f; // time between gold payments

    [SerializeField] private int goldCollected; // gold collected by player

    //Header for inspector tower variables
    [Header("Tower Variables")]
    public int towerCost = 2;
    public int towerHealth = 10;
    



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(GoldTick());
    }

    IEnumerator GoldTick()
    {
        goldTimer += Time.deltaTime;
        if (goldTimer >= goldInterval)
        {
            goldTimer = 0f;
            
            gold += goldPerSecond;

        }
        yield return null;
    }

    private void OnMouseDown()
    {
        goldCollected = gold;
        gold = 0;
        GameManager.Instance.AddGold(goldCollected);
    }
}
