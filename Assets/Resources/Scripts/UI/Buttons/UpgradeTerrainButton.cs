using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeTerrainButton : MonoBehaviour
{

    public GameObject[] ironHubPrefab;
    public GameObject[] copperHubPrefab;
    public GameObject[] farmHubPrefab;
    public GameObject[] buildingPrefab;

    [SerializeField] TerrainGeneration tg;
    [SerializeField] PlayerCompany pC;
    [SerializeField] GameManager gM;
    [SerializeField] TurnManagemet tm;

    [SerializeField] int cost = 0;
    [SerializeField] int costI = 0;
    [SerializeField] int costC = 0;
    [SerializeField] int costT = 0;

    void Start()
    {
        if (pC == null)
            pC = GameObject.FindGameObjectWithTag("Manager").GetComponent<PlayerCompany>();
        
        if (gM == null)
            gM = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        
        if (tm == null)
            tm = GameObject.FindGameObjectWithTag("Manager").GetComponent<TurnManagemet>();

        if(tg == null)
            tg = GameObject.FindGameObjectWithTag("Manager").GetComponent<TerrainGeneration>();
    }

    public void onClick()
    {
        GameObject tile = gM.currentTileSelected;
        int planet;
        switch (tg.currentPlanet)
        {
            case "Tutorial: Moon":
                planet = 0;
                break;
            case "Easy: Moon":
                planet = 0;
                break;
            case "Normal: Mars":
                planet = 1;
                break;
            case "Hard: Jupiter":
                planet = 2;
                break;
            default:
                planet = 0;
                break;
        }

        if (tile == null)
        {
            return;
        }

        if (pC.currency_Total >= cost && pC.iron_Total >= costI && pC.copper_Total >= costC && tm.turnHours >= costT)
        {
            if (tile.tag == "Terrain" || tile.tag == "Mountin")
            {
                //When Housing and Steel is implomented
            }
            else if(tile.tag == "Resource")
            {

                GameObject nT = Instantiate(buildingPrefab[planet], tile.transform.position, Quaternion.Euler(0, 0, 0));
                Node nodeNT = nT.GetComponent<Node>();
                Node nodeTile = tile.GetComponent<Node>();
                nodeNT.selectableTile = true;
                nodeNT.upgradeAvailable = false;
                nodeNT.gridX = nodeTile.gridX;
                nodeNT.gridy = nodeTile.gridy;
                nodeNT.walkable = true;

                ResourceTile r = tile.GetComponent<ResourceTile>();
                ResourceTile rNT = nT.GetComponent<ResourceTile>();
                rNT.resourceAmount = r.resourceAmount;
                rNT.resourceGain = r.resourceGain;
                rNT.resourceMod = r.resourceMod;
                rNT.tileOwner = 'P';

                switch (r.resoucreType)
                {
                    case 'I':
                        rNT.resoucreType = 'I';
                        nT.GetComponent<BuildingTile>().turnCount = 2;
                        nT.GetComponent<BuildingTile>().whatWillBeBuilt = ironHubPrefab[planet];
                        nT.GetComponent<BuildingTile>().costT = costT;
                        cost = 1000;
                        costC = 5;
                        costI = 10;
                        costT = 12;
                        break;
                    case 'C':
                        rNT.resoucreType = 'C';
                        nT.GetComponent<BuildingTile>().turnCount = 1;
                        nT.GetComponent<BuildingTile>().whatWillBeBuilt = copperHubPrefab[planet];
                        nT.GetComponent<BuildingTile>().costT = costT;
                        cost = 1000;
                        costC = 5;
                        costI = 10;
                        costT = 12;
                        break;
                    case 'F':
                        rNT.resoucreType = 'F';
                        nT.GetComponent<BuildingTile>().turnCount = 3;
                        nT.GetComponent<BuildingTile>().whatWillBeBuilt = farmHubPrefab[planet];
                        nT.GetComponent<BuildingTile>().costT = costT;
                        cost = 500;
                        costC = 3;
                        costI = 8;
                        costT = 12;
                        break;
                }

                pC.currency_Total -= cost;
                pC.iron_Total -= costI;
                pC.copper_Total -= costC;
                tm.turnHours -= costT;
                pC.addTimeCost(costT);
                
                Destroy(tile);
            }
        }  
    }
}
