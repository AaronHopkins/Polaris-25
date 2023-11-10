using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActions : MonoBehaviour
{

    public GameObject[] ironHubPrefab;
    public GameObject[] copperHubPrefab;
    public GameObject[] farmHubPrefab;
    public GameObject[] buildingPrefab;

    MarketManager mm;
    TradeManager tradM;
    ComsBoard cb;

    int cost = 0;
    int costI = 0;
    int costC = 0;
    int costT = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (mm == null)
            mm = gameObject.GetComponent<MarketManager>();
        
        if (cb == null)
            cb = gameObject.GetComponent<ComsBoard>();

        if (tradM == null)
            tradM = gameObject.GetComponent<TradeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // fuctions split up due to not working when they are one !!!!!!

    public bool checkForResources(char type, List<ResourceTile> res, EnemyCompany e)
    {
        foreach (ResourceTile i in res)
        {
            if (i.resoucreType == type)
            {
                return true;
            }
        }

        return false;
    }

    public bool CheckCost(EnemyCompany e, char type)
    {
        switch (type)
        {
            case 'C':

                cost = 1000;
                costC = 5;
                costI = 10;
                costT = 12;

                if (e.currency_Total >= cost && e.iron_Total >= costI && e.copper_Total >= costC && e.turnHours >= costT)
                {
                    return true;
                }
                break;
            case 'I':

                cost = 1000;
                costC = 5;
                costI = 10;
                costT = 12;

                if (e.currency_Total >= cost && e.iron_Total >= costI && e.copper_Total >= costC && e.turnHours >= costT)
                {
                    return true;
                }
                break;
            case 'F':

                cost = 500;
                costC = 3;
                costI = 8;
                costT = 12;

                if (e.currency_Total >= cost && e.iron_Total >= costI && e.copper_Total >= costC && e.turnHours >= costT)
                {
                    return true;
                }
                break;
        }

        return false;
    }

    public ResourceTile upgradeTile(char type, List<ResourceTile> res, int planet, EnemyCompany e)
    {
        foreach (ResourceTile i in res)
        {
            if (i.resoucreType == type)
            { 
                GameObject nT = Instantiate(buildingPrefab[planet], i.gameObject.transform.position, Quaternion.Euler(0, 0, 0));
                Node nodeNT = nT.GetComponent<Node>();
                Node nodeTile = i.gameObject.GetComponent<Node>();
                nodeNT.selectableTile = true;
                nodeNT.upgradeAvailable = false;
                nodeNT.gridX = nodeTile.gridX;
                nodeNT.gridy = nodeTile.gridy;
                nodeNT.walkable = true;

                ResourceTile r = i;
                ResourceTile rNT = nT.GetComponent<ResourceTile>();
                rNT.resourceAmount = r.resourceAmount;
                rNT.resourceGain = r.resourceGain;
                rNT.resourceMod = r.resourceMod;
                rNT.hubCreated = true;
                rNT.tileOwner = 'E';
                rNT.enemyOwner = e;

                switch (r.resoucreType)
                {
                    case 'I':
                        rNT.resoucreType = 'I';
                        nT.GetComponent<BuildingTile>().turnCount = 2;
                        nT.GetComponent<BuildingTile>().whatWillBeBuilt = ironHubPrefab[planet];
                        nT.GetComponent<BuildingTile>().costT = costT;
                        break;
                    case 'C':
                        rNT.resoucreType = 'C';
                        nT.GetComponent<BuildingTile>().turnCount = 1;
                        nT.GetComponent<BuildingTile>().whatWillBeBuilt = copperHubPrefab[planet];
                        nT.GetComponent<BuildingTile>().costT = costT;
                        break;
                    case 'F':
                        rNT.resoucreType = 'F';
                        nT.GetComponent<BuildingTile>().turnCount = 3;
                        nT.GetComponent<BuildingTile>().whatWillBeBuilt = farmHubPrefab[planet];
                        nT.GetComponent<BuildingTile>().costT = costT;
                        break;
                }

                e.currency_Total = e.currency_Total - cost;
                e.iron_Total = e.iron_Total - costI;
                e.copper_Total = e.copper_Total - costC;
                e.turnHours = e.turnHours - costT;
                e.addTimeCost(costT);
                e.removeUseableRes(i);
                Destroy(i.gameObject);
                
                return r;
            }
        }
        return null;
    }


    public bool CheckMarket(char type, int amount , EnemyCompany e)
    {
        switch (type)
        {
            case 'C':
                if(mm.getCopperR() > 0 && e.currency_Total >= mm.getCopperCost() * amount)
                {
                    return true;
                }
                break;
            case 'F':
                if (mm.getFoodR() > 0 && e.currency_Total >= mm.getFoodCost()* amount)
                {
                    return true;
                }
                break;
            case 'I':
                if (mm.getIronR() > 0 && e.currency_Total >= mm.getIronCost()* amount)
                {
                    return true;
                }
                break;
        }

        return false;
    }

    public int buyFromMarket(int amount, char type)
    {
        switch (type)
        {
            case 'C':
                mm.buy(amount, type);
                break;
            case 'I':
                mm.buy(amount, type);
                break;
            case 'F':
                mm.buy(amount, type);
                break;
        }

        return amount;
    }

    public int sellOnMarket(int amount, char type)
    {
        int currency = 0;
        switch (type)
        {
            case 'C':
                currency = mm.sellForCurrency(amount, type);
                break;
            case 'I':
                currency = mm.sellForCurrency(amount, type);
                break;
            case 'F':
                currency = mm.sellForCurrency(amount, type);
                break;
        }

        return currency;
    }

    public void sendMessageRequest(string name, string compName, string resource, int amount, int cost, EnemyCompany e)
    {
        string text = "Hello, We are in need of " + amount + " " + resource + " would you be able to trade for " + cost + ".";
        char type = 'N';
        switch (resource)
        {
            case "food":
                type = 'F';
                break;
            case "iron":
                type = 'I';
                break;
            case "copper":
                type = 'C';
                break;
        }

        cb.EnemyMessage(compName, name, text, e, amount, cost, type);
    }



}
