using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TradeDisplay : MonoBehaviour
{
    [SerializeField] PlayerCompany pc;
    [SerializeField] CanvasGroup trade;
    [SerializeField] TurnManagemet tm;
    [SerializeField] TerrainGeneration tg;
    [SerializeField] CanvasGroup tips;
    [SerializeField] Canvas canvas;

    int tipsCount = 0;

    GameManager gm;
    MarketManager mm;
    EnemyCompany ec;
    TradeManager tradeM;

    Transform tradeProfile;
    Transform cost;
    Transform total;
    Transform storage;
    Transform shares;

    Text ironCost;
    Text copperCost;
    Text foodCost;
    Text shareCost;

    Text ironInStock;
    Text copperInStock;
    Text foodInStock;
    Text shareInStock;

    Text ironInStorage;
    Text copperInStorage;
    Text foodInStorage;
    Text shareOwned;

    Text totalCurrency;


    void Start()
    {
        if (canvas == null)
            canvas = canvas = (Canvas)GameObject.FindObjectsOfType(typeof(Canvas))[1];

        if (trade == null)
            trade = canvas.transform.Find("Trading_Window").GetComponent<CanvasGroup>();

        GameObject manager = GameObject.FindGameObjectWithTag("Manager");

        if (pc == null)
            pc = manager.GetComponent<PlayerCompany>();
        
        if (gm == null)
            gm = manager.GetComponent<GameManager>();

        if (tradeM == null)
            tradeM = manager.GetComponent<TradeManager>();

        if (mm == null)
            mm = manager.GetComponent<MarketManager>();
        
        if (tm == null)
            tm = manager.GetComponent<TurnManagemet>();
        
        if (tg == null)
            tg = manager.GetComponent<TerrainGeneration>();

        tradeProfile = canvas.transform.Find("Trading_Window").Find("Background");

        cost = tradeProfile.Find("Table_bg").Find("Cost");
        total = tradeProfile.Find("Table_bg").Find("Total");
        storage = tradeProfile.Find("Storage_bg");
        shares = tradeProfile.Find("ShareTable_bg");

        ironCost = cost.Find("IronCost_txt").GetComponent<Text>();
        copperCost = cost.Find("CopperCost_txt").GetComponent<Text>();
        foodCost = cost.Find("FoodCost_txt").GetComponent<Text>();
        shareCost = shares.Find("SharesCost_txt").GetComponent<Text>();

        ironInStock = total.Find("IronTotal_txt").GetComponent<Text>();
        copperInStock = total.Find("CopperTotal_txt").GetComponent<Text>();
        foodInStock = total.Find("FoodTotal_txt").GetComponent<Text>();
        shareInStock = shares.Find("SharesTotal_txt").GetComponent<Text>();

        ironInStorage = storage.transform.Find("IronStorage_txt").GetComponent<Text>();
        copperInStorage = storage.transform.Find("CopperStorage_txt").GetComponent<Text>();
        foodInStorage = storage.transform.Find("FoodStorage_txt").GetComponent<Text>();
        shareOwned = shares.Find("SharesOwned_txt").GetComponent<Text>();

        totalCurrency = tradeProfile.Find("Currency_bg").Find("Currency_txt").GetComponent<Text>();
    }

    public void onClick()
    {
        if (tm.turnHours >= 2)
        {
            ec = gm.selecyedEnemy;

            if (tg.currentPlanet == "Tutorial: Moon" && tipsCount == 0)
            {
                tips.alpha = 1;
                tips.interactable = true;
                tips.blocksRaycasts = true;
                tipsCount++;
            }

            trade.alpha = 1;
            trade.interactable = true;
            trade.blocksRaycasts = true;

            foodCost.text = ec.getFoodCost().ToString();
            copperCost.text = ec.getCopperCost().ToString();
            ironCost.text = ec.getIronCost().ToString();
            shareCost.text = ec.getShareCost().ToString();

            ironInStock.text = ec.iron_Total.ToString();        
            copperInStock.text = ec.copper_Total.ToString();        
            foodInStock.text = ec.food_Total.ToString();
            shareInStock.text = ec.getShareLeft().ToString();

            totalCurrency.text = pc.currency_Total.ToString();
            foodInStorage.text = pc.food_Total.ToString();
            copperInStorage.text = pc.copper_Total.ToString();
            ironInStorage.text = pc.iron_Total.ToString();
            shareOwned.text = pc.amountShares(ec.getID()).ToString();

            tm.turnHours -= 2;
        }
    }


}
