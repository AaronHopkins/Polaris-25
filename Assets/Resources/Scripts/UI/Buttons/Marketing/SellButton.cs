using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellButton : MonoBehaviour
{

    MarketManager mm;
    PlayerCompany pc;
    Transform storage;
    Transform currency;

    Text ironStorage;
    Text copperStorage;
    Text foodStorage;
    Text currencyTotal;

    private void Start()
    {
        if (mm == null)
            mm = GameObject.FindGameObjectWithTag("Manager").GetComponent<MarketManager>();

        if (pc == null)
            pc = GameObject.FindGameObjectWithTag("Manager").GetComponent<PlayerCompany>();

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
        switch (gameObject.transform.parent.name)
        {
            case "Iron_buttons":
                if (pc.iron_Total > 0)
                {
                    pc.setTotal(-1, 'I'); 
                    pc.setTotal(mm.getIronCost(), 'M');
                    ironStorage.text = pc.iron_Total.ToString();
                    mm.sell(1, 'I');
                }
                break;
            case "Copper_buttons":

                if (pc.copper_Total > 0)
                {
                    pc.setTotal(-1, 'C');
                    pc.setTotal(mm.getIronCost(), 'M');
                    copperStorage.text = pc.copper_Total.ToString();
                    mm.sell(1, 'C');
                }
                break;
            case "Food_buttons":
                if(pc.food_Total > 0)
                {
                    pc.setTotal(-1, 'F');
                    pc.setTotal(mm.getIronCost(), 'M');
                    foodStorage.text = pc.food_Total.ToString();
                    mm.sell(1,'F');
                }
                break;
        }
        currencyTotal.text = pc.currency_Total.ToString();
    }
}
