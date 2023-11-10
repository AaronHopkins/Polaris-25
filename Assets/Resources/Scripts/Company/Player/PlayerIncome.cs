using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerIncome : MonoBehaviour
{
    PlayerCompany PC;

    [SerializeField]private int foodMod;
    [SerializeField]private int livestockMod;
    [SerializeField]private int ironMod;
    [SerializeField]private int copperMod;
    [SerializeField]private int steelMod;
    [SerializeField]private int currencyMod;

    //Resorce Income
    private int foodIncome;
    public int food_Income { get => foodIncome; set => foodIncome = value; }

    private int livestockIncome;
    public int livestock_Income { get => livestockIncome; set => livestockIncome = value; }

    private int ironIncome;
    public int iron_Income { get => ironIncome; set => ironIncome = value; }

    private int copperIncome;
    public int copper_Income { get => copperIncome; set => copperIncome = value; }

    private int steelIncome;
    public int steel_Income { get => steelIncome; set => steelIncome = value; }

    private int currencyIncome;
    public int currenct_Income { get => currencyIncome; set => currencyIncome = value; }

    private void Awake()
    {
        PC = this.gameObject.GetComponent<PlayerCompany>();
        foodMod = 1;
        copperMod = 1;
        ironMod = 1;
        currencyMod = 1;
    }

    private void Update()
    {    }

    public void setStrAndWeak()
    {

        switch (PC.company_Strength)
        {
            case "Plus to Currency":
                currencyMod = currencyMod + 10;
                break;
            case "Plus to Iron":
                ironMod = ironMod + 10;
                break;
            case "Plus to Copper":
                copperMod = copperMod + 10;
                break;
            case "Plus to Food":
                foodMod = foodMod + 10;
                break;
        }

        switch (PC.company_Weakness)
        {
            case "Minus to Currency":
                currencyMod = currencyMod + -10;
                break;
            case "Minus to Iron":
                ironMod = ironMod + -10;
                break;
            case "Minus to Copper":
                copperMod = copperMod + -10;
                break;
            case "Minus to Food":
                foodMod = foodMod + -10;
                break;
        }
    }

    public void setFoodMod(int v)
    {
        foodMod = foodMod + v;
    }

    public void setLivestockMod(int v)
    {
        livestockMod = livestockMod + v;
    }

    public void setIronMod(int v)
    {
        ironMod = ironMod + v;
    } 
    
    public void setCopperMod(int v)
    {
        copperMod = copperMod + v;
    }

    public void setSteelMod(int v)
    {
        steelMod = steelMod + v;
    }

    public void setCurrencyMod(int v)
    {
        currencyMod = currencyMod + v;
    }

    public void setIncome(int g, char t)
    {
        switch (t)
        {
            case 'I':
                iron_Income = iron_Income + g + (iron_Income + g) / ironMod ;
                break;
            case 'C':
                copper_Income = copper_Income + g + (copper_Income + g) / copperMod;
                break;
            case 'F':
                food_Income = food_Income + g + (food_Income + g) / foodMod;
                break;
            case 'M':
                currenct_Income = currenct_Income + g + (currenct_Income + g) / currencyMod;
                break;
        }
    }

    public void gainIncome(char t, int mod)
    {
        switch (t)
        {
            case 'I':
                PC.setTotal((ironIncome * mod), t);
                break;
            case 'C':
                PC.setTotal((copperIncome * mod), t);
                break;
            case 'F':
                PC.setTotal((foodIncome * mod), t);
                break;
            case 'M':
                PC.setTotal((currencyIncome * mod), t);
                break;
        }
    }
}
