using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMessage : MonoBehaviour
{
    [SerializeField] EnemyCompany ec;
    RequestBoard rb;
    TurnManagemet tm;
    public bool accepted;
    public bool declined;

    int askedAmount = 0;
    int costGiven = 0;
    char rtype = 'n';

    string cName;
    string personName;
    string message;

    int turnCount;

    // Start is called before the first frame update
    void Start()
    {
        if (rb == null)
            rb = GameObject.FindGameObjectWithTag("Manager").GetComponent<RequestBoard>();
        if (tm == null)
            tm = GameObject.FindGameObjectWithTag("Manager").GetComponent<TurnManagemet>();

        accepted = false;
        declined = false;
        turnCount = tm.currentTurn + 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(turnCount == tm.currentTurn)
        {
            Destroy(gameObject);
            turnCount++;
        }

        if (accepted)
        {
            rb.EnemyMessage(cName, personName, message, 3, ec, askedAmount, costGiven, rtype);
            Destroy(gameObject);
        }

        if (declined)
        {
            ec.changeHostility(-10);
            Destroy(gameObject);
        }
    }

    public void setInfo(string comName, string ceoName, string textMessage, EnemyCompany e, int amount, int cost, char type)
    {
        cName = comName;
        personName = ceoName;
        message = textMessage;
        ec = e;
        askedAmount = amount;
        costGiven = cost;
        rtype = type;
    }
}
