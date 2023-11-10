using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RequestBoard : MonoBehaviour
{
    [SerializeField] GameObject message;
    [SerializeField] Transform messagepos;
    [SerializeField] Transform messagepos2;
    [SerializeField] Transform reqProfile;

    int messageCount = 0;

    Vector3 mpos;
    Vector3 mpos2;
    TurnManagemet tm;

    int turnCount;

    // Start is called before the first frame update
    void Start()
    {
        if (tm == null)
            tm = GameObject.FindGameObjectWithTag("Manager").GetComponent<TurnManagemet>();
        turnCount = tm.currentTurn + 1;

        mpos = messagepos.position;
        mpos2 = messagepos2.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(tm.currentTurn == turnCount)
        {
            messageCount = 0;
            mpos = messagepos.position;
            mpos2 = messagepos2.position;
            turnCount++;
        }
    }

    public void EnemyMessage(string comName, string ceoName, string textMessage, int turns, EnemyCompany e, int amount, int cost, char type)
    {
        GameObject mes;

        if (messageCount == 0)
        {
            mes = Instantiate(message, mpos, Quaternion.identity);
            mes.transform.SetParent(reqProfile);
            mes.transform.Find("NameCompany_txt").GetComponent<Text>().text = comName + " : " + ceoName; ;
            mes.transform.Find("Message_txt").GetComponent<Text>().text = textMessage;
            mes.transform.Find("Turns").GetComponent<Text>().text = turns.ToString();
            mes.SetActive(true);
            mes.transform.localScale = new Vector3(1, 1, 1);
            mes.GetComponent<EnemyRequest>().setInfo(turns, e, amount, cost, type);
            mpos = mes.transform.position;
        }
        else if (messageCount < 4)
        {
            Vector3 pos = new Vector3(mpos.x, (float)(mpos.y + -2.28), mpos.z);
            mes = Instantiate(message, pos, Quaternion.identity);
            mes.transform.SetParent(reqProfile);
            mes.transform.Find("NameCompany_txt").GetComponent<Text>().text = comName + " : " + ceoName; ;
            mes.transform.Find("Message_txt").GetComponent<Text>().text = textMessage;
            mes.transform.Find("Turns").GetComponent<Text>().text = turns.ToString();
            mes.SetActive(true);
            mes.transform.localScale = new Vector3(1, 1, 1);
            mes.GetComponent<EnemyRequest>().setInfo(turns, e, amount, cost, type);
            mpos = mes.transform.position;
        }
        else if (messageCount == 4)
        {
            Vector3 pos = new Vector3(mpos2.x, (float)(mpos2.y), mpos2.z);
            mes = Instantiate(message, pos, Quaternion.identity);
            mes.transform.SetParent(reqProfile);
            mes.transform.Find("NameCompany_txt").GetComponent<Text>().text = comName + " : " + ceoName; ;
            mes.transform.Find("Message_txt").GetComponent<Text>().text = textMessage;
            mes.transform.Find("Turns").GetComponent<Text>().text = turns.ToString();
            mes.SetActive(true);
            mes.transform.localScale = new Vector3(1, 1, 1);
            mes.GetComponent<EnemyRequest>().setInfo(turns, e, amount, cost, type);
            mpos2 = mes.transform.position;
        }
        else if (messageCount > 4 && messageCount < 8)
        {
            Vector3 pos = new Vector3(mpos2.x, (float)(mpos2.y + -2.28), mpos2.z);
            mes = Instantiate(message, pos, Quaternion.identity);
            mes.transform.SetParent(reqProfile);
            mes.transform.Find("NameCompany_txt").GetComponent<Text>().text = comName + " : " + ceoName; ;
            mes.transform.Find("Message_txt").GetComponent<Text>().text = textMessage;
            mes.transform.Find("Turns").GetComponent<Text>().text = turns.ToString();
            mes.SetActive(true);
            mes.transform.localScale = new Vector3(1, 1, 1);
            mes.GetComponent<EnemyRequest>().setInfo(turns, e, amount, cost, type);
            mpos2 = mes.transform.position;
        }

        messageCount++;
    }

} 
