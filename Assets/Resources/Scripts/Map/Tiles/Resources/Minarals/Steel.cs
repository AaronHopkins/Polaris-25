using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steel : ResourceTile
{
    private const int MAXAMOUNT = 700;
    private const int MINAMOUNT = 400;

    public void genSteel()
    {
        resourceAmount = Random.Range(MINAMOUNT,MAXAMOUNT);
        resoucreType = 'S';
    }

}
