using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BrickData", menuName = "ScriptableObjects/BrickData", order = 1)]
public class BrickData : ScriptableObject
{
    public string brickName;
    public string brickDescription;
    public Sprite brickImage;
    public int brickPrice;
    public int brickHealth;
    public int brickDamage;

}
