using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveButton : MonoBehaviour
{
    EnemyRequest er;
    PlayerCompany pc;

    // Start is called before the first frame update
    void Start()
    {
        if(er == null)
            er = gameObject.transform.parent.GetComponent<EnemyRequest>();

        if (pc == null)
            pc = GameObject.FindGameObjectWithTag("Manager").GetComponent<PlayerCompany>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClick()
    {
        switch (er.rtype)
        {
            case 'F':
                if (pc.food_Total > er.askedAmount)
                {
                    er.complete = true;
                }
                break;
            case 'C':
                if (pc.copper_Total > er.askedAmount)
                {
                    er.complete = true;
                }
                break;
            case 'I':
                if (pc.iron_Total > er.askedAmount)
                {
                    er.complete = true;
                }
                break;
        }
    }
}
