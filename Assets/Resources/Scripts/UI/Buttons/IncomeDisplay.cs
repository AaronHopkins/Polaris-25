using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IncomeDisplay : MonoBehaviour
{
    [SerializeField] PlayerIncome pci;
    [SerializeField] PlayerCompany pc;
    [SerializeField] CanvasGroup income;
    [SerializeField] TerrainGeneration tg;
    [SerializeField] CanvasGroup tips;
    [SerializeField] Canvas canvas;

    Transform incomeProfile;

    int tipCount = 0;

    Text ironTxt;
    Text copperTxt;
    Text foodTxt;
    Text netWorth;

    void Start()
    {
        if(canvas == null)
            canvas = canvas = (Canvas)GameObject.FindObjectsOfType(typeof(Canvas))[1];

        if (income == null)
            income = canvas.transform.Find("Income_Window").GetComponent<CanvasGroup>();

        GameObject manger = GameObject.FindGameObjectWithTag("Manager");

        if (tg == null)
            tg = manger.GetComponent<TerrainGeneration>();

        if (pci == null)
            pci = manger.GetComponent<PlayerIncome>();

        if (pc == null)
            pc = manger.GetComponent<PlayerCompany>();

        incomeProfile = canvas.transform.Find("Income_Window").Find("Background");

        ironTxt = incomeProfile.Find("IronIncome").GetComponent<Text>();
        copperTxt = incomeProfile.Find("CopperIncome").GetComponent<Text>();
        foodTxt = incomeProfile.Find("FoodIncome").GetComponent<Text>();
        netWorth = incomeProfile.Find("NetWorthTotal").GetComponent<Text>();
    }

    private void Update()
    {
        ironTxt.text = "Iron gain each turn: " + pci.iron_Income.ToString();
        copperTxt.text = "Copper gain each turn: " + pci.copper_Income.ToString();
        foodTxt.text = "Food gain each turn: " + pci.food_Income.ToString();
        netWorth.text = "Net-Worth Total: " + pc.getNetWorth();
    }

    public void onClick()
    {

        if (tg.currentPlanet == "Tutorial: Moon" && tipCount == 0)
        {
            tips.alpha = 1;
            tips.interactable = true;
            tips.blocksRaycasts = true;
            tipCount++;
        }

        income.alpha = 1;
        income.interactable = true;
        income.blocksRaycasts = true;
    }

}
