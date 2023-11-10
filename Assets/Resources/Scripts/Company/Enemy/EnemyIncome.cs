using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIncome : MonoBehaviour
{
    EnemyCompany enCompany;

    [SerializeField] private int foodMod = 1;
    [SerializeField] private int livestockMod = 1;
    [SerializeField] private int ironMod = 1;
    [SerializeField] private int copperMod = 1;
    [SerializeField] private int steelMod = 1;
    [SerializeField] private int currencyMod = 1;

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
    }

    private void Update()
    { }

    public void setStrAndWeak(EnemyCompany ec)
    {
        enCompany = ec;

        switch (ec.company_Strength)
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

        switch (ec.company_Weakness)
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
                iron_Income = iron_Income + g + (iron_Income + g) / ironMod;
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
                enCompany.setTotal((ironIncome * mod), t);
                break;
            case 'C':
                enCompany.setTotal((copperIncome * mod), t);
                break;
            case 'F':
                enCompany.setTotal((foodIncome * mod), t);
                break;
            case 'M':
                enCompany.setTotal((currencyIncome * mod), t);
                break;
        }
    }
}
