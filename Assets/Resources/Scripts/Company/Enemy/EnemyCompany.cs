using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyCompany : CompanyActions
{
    [SerializeField] Transform enemyProfile;

    TerrainGeneration tg;
    TradeManager tradeM;
    EnemyActions ea;
    EnemyInfo ei;
    EnemyIncome eIncome;
    TurnManagemet tm;

    Transform comNameText;
    Transform comCEOText;
    Transform planetText;
    Transform typeText;
    Transform strengthText;
    Transform portraitImage;
    Transform weaknessText;
    Transform favorText;

    int turnCount;

    public int playerhostility = 0;
    public bool selected = false;

    private int foodCost;
    private int ironCost;
    private int copperCost;

    bool friend = false;
    bool bought = false;

    GameObject companyOwner;

    int numOfShares = 10;
    List<GameObject> shares = new List<GameObject>();
    List<int> sharesOwned = new List<int>();
    int shareCost;

    Text txt;

    List<int> TimeCost = new List<int>();
    public int turnHours;

    //Resorce Total
    private int foodTotal;
    public int food_Total { get => foodTotal; set => foodTotal = value; }

    private int livestockTotal;
    public int livestock_Total { get => livestockTotal; set => livestockTotal = value; }

    private int ironTotal;
    public int iron_Total { get => ironTotal; set => ironTotal = value; }

    private int copperTotal;
    public int copper_Total { get => copperTotal; set => copperTotal = value; }

    private int steelTotal;
    public int steel_Total { get => steelTotal; set => steelTotal = value; }

    private int currencyTotal;
    public int currency_Total { get => currencyTotal; set => currencyTotal = value; }

    //Company Info
    public int companyID;

    [SerializeField] private string companyName;
    public string company_Name
    { get => companyName; set => companyName = value; }

    [SerializeField] private string companyCEOName;
    public string company_CEOName
    { get => companyCEOName; set => companyCEOName = value; }

    private string planetName;
    public string planet_Name
    { get => planetName; set => planetName = value; }

    private string companyType;
    public string company_Type
    { get => companyType; set => companyType = value; }

    private string companyStrength;
    public string company_Strength
    { get => companyStrength; set => companyStrength = value; }

    private string companyWeakness;
    public string company_Weakness
    { get => companyWeakness; set => companyWeakness = value; }

    private Sprite companyPortait;
    public Sprite company_Portait
    { get => companyPortait; set => companyPortait = value; }


    private void Awake()
    {
        GameObject manager  = GameObject.FindGameObjectWithTag("Manager");

        if (enemyProfile == null)
            enemyProfile = GameObject.FindGameObjectWithTag("Enemy Profile").transform.Find("Background");

        if (tg == null)
            tg = manager.GetComponent<TerrainGeneration>();

        if (ea == null)
            ea = manager.GetComponent<EnemyActions>();

        if (ei == null)
            ei = manager.GetComponent<EnemyInfo>();

        if (tm == null)
            tm = manager.GetComponent<TurnManagemet>();

        if (tradeM == null)
            tradeM = manager.GetComponent<TradeManager>();

        if (eIncome == null)
            eIncome = gameObject.GetComponent<EnemyIncome>();

        companyPortait = ei.getEnPort();
        companyCEOName = ei.getComCEO();
        companyName = ei.getComName();
        companyType = ei.getEnType();
        companyStrength = ei.getEnStr();
        companyWeakness = ei.getEnWeak();
        companyID = ei.getID();
        companyOwner = gameObject;

        ei.addCompany(gameObject);
        sortResourcesCost();
        eIncome.setStrAndWeak(this);
        tm.addEnemy(this);
    }

    private void Start()
    {
        currencyTotal = 4000;
        ironTotal = 30;
        copperTotal = 15;
        foodTotal = Random.Range(30, 50);
        turnHours = 24;
        range = 4;
        turnCount = tm.currentTurn + 1;
        playerhostility = 0;
    }

    private void Update()
    {
        if (idle)
        {
            if (tm.currentTurn == turnCount)
            {
                findSelectableNodes(gameObject.tag);
                sortResourcesCost();
                turnCount++;
            }

            /////////////////////////////Looking for resources and building hubs////////////////////////////////////
            if (useableResource.Count > 0)
            {
                if (ea.checkForResources('C', useableResource, this))
                {
                    if (ea.CheckCost(this, 'C'))
                    {
                        ea.upgradeTile('C', useableResource, tg.planetInt, this);
                    }
                }
                else if (ea.checkForResources('I', useableResource, this))
                {
                    if (ea.CheckCost(this, 'I'))
                    {
                        ea.upgradeTile('I', useableResource, tg.planetInt, this);
                    }
                }
                else if (ea.checkForResources('F', useableResource, this))
                {
                    if (ea.CheckCost(this, 'F'))
                    {
                        ea.upgradeTile('F', useableResource, tg.planetInt, this);
                    }
                }
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////
            
            /////////////////////////////Buying From The Market if a resource is low///////////////////////////////
            if (copperTotal <= 10)
            {
                if (ea.CheckMarket('C', 5 , this))
                {
                    copperTotal += ea.buyFromMarket(5, 'C');
                }
                else
                {
                    ea.sendMessageRequest(companyCEOName, companyName, "copper", 5, copperCost * 12, this);
                }
            }

            if (ironTotal <= 10)
            {
                if(ea.CheckMarket('I', 10 , this))
                {
                    ironTotal += ea.buyFromMarket(10, 'I');
                }
                else
                {
                    ea.sendMessageRequest(companyCEOName, companyName, "iron", 10, ironCost * 12, this);
                }
            }

            if (foodTotal <= 20)
            {
                if(ea.CheckMarket('F', 30 , this))
                {
                    foodTotal += ea.buyFromMarket(30, 'F');
                }
                else
                {
                    ea.sendMessageRequest(companyCEOName, companyName, "food", 30, foodCost * 12, this);
                }
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////

            /////////////////////////////Selling on the market if a resource is High///////////////////////////////
            if (copperTotal > 20)
            {
                int amount = copperTotal - 20;
                currencyTotal += ea.sellOnMarket(amount, 'C');
                copperTotal -= amount;
            }

            if(ironTotal > 20)
            {
                int amount = ironTotal - 20;
                currencyTotal += ea.sellOnMarket(amount, 'I');
                ironTotal -= amount;
            }

            if(foodTotal > 50)
            {
                int amount = foodTotal - 50;
                currencyTotal += ea.sellOnMarket(amount, 'F');
                foodTotal -= amount;
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////

            if (useableResource.Count == 0)
            {
                checkOtherCompSharesCost();
            }

            if (playerhostility > 70)
            {
                //will be used as an ally if there is fight?? stage to stuff maybe
                friend = true;
            }
            else
            {
                friend = false;
            }


            turnHours = 0;
            if (turnHours == 0)
            {
                idle = false;
                tm.endTurn();
            }            
        }
        
        if (bought)
        {
            setNewInfo();
            foreach (ResourceTile i in upgradedRescource)
            {
                i.enemyOwner = companyOwner.GetComponent<EnemyCompany>();
                gameObject.GetComponent<SpriteRenderer>().sprite = tg.companyBought();
            }

            bought = false;
        }
    }

    public int getShareLeft()
    {
        return numOfShares;
    }

    public void buyingShares(int compID)
    {
        sharesOwned.Add(compID);
    }

    public int amountSharesLost(int compID)
    {
        int num = 0;
        
        foreach (GameObject i in shares)
        {
            if (i.tag == "Player Company")
            {
                if (GameObject.FindGameObjectWithTag("Manager").GetComponent<PlayerCompany>().companyID == compID)
                {
                    num++;
                }
            }
            else 
            { 
                if (i.GetComponent<EnemyCompany>().companyID == compID)
                {
                    num++;
                }
            }
        }

        return num;
    }

    public int amountSharesBought(int compID)
    {
        int num = 0;
        foreach (int i in sharesOwned)
        {
            if (i == compID)
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
        int num = 0;
        if(comp.tag == "Player Company")
        {
            num = amountSharesLost(GameObject.FindGameObjectWithTag("Manager").GetComponent<PlayerCompany>().companyID);
        }
        else
        {
            num = amountSharesLost(comp.GetComponent<EnemyCompany>().companyID);
        }

        if (num == 10) 
        {
            idle = false;
            bought = true;
            companyOwner = comp;
        }
    }

    public void addShare(GameObject comp)
    {
        numOfShares += 1;
        shares.Remove(comp);
    }

    void checkOtherCompSharesCost()
    {
        GameObject com = ei.checkSharesCost(this);

        if (com == null)
        {
            return;
        }

        if(com.tag == "Player Company" && friend == true)
        {
            return;
        }

        if(com.tag == "Player Company")
        {
            currency_Total -= com.GetComponent<PlayerCompany>().getShareCost();
            buyingShares(com.GetComponent<PlayerCompany>().companyID);
            com.GetComponent<PlayerCompany>().loseShare(gameObject);
        }
        else
        {
            currency_Total -= com.GetComponent<EnemyCompany>().getShareCost();
            buyingShares(com.GetComponent<EnemyCompany>().companyID);
            com.GetComponent<EnemyCompany>().loseShare(gameObject);
        }

    }

    public void addUpgradedRes(ResourceTile r)
    {
        upgradedRescource.Add(r);
    }

    public void removeUseableRes(ResourceTile r)
    {
        useableResource.Remove(r);
    }

    public void changeHostility(int i)
    {
        playerhostility = playerhostility + i;
    }

    public int getHostility()
    {
        return playerhostility;
    }

    public int getID()
    {
        return companyID;
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
        shareCost = tradeM.genShareCost();
        return shareCost;
    }

    public void setFoodCost(int c)
    {
        foodCost += c;
    }

    public void setIronCost(int c)
    {
        ironCost += c;
    }

    public void setCopperCost(int c)
    {
        copperCost += c;
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

    public void setNewInfo()
    {
        if(companyOwner.tag == "Player Company")
        {
            PlayerCompany p = GameObject.FindGameObjectWithTag("Manager").GetComponent<PlayerCompany>();

            companyPortait = p.getPortrait();
            companyCEOName = p.company_CEOName;
            companyName = p.company_Name;
            companyType = p.company_Type;
            companyStrength = p.company_Strength;
            companyWeakness = p.company_Weakness;
            companyID = p.companyID;
        }
        else
        {
            EnemyCompany e = companyOwner.GetComponent<EnemyCompany>();
            
            companyPortait = e.company_Portait;
            companyCEOName = e.company_CEOName;
            companyName = e.company_Name;
            companyType = e.company_Type;
            companyStrength = e.company_Strength;
            companyWeakness = e.company_Weakness;
            companyID = e.companyID;
        }




        companyOwner.GetComponent<CompanyActions>().addNewSelectableNodes(selectableNodes);
    }

    public void setProfileInfo()
    {
        comNameText = enemyProfile.Find("Company_Name");
        txt = comNameText.GetComponent<Text>();
        txt.text = "Company Name: " + companyName;

        comCEOText = enemyProfile.Find("Name_of_CEO");
        txt = comCEOText.GetComponent<Text>();
        txt.text = "Name of CEO: " + companyCEOName;

        planetText = enemyProfile.transform.Find("Base_Planet");
        txt = planetText.GetComponent<Text>();
        txt.text = "Base Planet: " + planetName;

        typeText = enemyProfile.Find("Type_of_Company");
        txt = typeText.GetComponent<Text>();
        txt.text = "Type of Compnay: " + companyType;

        strengthText = enemyProfile.Find("Companys_Strength");
        txt = strengthText.GetComponent<Text>();
        txt.text = "Companys Strength: " + companyStrength;

        weaknessText = enemyProfile.Find("Companys_Weakness");
        txt = weaknessText.GetComponent<Text>();
        txt.text = "Companys Weekness: " + companyWeakness;

        portraitImage = enemyProfile.Find("Character_Portrait");
        portraitImage.GetComponent<Image>().sprite = companyPortait;

        favorText = enemyProfile.Find("Companys_Favor");
        txt = favorText.GetComponent<Text>();
        txt.text = "Favor: " + playerhostility;
    }

    public void setTotal(int g, char t)
    {
        switch (t)
        {
            case 'I':
                iron_Total = iron_Total + g;
                break;
            case 'C':
                copper_Total = copper_Total + g;
                break;
            case 'F':
                food_Total = food_Total + g;
                break;
        }
    }

    public void setIncome(int g, char t)
    {
        eIncome.setIncome(g,t);
    }

    public void gainIncome(char type,  int mod)
    {
        eIncome.gainIncome(type, mod);
    }

    void sortResourcesCost() 
    {
        foodCost = tradeM.genFoodCost();
        copperCost = tradeM.genCopperCost();
        ironCost = tradeM.genIronCost();
        shareCost = tradeM.genShareCost();

        if (playerhostility <= 100 && playerhostility >= 70)
        {
            //high friendship
            foodCost -= (foodCost / 15);
            copperCost -= (copperCost / 15);
            ironCost -= (ironCost / 15);
            shareCost -= (shareCost / 15);
        }
        else if (playerhostility <= 69 && playerhostility >= 40) 
        {
            //normal frindship

            foodCost -= (foodCost / 5);
            copperCost -= (copperCost / 5);
            ironCost -= (ironCost / 5);
            shareCost -= (shareCost / 5);
        }
        else
        {
            //low friendship
            foodCost += (foodCost / 10);
            copperCost += (copperCost / 10);
            ironCost += (ironCost / 10);
            shareCost += (shareCost / 10);
        }

    }
}
