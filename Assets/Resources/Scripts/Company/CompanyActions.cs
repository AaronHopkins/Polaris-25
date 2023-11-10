using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CompanyActions : MonoBehaviour
{

    public bool idle;
    public bool turn;

    public List<ResourceTile> upgradedRescource = new List<ResourceTile>();
    public List<ResourceTile> useableResource = new List<ResourceTile>();
    public List<Node> selectableNodes = new List<Node>();
    private TurnManagemet turnM;
    public int range;

    public void initCA()
    {
        turnM = GameObject.FindGameObjectWithTag("Manager").transform.GetComponent<TurnManagemet>();
        turnM.addCompnay(this);
        idle = false;
    }

    void Update()
    {
        
    }

    public void findSelectableNodes(string tag)
    {
        Collider2D[] collidersHit = Physics2D.OverlapCircleAll(transform.position , range);
        useableResource.Clear();
        foreach (Collider2D item in collidersHit)
        {
            
            Node N = item.GetComponent<Node>();
            if (N != null)
            {
                if (N.tag == "Player Company" || N.tag == "Enemy Company")
                {

                }
                else
                {
                    switch (tag)
                    {
                        case "Player Company":
                            N.selectableTile = true;
                            N.setUpgradable();
                            if (N.tag == "Resource")
                                useableResource.Add(N.gameObject.GetComponent<ResourceTile>());
                            selectableNodes.Add(N);
                            break;
                        case "Enemy Company":
                            if (N.tag == "Resource")
                            {
                                useableResource.Add(N.gameObject.GetComponent<ResourceTile>());
                            }

                            selectableNodes.Add(N);
                            break;
                    }

                }
            }
        }

    }

    public void addNewSelectableNodes(List<Node> n)
    {
        foreach (Node i in n)
        {
            selectableNodes.Add(i);
        }
    }

    public void beginTurn()
    {
        turnM.turnHours = 24;
        turn = true;
        idle = true;
    }

    public void endTurn()
    {
        turn = false;
        idle = false;
    }
}
