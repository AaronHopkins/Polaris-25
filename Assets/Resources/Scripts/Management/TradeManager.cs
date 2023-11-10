using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TradeManager : MonoBehaviour
{
    MarketManager mm;
    TurnManagemet tm;

    const int shareMIN = 2000;
    const int shareMAX = 3000;

    private int foodCost;
    private int ironCost;
    private int copperCost;
    private int shareCost;

    [SerializeField] Text ironTotaltxt;
    [SerializeField] Text copperTotaltxt;
    [SerializeField] Text foodTotaltxt;
    [SerializeField] Text shareTotaltxt;

    //int turnCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (mm == null)
            mm = gameObject.GetComponent<MarketManager>();
        if (tm == null)
            tm = GameObject.FindGameObjectWithTag("Manager").GetComponent<TurnManagemet>();
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
    
    public int getShareCost()
    {
        return shareCost;
    }

    public int genFoodCost()
    {
        foodCost = Random.Range(mm.getMinCost(), mm.getMaxCost());

        return foodCost;
    }

    public int genIronCost()
    {
        ironCost = Random.Range(mm.getMinCost(), mm.getMaxCost());
        return ironCost;
    }

    public int genCopperCost()
    {
        copperCost = Random.Range(mm.getMinCost(), mm.getMaxCost());
        return copperCost;
    }

    public int genShareCost()
    {
        shareCost = Random.Range(shareMIN, shareMAX);
        return shareCost;
    }

    public void updatTotal(EnemyCompany e)
    {
        ironTotaltxt.text = e.iron_Total.ToString();
        copperTotaltxt.text = e.copper_Total.ToString();
        foodTotaltxt.text = e.food_Total.ToString();
        shareTotaltxt.text = e.getShareLeft().ToString();
    }

    void Update()
    {

    }
}
