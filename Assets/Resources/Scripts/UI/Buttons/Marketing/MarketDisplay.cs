using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketDisplay : MonoBehaviour
{
    [SerializeField] PlayerCompany pc;
    [SerializeField] CanvasGroup market;
    [SerializeField] TerrainGeneration tg;
    [SerializeField] CanvasGroup tips;
    [SerializeField] Canvas canvas;

    MarketManager mm;

    int tipCount = 0;

    Transform marketProfile;
    Transform cost;
    Transform total;
    Transform storage;

    Text ironCost;
    Text copperCost;
    Text foodCost;
    
    Text ironInStock;
    Text copperInStock;
    Text foodInStock;

    Text ironInStorage;
    Text copperInStorage;
    Text foodInStorage;

    Text totalCurrency;

    void Start()
    {
        if (canvas == null)
            canvas = canvas = (Canvas)GameObject.FindObjectsOfType(typeof(Canvas))[1];

        if (market == null)
            market = canvas.transform.Find("Market_Window").GetComponent<CanvasGroup>();

        GameObject manager = GameObject.FindGameObjectWithTag("Manager");

        if (pc == null)
            pc = manager.GetComponent<PlayerCompany>();
        
        if (mm == null)
            mm = manager.GetComponent<MarketManager>();
        
        if (tg == null)
            tg = manager.GetComponent<TerrainGeneration>();

        marketProfile = canvas.transform.Find("Marketing_Window").Find("Background");

        cost = marketProfile.Find("Table_bg").Find("Cost");
        total = marketProfile.Find("Table_bg").Find("Total");
        storage = marketProfile.Find("Storage_bg");

        ironCost = cost.Find("IronCost_txt").GetComponent<Text>();
        copperCost = cost.Find("CopperCost_txt").GetComponent<Text>();
        foodCost = cost.Find("FoodCost_txt").GetComponent<Text>();

        ironInStock = total.Find("IronTotal_txt").GetComponent<Text>();
        copperInStock = total.Find("CopperTotal_txt").GetComponent<Text>();
        foodInStock = total.Find("FoodTotal_txt").GetComponent<Text>();

        ironInStorage = storage.transform.Find("IronStorage_txt").GetComponent<Text>();
        copperInStorage = storage.transform.Find("CopperStorage_txt").GetComponent<Text>();
        foodInStorage = storage.transform.Find("FoodStorage_txt").GetComponent<Text>();

        totalCurrency = marketProfile.Find("Currency_bg").Find("Currency_txt").GetComponent<Text>();
    }

    public void onClick()
    {
        market.alpha = 1;
        market.interactable = true;
        market.blocksRaycasts = true;

        if (tg.currentPlanet == "Tutorial: Moon" && tipCount == 0)
        {
            tips.alpha = 1;
            tips.interactable = true;
            tips.blocksRaycasts = true;
            tipCount++;
        }

        ironCost.text = mm.getIronCost().ToString();
        ironInStock.text = mm.getIronR().ToString();

        copperCost.text = mm.getCopperCost().ToString();
        copperInStock.text = mm.getCopperR().ToString();

        foodCost.text = mm.getFoodCost().ToString();
        foodInStock.text = mm.getFoodR().ToString();

        totalCurrency.text = pc.currency_Total.ToString();
        foodInStorage.text = pc.food_Total.ToString();
        copperInStorage.text = pc.copper_Total.ToString();
        ironInStorage.text = pc.iron_Total.ToString();
    }
}
