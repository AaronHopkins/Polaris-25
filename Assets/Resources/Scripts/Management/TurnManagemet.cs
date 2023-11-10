using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnManagemet : MonoBehaviour
{

    static Dictionary<string, List<CompanyActions>> companies = new Dictionary<string, List<CompanyActions>>();
    public static Queue<string> turns = new Queue<string>();
    public static List<CompanyActions> com = new List<CompanyActions>();

    List<EnemyCompany> enemies = new List<EnemyCompany>();

    GameManager gameM;
    PlayerCompany pc;
    public int turnHours;
    int timeCost;

    [SerializeField] Transform turnNum;
    [SerializeField] Transform turnHourBG;
    [SerializeField] Canvas canvas;
    [SerializeField] CanvasGroup T;
    
    Text turnTxt;
    Text turnHourText;
    public int currentTurn = 0;
    Transform topHud;

    void Start()
    {
        gameM = gameObject.GetComponent<GameManager>();
        pc = gameObject.GetComponent<PlayerCompany>();

        if (canvas == null)
        {
            canvas = (Canvas)GameObject.FindObjectsOfType(typeof(Canvas))[1];
        }


        if (turnNum == null)
        {
            topHud = canvas.transform.Find("Top_HUD");
            turnNum = topHud.transform.Find("Turn_Background");
            turnHourBG = turnNum.transform.Find("TurnHour_bg");
            turnHourText = turnHourBG.Find("TurnHour_txt").GetComponent<Text>();
            T = turnNum.GetComponent<CanvasGroup>();
        }

        turnTxt = turnNum.Find("CurrentTurn_txt").GetComponent<Text>();
    }

    void Update()
    {
        if (gameM.gameStart == true)
        {
            if (com.Count == 0)
            {
                comTurnQueue();
            }

            if (turns.Peek() == "Player Company")
            {

            }
            else if (turns.Peek() == "Enemy Company")
            {

            }

            T.alpha = 1;
            T.interactable = true;
            T.blocksRaycasts = true;
            turnTxt.text = "Turn: " + currentTurn;
            turnHourText.text = (turnHours - timeCost) + " Hours";
        }
        
    }

    public void addEnemy(EnemyCompany e)
    {
        enemies.Add(e);
    }

    public void addCompnay(CompanyActions unit)
    {
        List<CompanyActions> list;
        if (!companies.ContainsKey(unit.tag))
        {
            list = new List<CompanyActions>();
            companies[unit.tag] = list;

            if (!turns.Contains(unit.tag))
            {
                turns.Enqueue(unit.tag);
            }
        }
        else
        {
            list = companies[unit.tag];
        }

        list.Add(unit);
    }

    void comTurnQueue()
    {
        if(currentTurn == 0)
        {
            endTurn();
        }

        List<CompanyActions> ComList = companies[turns.Peek()];

        foreach (CompanyActions c in ComList)
        {
            com.Add(c);
        }

        startTurn();
    }

    public void startTurn()
    {
        if (com.Count > 0)
        {
            foreach (CompanyActions c in com)
            {
                if(turns.Peek() == "Player Company")
                {
                    timeCost = pc.getTimeCostTotal();
                }
                else
                {
                    foreach(EnemyCompany i in enemies)
                    {
                        i.turnHours = 24 - i.getTimeCostTotal();
                    }
                }

                c.beginTurn();
            }
        }
    }

    public void reloadMap()
    {
        companies.Clear();
        turns.Clear();
        com.Clear();
    }

    public void endTurn()
    {
        foreach (CompanyActions c in com.ToArray())
        {
            if (c.idle == false)
            {
                c.endTurn();
                com.Remove(c);
            }
        }

        if (com.Count > 0)
        {
            startTurn();
        }
        else
        {
            if(turns.Peek() == "Enemy Company")
            {
                currentTurn++;
                turnTxt.text = "Turn: " + currentTurn;
            }
            string Com = turns.Dequeue();
            turns.Enqueue(Com);
            comTurnQueue();
        }
    }
}
