using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour
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
        GameObject manager = GameObject.FindGameObjectWithTag("Manager");

        if (mm == null)
            mm = manager.GetComponent<MarketManager>();
        
        if (pc == null)
            pc = manager.GetComponent<PlayerCompany>();

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
                if (mm.getIronR() > 0 && pc.currency_Total > mm.getIronCost())
                {
                    pc.setTotal(1, 'I');
                    pc.setTotal(-mm.getIronCost(), 'M');
                    ironStorage.text = pc.iron_Total.ToString();
                    mm.buy(1, 'I');
                }
                break;
            case "Copper_buttons":
                if(mm.getCopperR() > 0 && pc.currency_Total > mm.getCopperCost())
                {
                    pc.setTotal(1, 'C');
                    pc.setTotal(-mm.getCopperCost(), 'M');
                    copperStorage.text = pc.copper_Total.ToString();
                    mm.buy(1,'C');
                }
                break;
            case "Food_buttons":
                if(mm.getFoodR() > 0 && pc.currency_Total > mm.getFoodCost())
                {
                    pc.setTotal(1, 'F');
                    pc.setTotal(-mm.getFoodCost(), 'M');
                    foodStorage.text = pc.food_Total.ToString();
                    mm.buy(1,'F');
                }
                break;
        }

        currencyTotal.text = pc.currency_Total.ToString();
    }
}
