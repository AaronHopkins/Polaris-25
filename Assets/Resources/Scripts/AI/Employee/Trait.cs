using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trait : MonoBehaviour
{
    private const int MAXTRAITAMOUNT = 4;
    private int traitNum;
    private string description;


    public string getDesc()
    {
        return description;
    }

    public void randTrait()
    {
        traitNum = Random.Range(1,MAXTRAITAMOUNT);

        switch (traitNum)
        {
            case 1:
                description = "+10 to r1";
                break;
            case 2:
                description = "+10 to r2";
                break;
            case 3:
                description = "+10 to l1";
                break;
            case 4:
                description = "+10 to l2";
                break;
            default:
                Debug.Log("--RandTrait ERROR--");
                break;
        }
            
    }
}
