using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo : MonoBehaviour
{
    int enID;
    [SerializeField] string[] enComName;
    [SerializeField] List<string> usableComName;
    [SerializeField] string[] enComCEO;
    [SerializeField] string[] enType;
    [SerializeField] string[] enStrength;
    [SerializeField] string[] enWeakness;
    [SerializeField] Sprite[] charactarPort;
    [SerializeField] List<GameObject> companies = new List<GameObject>();

    private void Start()
    {
        refresh();
    }

    public int getID()
    {
        enID++;
        return enID;
    }

    public void addCompany(GameObject g)
    {
        companies.Add(g);
    }
    
    public void removeCompany(GameObject g)
    {
        companies.Remove(g);
    }

    public GameObject checkSharesCost(EnemyCompany e)
    {
        GameObject g = null;

        foreach (GameObject i in companies)
        {
            if (i.tag == "Player Company")
            {
                PlayerCompany p = GameObject.FindGameObjectWithTag("Manager").GetComponent<PlayerCompany>();
                if (p.getShareCost() <= e.currency_Total)
                {
                    g = i;
                    return g;
                }
            }
            else
            {
                if (i.GetComponent<EnemyCompany>().getShareCost() <= e.currency_Total)
                {
                    g = i;
                    return g;
                }
            }
        }
        return g;
    }

    public string getComName()
    {
        string name = usableComName[Random.Range(0, usableComName.Count)];
        usableComName.Remove(name);
        return name;
    }
    
    public string getComCEO()
    {
        return enComCEO[Random.Range(0, enComCEO.Length)];
    }

    public string getEnType()
    {
        return enType[Random.Range(0, enType.Length)];
    }
    
    public string getEnStr()
    {
        return enStrength[Random.Range(0, enStrength.Length)];
    } 
    
    public string getEnWeak()
    {        
        return enWeakness[Random.Range(0,enWeakness.Length)];
    } 
    
    public Sprite getEnPort()
    {        
        return charactarPort[Random.Range(0,charactarPort.Length)];
    }
    
    public void refresh()
    {
        enID = 0;
        usableComName.Clear();
        companies.Clear();
        foreach(string i in enComName)
        {
            usableComName.Add(i);
        }
    }
}
