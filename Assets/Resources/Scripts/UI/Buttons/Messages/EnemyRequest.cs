using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyRequest : MonoBehaviour
{
    public int turnsToComplet;
    EnemyCompany ec;
    TurnManagemet tm;
    PlayerCompany pc;
    Text turnDesplay;
    int turnCount;
    public bool complete;

    public int askedAmount = 0;
    int costGiven = 0;
    public char rtype;

    // Start is called before the first frame update
    void Start()
    {
        if (tm == null)
            tm = GameObject.FindGameObjectWithTag("Manager").GetComponent<TurnManagemet>();

        if (pc == null)
            pc = GameObject.FindGameObjectWithTag("Manager").GetComponent<PlayerCompany>();

        turnDesplay = gameObject.transform.Find("Turns").GetComponent<Text>();
        turnCount = tm.currentTurn + 1;
        complete = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(tm.currentTurn == turnCount)
        {
            turnsToComplet--;

            if (turnsToComplet == 0)
            {
                turnDesplay.text = "Last Turn";
            }
            else
            {
                turnDesplay.text = turnsToComplet.ToString();
            }
            turnCount++;
        }

        if (complete)
        {
            switch (rtype)
            {
                case 'F':
                    pc.setTotal(askedAmount, 'F');
                    break;
                case 'C':
                    pc.setTotal(askedAmount, 'C');
                    break;
                case 'I':
                    pc.setTotal(askedAmount, 'I');
                    break;
            }

            pc.setTotal(costGiven, 'M');
            ec.changeHostility(20);
            Destroy(gameObject);
        }

        if(turnsToComplet == -1)
        {
            ec.changeHostility(-20);
            Destroy(gameObject);
        }
    }

    public void setInfo(int turn, EnemyCompany e, int amount, int cost, char type)
    {
        turnsToComplet = turn;
        ec = e;
        askedAmount = amount;
        costGiven = cost;
        rtype = type;
    }
}
