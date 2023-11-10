using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingTile : ResourceTile
{

    public int turnCount;
    public int turnUntilDone;
    public int turnBuilt;
    public GameObject whatWillBeBuilt;
    public int costT;

    [SerializeField] GameManager gM;
    [SerializeField] TurnManagemet tM;
    [SerializeField] PlayerCompany pc;

    void Start()
    {
        if (gM == null)
            gM = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();

        if (tM == null)
            tM = GameObject.FindGameObjectWithTag("Manager").GetComponent<TurnManagemet>();

        if (pc == null)
            pc = GameObject.FindGameObjectWithTag("Manager").GetComponent<PlayerCompany>();

        turnUntilDone = turnCount;

        turnBuilt = tM.currentTurn;
    }

    void Update()
    {
        turnUntilDone = turnCount - tM.currentTurn + 1;

        if(tM.currentTurn == (turnBuilt + turnCount))
        {
            GameObject nT = Instantiate(whatWillBeBuilt, gameObject.transform.position, Quaternion.Euler(0, 0, 0));
            Node nodeNT = nT.GetComponent<Node>();
            nodeNT.selectableTile = true;
            nodeNT.upgradeAvailable = false;
            nodeNT.gridX = gridX;
            nodeNT.gridy = gridy;
            nodeNT.walkable = true;

            ResourceTile rNT = nT.GetComponent<ResourceTile>();
            rNT.resourceAmount = resourceAmount;
            rNT.resourceGain = resourceGain;
            rNT.resourceMod = resourceMod;
            rNT.resoucreType = resoucreType;
            rNT.hubCreated = true;
            rNT.tileOwner = tileOwner;
            
            if(tileOwner == 'P')
                pc.removeATimeCost(costT);
            else
            {
                rNT.enemyOwner = enemyOwner;
                enemyOwner.removeATimeCost(costT);
                enemyOwner.addUpgradedRes(rNT);
            }

            Destroy(gameObject);
        }
    }
}
