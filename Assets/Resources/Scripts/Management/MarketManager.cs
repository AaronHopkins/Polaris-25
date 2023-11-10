using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketManager : MonoBehaviour
{

    TurnManagemet tm;

    [SerializeField] Text ironTotaltxt;
    [SerializeField] Text copperTotaltxt;
    [SerializeField] Text foodTotaltxt;

    const int MAXRESOURCE = 100;
    const int MINRESOURCE = 40;
    const int MAXCOST = 40;
    const int MINCOST = 10;


    private int foodTotal;
    private int ironTotal;
    private int copperTotal;

    private int foodCost;
    private int ironCost;
    private int copperCost;

    int turnCount;

    public void setFoodR(int amount)
    {
        foodTotal += amount;
    }

    public void setIronR(int amount)
    {
        ironTotal += amount;
    }

    public void setCopperR(int amount)
    {
        copperTotal += amount;
    }

    public int getFoodR()
    {
        return foodTotal;
    }

    public int getIronR()
    {
        return ironTotal;
    }

    public int getCopperR()
    {
        return copperTotal;
    }

    public int getFoodCost()
    {
        return foodCost;
    }

    public int getIronCost()
    {
        return ironCost;
    }

    public int getCopperCost()
    {
        return copperCost;
    }

    public int getMaxCost()
    {
        return MAXCOST;
    } 
    
    public int getMinCost()
    {
        return MINCOST;
    }

    public void genResources()
    {
        foodTotal = Random.Range(MAXRESOURCE, MINRESOURCE);
        ironTotal = Random.Range(MAXRESOURCE, MINRESOURCE);
        copperTotal = Random.Range(MAXRESOURCE, MINRESOURCE);

        foodCost = Random.Range(MAXCOST, MINCOST);
        copperCost = Random.Range(MAXCOST, MINCOST);
        ironCost = Random.Range(MAXCOST, MINCOST); 
    }

    void Start()
    {
        genResources();
        tm = gameObject.GetComponent<TurnManagemet>();
        turnCount = tm.currentTurn + 1;
    }

    void Update()
    {
        if (tm.currentTurn == turnCount)
        {
            genResources();
            turnCount++;
        }

        ironTotaltxt.text = ironTotal.ToString();
        copperTotaltxt.text = copperTotal.ToString();
        foodTotaltxt.text = foodTotal.ToString();
    }

    public int sellForCurrency(int amount, char type)
    {
        int num = 0;
        switch (type)
        {
            case 'C':
                copperTotal += amount;
                num = copperCost * amount;
                break;
            case 'I':
                ironTotal += amount;
                num = ironCost * amount;
                break;
            case 'F':
                foodTotal += amount;
                num = foodCost * amount;
                break;
        }
        return num;
    }

    public void buy(int amount, char type)
    {
        switch (type)
        {
            case 'C':
                if (copperTotal >= amount)
                    copperTotal -= amount;
                break;
            case 'I':
                if(ironTotal >= amount)
                    ironTotal -= amount;
                break;
            case 'F':
                if(foodTotal >= amount)
                    foodTotal -= amount;
                break;
        }
    }

    public void sell(int amount, char type)
    {
        switch (type)
        {
            case 'C':
                if (copperTotal >= amount)
                    copperTotal += amount;
                break;
            case 'I':
                if (ironTotal >= amount)
                    ironTotal += amount;
                break;
            case 'F':
                if (foodTotal >= amount)
                    foodTotal += amount;
                break;
        }
    }
}
