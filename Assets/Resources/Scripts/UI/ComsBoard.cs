using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComsBoard : MonoBehaviour
{
    [SerializeField] GameObject message;
    [SerializeField] Transform messagepos;
    [SerializeField] Transform messagepos2;
    [SerializeField] Transform comsProfile;

    Vector3 mpos;
    Vector3 mpos2;
    TurnManagemet tm;
    GameManager gm;

    int messageCount = 0;
    int acceptedMessages = 0;
    int declinedMessages = 0;

    int turnCount;

    // Start is called before the first frame update
    void Start()
    {
        if (tm == null)
            tm = GameObject.FindGameObjectWithTag("Manager").GetComponent<TurnManagemet>();
        if (gm == null)
            gm = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        turnCount = tm.currentTurn + 1;
    }

    
    void Update()
    {
        mpos = messagepos.position;
        mpos2 = messagepos2.position;

        if (turnCount == tm.currentTurn)
        {
            messageCount = 0;
            turnCount++;
        }
    }

    public void addAccepted()
    {
        acceptedMessages++;
    }
    public void addDeclined()
    {
        declinedMessages++;
    }

    public void EnemyMessage(string comName, string ceoName, string textMessage, EnemyCompany e, int amount, int cost, char type)
    {
        GameObject mes;

        if (messageCount == 0)
        {
            mes = Instantiate(message, mpos, Quaternion.identity);
            mes.transform.SetParent(comsProfile);
            mes.transform.Find("NameCompany_txt").GetComponent<Text>().text = comName + " : " + ceoName; ;
            mes.transform.Find("Message_txt").GetComponent<Text>().text = textMessage;
            mes.SetActive(true);
            mes.transform.localScale = new Vector3(1, 1, 1);
            mes.GetComponent<EnemyMessage>().setInfo(comName, ceoName, textMessage, e, amount, cost, type);
            mpos = mes.transform.position;
        }
        else if (messageCount < 4)
        {
            Vector3 pos = new Vector3(mpos.x, (float)(mpos.y + -2.28), mpos.z);
            mes = Instantiate(message, pos, Quaternion.identity);
            mes.transform.SetParent(comsProfile);
            mes.transform.Find("NameCompany_txt").GetComponent<Text>().text = comName + " : " + ceoName; ;
            mes.transform.Find("Message_txt").GetComponent<Text>().text = textMessage;
            mes.SetActive(true);
            mes.transform.localScale = new Vector3(1, 1, 1);
            mes.GetComponent<EnemyMessage>().setInfo(comName, ceoName, textMessage, e, amount, cost,type);
            mpos = mes.transform.position;
        }
        else if(messageCount == 4)
        {
            Vector3 pos = new Vector3(mpos2.x, (float)(mpos2.y), mpos2.z);
            mes = Instantiate(message, pos, Quaternion.identity);
            mes.transform.SetParent(comsProfile);
            mes.transform.Find("NameCompany_txt").GetComponent<Text>().text = comName + " : " + ceoName; ;
            mes.transform.Find("Message_txt").GetComponent<Text>().text = textMessage;
            mes.SetActive(true);
            mes.transform.localScale = new Vector3(1, 1, 1);
            mes.GetComponent<EnemyMessage>().setInfo(comName, ceoName, textMessage, e, amount, cost,type);
            mpos2 = mes.transform.position;
        }
        else if (messageCount > 4 && messageCount < 8)
        {
            Vector3 pos = new Vector3(mpos2.x, (float)(mpos2.y + -2.28), mpos2.z);
            mes = Instantiate(message, pos, Quaternion.identity);
            mes.transform.SetParent(comsProfile);
            mes.transform.Find("NameCompany_txt").GetComponent<Text>().text = comName + " : " + ceoName; ;
            mes.transform.Find("Message_txt").GetComponent<Text>().text = textMessage;
            mes.SetActive(true);
            mes.transform.localScale = new Vector3(1, 1, 1);
            mes.GetComponent<EnemyMessage>().setInfo(comName, ceoName, textMessage, e, amount, cost,type);
            mpos2 = mes.transform.position;
        }

        messageCount++;
    }
}
