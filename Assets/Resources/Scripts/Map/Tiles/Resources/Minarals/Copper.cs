using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Copper : ResourceTile
{
    private const int MAXAMOUNT = 700;
    private const int MINAMOUNT = 400;

    [SerializeField] PlayerIncome pI;
    [SerializeField] TurnManagemet tM;
    public int turnCount;

    private void Awake()
    {
        if (pI == null)
            pI = GameObject.FindGameObjectWithTag("Manager").GetComponent<PlayerIncome>();

        if (tM == null)
            tM = GameObject.FindGameObjectWithTag("Manager").GetComponent<TurnManagemet>();

        turnCount = tM.currentTurn + 1;
    }

    private void Start()
    {
        if (hubCreated == true)
        {
            if (tileOwner == 'P')
                pI.setIncome(resourceGain, resoucreType);
            else
            {
                enemyOwner.setIncome(resourceGain, resoucreType);
            }
                

            terrainName = "Copper Mine";
            description = terrainName + ": This mine produces "+ resourceGain + " Copper per turn.";
        }
    }

    public void genCopper()
    {
        resourceAmount = Random.Range(MINAMOUNT, MAXAMOUNT);
        resoucreType = 'C';

        resourceGain = Random.Range(2, 5);

        terrainName = "Copper Vein";
        description = terrainName + ": This tile has a high density of copper running through it. Mining it would produce " + resourceGain + " Copper per turn.";
    }

    private void Update()
    {
        if (hubCreated == true)
        {
            if (tM.currentTurn == turnCount)
            {
                if (tileOwner == 'P')
                    pI.gainIncome(resoucreType, 1);
                else
                    enemyOwner.gainIncome(resoucreType, 1);
                turnCount++;
            }
        }
    }
}
