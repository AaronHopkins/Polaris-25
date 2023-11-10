using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCompany : MonoBehaviour
{
    [SerializeField] Transform comProfile;
    [SerializeField] GameObject outlinePrefab;
    public  PlayerCompanyBuild playerBuilding;
    public GameObject companyPlayer;

    List<int> TimeCost = new List<int>();
    int turnCount;

    List<int> turnArray = new List<int>();
    List<int> networthArray = new List<int>();
    int turnNetworth = 0;

    int turnsTillDeath = 3;

    PlayerIncome pi;
    TurnManagemet tm;
    TradeManager tradeM;
    GameManager gm;
    MarketManager mm;

    Transform comNameText;
    Transform comCEOText;
    Transform planetText;
    Transform typeText;
    Transform strengthText;
    Transform weaknessText;
    Transform portraitImage;

    public Sprite[] charactarPort;

    GameObject outlineTile;
    List<GameObject> outlineObjects = new List<GameObject>();

    Canvas canvas;

    Text totalCurrencytxt;
    Text totalIronTxt;
    Text totalCopperTxt;
    Text totalFoodTxt;

    int numOfShares = 10;
    List<GameObject> shares = new List<GameObject>();
    List<int> sharesOwned = new List<int>();
    int shareCost;

    const int MAXQUESTINT = 300;
    public bool questComplete = false;
    int questInt = 0;
    char questChar;

    Transform topHUD;
    Transform currencyUI;
    Transform ironUI;
    Transform copperUI;
    Transform foodUI;
    Text txt;

    double netWorth = 0;

    //Resorce Total
    //Really dont like this Get and Set but have no time to change
    private int foodTotal;
    public int food_Total{ get => foodTotal; set => foodTotal = value; }
    
    private int livestockTotal;
    public int livestock_Total{ get => livestockTotal; set => livestockTotal = value; }

    private int ironTotal;
    public int iron_Total{ get => ironTotal; set => ironTotal = value; }

    private int copperTotal; 
    public int copper_Total{ get => copperTotal; set => copperTotal = value; }

    private int steelTotal;
    public int steel_Total{ get => steelTotal; set => steelTotal = value; }

    private int currencyTotal;
    public int currency_Total { get => currencyTotal; set => currencyTotal = value; }

    //Company Info
    public int companyID;

    private string companyName;
    public string company_Name { get => companyName; set => companyName = value; }

    private string companyCEOName;
    public string company_CEOName { get => companyCEOName; set=> companyCEOName = value; }

    private string planetName;
    public string planet_Name  { get => planetName; set=> planetName = value; }

    private string companyType;
    public string company_Type  { get => companyType; set=>companyType = value; }

    private string companyStrength;
    public string company_Strength  { get => companyStrength; set=> companyStrength = value; }

    private string companyWeakness;
    public string company_Weakness { get => companyWeakness; set => companyWeakness = value; }

    private int companyPortait;
    public int company_Portait { get => companyPortait; set => companyPortait = value; }


    private void Awake()
    {
        if (canvas == null)
        {
            canvas = (Canvas)GameObject.FindObjectsOfType(typeof(Canvas))[1];
        }

        if (comProfile == null)
        {
            comProfile = GameObject.FindGameObjectWithTag("Company Profile").transform;
        }

        if (tm == null)
            tm = gameObject.GetComponent<TurnManagemet>();

        if (tradeM == null)
            tradeM = gameObject.GetComponent<TradeManager>();

        if (mm == null)
            mm = gameObject.GetComponent<MarketManager>();

        if (gm == null)
            gm = gameObject.GetComponent<GameManager>();

        if(pi == null)
            pi = this.gameObject.GetComponent<PlayerIncome>();
        
        if (outlinePrefab == null)
            outlinePrefab = Resources.Load("Prefabs/Outline") as GameObject;
        
        topHUD = canvas.transform.Find("Top_HUD");

        currencyUI = topHUD.Find("IncomeUI_Background").Find("Currency_Background");
        copperUI = topHUD.Find("IncomeUI_Background").Find("Copper_Background");
        ironUI = topHUD.Find("IncomeUI_Background").Find("Iron_Background");
        foodUI = topHUD.Find("IncomeUI_Background").Find("Food_Background");

        totalCurrencytxt = currencyUI.Find("CurrencyIncomeTotal_txt").GetComponent<Text>();
        totalCopperTxt = copperUI.Find("CopperIncomeTotal_txt").GetComponent<Text>();
        totalIronTxt = ironUI.Find("IronIncomeTotal_txt").GetComponent<Text>();
        totalFoodTxt = foodUI.Find("FoodIncomeTotal_txt").GetComponent<Text>();

        companyID = 25;
    }

    private void Start()
    {
        outlineTile = new GameObject();
        outlineTile.name = "Outline Tiles";
        turnCount = tm.currentTurn + 1;
    }

    private void Update()
    {
        totalCurrencytxt.text = ": " + currency_Total.ToString();
        totalCopperTxt.text = ": " + copper_Total.ToString();
        totalFoodTxt.text = ": " + food_Total.ToString();
        totalIronTxt.text = ": " + iron_Total.ToString();

        if (tm.currentTurn == turnCount)
        {
            shareCost = tradeM.genShareCost();
            addTurnNetworthArray();
            turnNetworth = 0;

            if (food_Total <= 0)
            {
                turnsTillDeath--;
                string t = "Warning the food storage is low, please gain above 30 to stay aflot. You have " + turnsTillDeath + " till you lose.";
                gm.displayInfo(t);                
            }
            else if(foodTotal >= 30 && turnsTillDeath <= 2)
            {
                turnsTillDeath = 2;
            }

            if(questInt == MAXQUESTINT)
            {
                questComplete = true;
            }

            turnCharge();
            turnCount++;

            if (turnsTillDeath == 0)
            {
                gm.loseOfFoodChange();
            }
        }
    }

    public void addTurnNetworthArray()
    {
        turnArray.Add(tm.currentTurn);
        networthArray.Add(turnNetworth);
    }

    public List<int> getTurnArray()
    {
        return turnArray;
    }
    
    public List<int> getNetworthArray()
    {
        return networthArray;
    }

    public void setOutlineTile(PlayerCompanyBuild pb)
    {     
        foreach (Node N in pb.selectableNodes)
        {
            GameObject o = Instantiate(outlinePrefab, N.transform.position, Quaternion.Euler(0, 0, 0));
            o.transform.SetParent(outlineTile.transform);
            outlineObjects.Add(o);
        }
    }

    public void deleteOutline()
    {
        Destroy(GameObject.Find("Outline Tiles"));
        outlineObjects.Clear();
    }

    public void createOutlinePerant()
    {
        outlineTile = new GameObject();
        outlineTile.name = "Outline Tiles";
    }

    public void buyingShares(int compID)
    {
        sharesOwned.Add(compID);
    }

    public int amountShares(int compID)
    {
        int num = 0;
        foreach (int i in sharesOwned)
        {
            if(i == compID)
            {
                num++;
            }
        }

        return num;
    }

    public void loseShare(GameObject comp)
    {
        numOfShares -= 1;
        shares.Add(comp);
    }

    public void addShare(GameObject comp)
    {
        numOfShares += 1;
        shares.Remove(comp);
    }

    public int getShareCost()
    {
        shareCost = tradeM.genShareCost();
        return shareCost;
    }

    public void addTimeCost(int a)
    {
        TimeCost.Add(a);
    }

    public void clearTimeCost()
    {
        TimeCost.Clear();
    }

    public void removeATimeCost(int a)
    {
        TimeCost.Remove(a);
    }

    public int getTimeCostTotal()
    {
        int time = 0;

        foreach (int i in TimeCost)
        {
            time = time + i;
        }
        return time;
    }

    public double getNetWorth()
    {
        return netWorth;
    }

    public void setStartingNetWorth()
    {
        netWorth -= netWorth * 2;
        turnNetworth = (int)netWorth;
    }

    public void setInfo()
    {
        comNameText = comProfile.Find("Company_Name");
        txt = comNameText.GetComponent<Text>();
        txt.text = "Company Name: " + companyName; 

        comCEOText = comProfile.Find("Name_of_CEO");
        txt = comCEOText.GetComponent<Text>();
        txt.text = "Name of CEO: " + companyCEOName;

        planetText = comProfile.transform.Find("Base_Planet");
        txt = planetText.GetComponent<Text>();
        txt.text = "Base Planet: " + planetName;

        typeText = comProfile.Find("Type_of_Company");
        txt = typeText.GetComponent<Text>();
        txt.text = "Type of Company: " + companyType;

        strengthText = comProfile.Find("Companys_Strength");
        txt = strengthText.GetComponent<Text>();
        txt.text = "Companys Strength: " + companyStrength;

        weaknessText = comProfile.Find("Companys_Weakness");
        txt = weaknessText.GetComponent<Text>();
        txt.text = "Companys Weekness: " + companyWeakness;

        portraitImage = comProfile.Find("Character_Portrait");

        switch (company_Portait)
        {
            case 1:
                portraitImage.GetComponent<Image>().sprite = charactarPort[0];
                break;
            case 2:
                portraitImage.GetComponent<Image>().sprite = charactarPort[1];
                break;
            case 3:
                portraitImage.GetComponent<Image>().sprite = charactarPort[2];
                break;
            case 4:
                portraitImage.GetComponent<Image>().sprite = charactarPort[3];
                break;
            default:
                portraitImage.GetComponent<Image>().sprite = charactarPort[Random.Range(0, charactarPort.Length)];
                break;
        }

        switch (companyType)
        {
            case "Quest: Gain Iron":
                questChar = 'I';
                break;
            case "Quest: Gain Copper":
                questChar = 'C';
                break;
            case "Quest Gain Food":
                questChar = 'F';
                break; 
        }

        pi.setStrAndWeak(); 
    }

    public Sprite getPortrait()
    {
        return portraitImage.GetComponent<Image>().sprite;
    }

    public void setTotal(int g, char t)
    {
        
        switch (t)
        {
            case 'I':
                iron_Total += g;
                if (g > 0)
                {
                    netWorth += mm.getIronCost() * g;
                    turnNetworth += mm.getIronCost() * g;
                }
                else
                {
                    netWorth -= mm.getIronCost() * g;
                    turnNetworth -= mm.getIronCost() * g;
                }
                if (questChar == 'I')
                    questInt += g;
                break;
            case 'C':
                copper_Total += g;
                if (g > 0)
                {
                    netWorth += mm.getIronCost() * g;
                    turnNetworth += mm.getIronCost() * g;
                }
                else
                {
                    netWorth -= mm.getIronCost() * g;
                    turnNetworth -= mm.getIronCost() * g;
                }
                if (questChar == 'C')
                    questInt += g;
                break;
            case 'F':
                food_Total += g;
                if (g > 0)
                {
                    netWorth += mm.getIronCost() * g;
                    turnNetworth += mm.getIronCost() * g;
                }
                else
                {
                    netWorth -= mm.getIronCost() * g;
                    turnNetworth -= mm.getIronCost() * g;
                }
                if (questChar == 'F')
                    questInt += g;
                break;
            case 'M':
                currency_Total += g;
                netWorth += g;
                turnNetworth += g;
                break;
        }
    }

    void turnCharge()
    {
        if(food_Total > 0)
            food_Total -= 5;
    }
}
