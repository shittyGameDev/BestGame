using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBase : MonoBehaviour
{
    public int cost = 10;

    public bool IsActive { get; private set; } = false;

    public void ActivateTower()
    {
        IsActive = true;
    }
}
