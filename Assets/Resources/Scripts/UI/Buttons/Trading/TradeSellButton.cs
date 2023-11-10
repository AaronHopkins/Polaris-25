using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TradeSellButton : MonoBehaviour
{

    TradeManager tradeM;
    PlayerCompany pc;
    GameManager gm;

    Transform storage;
    Transform currency;

    Text ironStorage;
    Text copperStorage;
    Text foodStorage;
    Text currencyTotal;

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
            while (perant.name != "Marketing_Window")
            {
                storage = perant.transform;
                if (storage.name == "Background")
                {
                    currency = storage.Find("Currency_bg");
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
    }

    public void onClick()
    {
        EnemyCompany e = gm.currentTileSelected.GetComponent<EnemyCompany>();

        switch (gameObject.transform.parent.name)
        {
            case "Iron_buttons":
                if(pc.iron_Total > 0)
                {
                    pc.setTotal(-1, 'I');
                    pc.setTotal(tradeM.getIronCost(), 'M');
                    ironStorage.text = pc.iron_Total.ToString();
                }
                break;
            case "Copper_buttons":
                if(pc.copper_Total > 0)
                {
                    pc.setTotal(-1, 'C');
                    pc.setTotal(tradeM.getCopperCost(), 'M');
                    copperStorage.text = pc.copper_Total.ToString();
                }
                break;
            case "Food_buttons":
                if(pc.food_Total > 0)
                {
                    pc.setTotal(-1, 'F');
                    pc.setTotal(tradeM.getFoodCost(), 'M');
                    foodStorage.text = pc.food_Total.ToString();
                }
                break;
        }
        currencyTotal.text = pc.currency_Total.ToString();
    }
}
