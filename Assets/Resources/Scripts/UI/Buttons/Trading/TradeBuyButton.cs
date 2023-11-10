using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TradeBuyButton : MonoBehaviour
{
    TradeManager tradeM;
    EnemyCompany ec;
    GameManager gm;
    PlayerCompany pc;
    Transform storage;
    Transform currency;
    Transform share;

    Text ironStorage;
    Text copperStorage;
    Text foodStorage;
    Text currencyTotal;
    Text shareTotal;

    private void Start()
    {
        GameObject manager = GameObject.FindGameObjectWithTag("Manager");

        if (tradeM == null)
            tradeM = manager.GetComponent<TradeManager>();
        
        if (pc == null)
            pc = manager.GetComponent<PlayerCompany>();

        if (gm == null)
            gm = manager.GetComponent<GameManager>();

        if (storage == null)
        {
            Transform perant = transform.parent;
            while (perant.name != "Trading_Window")
            {
                storage = perant.transform;
                if (storage.name == "Background")
                {
                    currency = storage.Find("Currency_bg");
                    share = storage.Find("ShareTable_bg");
                    storage = storage.Find("Storage_bg");
                    break;
                }
                perant = perant.parent;
            }
        }

        ironStorage = storage.Find("IronStorage_txt").GetComponent<Text>();
        copperStorage = storage.Find("CopperStorage_txt").GetComponent<Text>();
        foodStorage = storage.Find("FoodStorage_txt").GetComponent<Text>();
        currencyTotal = currency.Find("Currency_txt").GetComponent<Text>();
        shareTotal = share.Find("SharesOwned_txt").GetComponent<Text>();
    }

    public void onClick()
    {
        ec = gm.selecyedEnemy;

        switch (gameObject.transform.parent.name)
        {
            case "ShareTable_bg":
                if(pc.currency_Total >= tradeM.getShareCost())
                {
                    pc.buyingShares(ec.getID());
                    pc.setTotal(-ec.getShareCost(),'M');
                    shareTotal.text = pc.amountShares(ec.getID()).ToString();
                    if(ec.getShareLeft() > 0)
                    {
                        ec.loseShare(pc.companyPlayer);
                    }
                }
                break;
            case "Iron_buttons":
                if(ec.iron_Total > 0 && pc.currency_Total > tradeM.getIronCost())
                {
                    pc.setTotal(1, 'I');
                    pc.setTotal(-tradeM.getIronCost(), 'M');
                    ironStorage.text = pc.iron_Total.ToString();
                    ec.iron_Total -= 1;
                }
                break;
            case "Copper_buttons":
                if(ec.copper_Total > 0 && pc.currency_Total > tradeM.getCopperCost())
                {
                    pc.setTotal(1, 'C');
                    pc.setTotal(-tradeM.getCopperCost(), 'M');
                    copperStorage.text = pc.copper_Total.ToString();
                    ec.copper_Total -= 1;
                }
                break;
            case "Food_buttons":
                if(ec.food_Total > 0 && pc.currency_Total > tradeM.getFoodCost())
                {
                    pc.setTotal(1, 'F');
                    pc.setTotal(-tradeM.getFoodCost(), 'M');
                    foodStorage.text = pc.food_Total.ToString();
                    ec.food_Total -= 1;
                }
                break;
        }

        tradeM.updatTotal(ec);
        currencyTotal.text = pc.currency_Total.ToString();
    }
}
