using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iron : ResourceTile
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
                enemyOwner.setIncome(resourceGain, resoucreType);

            terrainName = "Iron Mine";
            description = terrainName + ": This Iron Mine produces " + resourceGain + " Iron Ore per turn.";
        }
    }


    public void genIron()
    {
        resourceAmount = Random.Range(MINAMOUNT, MAXAMOUNT);
        resoucreType = 'I';

        resourceGain = Random.Range(2,5);

        terrainName = "Iron Deposit";
        description = terrainName + ": This location has a high density of iron under the ground. Claiming it will produce "+ resourceGain + " Iron Ore per turn.";
    }

    private void Update()
    {
        if(hubCreated == true)
        {            
            if (tM.currentTurn == turnCount)
            {
                if (tileOwner == 'P')
                    pI.gainIncome(resoucreType, 2);
                else
                {
                    enemyOwner.gainIncome(resoucreType, 2);
                }

                turnCount++;
            }
        }
    }
}
