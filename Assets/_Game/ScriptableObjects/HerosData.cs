using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "herodata", menuName = "ScriptableObjects/herodata", order = 1)]
public class HerosData : ScriptableObject
{
    public int priceHero;
    public Sprite imageHero;
    public PoolType poolType;
}
