using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farmable : ResourceTile
{
    private const int MAXAMOUNT = 400;
    private const int MINAMOUNT = 300;

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

            terrainName = "Cultivated Farm";
            description = terrainName + ": Produces " + resourceGain + " food each turn";
        }
    }

    public void genFarm()
    {
        resourceAmount = Random.Range(MINAMOUNT, MAXAMOUNT);
        resoucreType = 'F';

        resourceGain = Random.Range(2, 5);

        terrainName = "Uncultivated Farm Land";
        description = terrainName + ": Can be cultivated for producing food. Once cultivated produces " + resourceGain + " Food per turn.";
    }

    private void Update()
    {
        if (hubCreated == true)
        {

            if (tM.currentTurn == turnCount)
            {
                if (tileOwner == 'P')
                    pI.gainIncome(resoucreType, 3);
                else
                {
                    enemyOwner.gainIncome(resoucreType, 2);
                }
                turnCount++;
            }
        }
    }
}
