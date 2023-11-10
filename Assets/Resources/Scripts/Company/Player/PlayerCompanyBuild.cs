using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCompanyBuild : CompanyActions
{
    EnemyInfo ei;
    [SerializeField] PlayerCompany pc;

    private void Awake()
    {
        if (pc == null)
            pc = GameObject.FindGameObjectWithTag("Manager").GetComponent<PlayerCompany>();

        if (ei == null)
            ei = GameObject.FindGameObjectWithTag("Manager").GetComponent<EnemyInfo>();
    }

    void Start()
    {
        pc.playerBuilding = this;
        pc.companyPlayer = gameObject;
        range = 4;
        findSelectableNodes(gameObject.tag);
        ei.addCompany(gameObject);
        pc.setOutlineTile(this);
    }

    void Update()
    {
        if (idle)
        {
            //Debug.Log("play true");
        }
    }
}
