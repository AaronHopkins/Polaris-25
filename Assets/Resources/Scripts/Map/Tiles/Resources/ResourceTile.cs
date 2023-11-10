using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceTile : Node
{
    public int resourceAmount;
    public int resourceMod;
    public int resourceGain;
    public char resoucreType;
    public bool hubCreated;
    public bool tileWorked = false;
    public char tileOwner;
    public EnemyCompany enemyOwner;

    private void Awake()
    {


    }
}

