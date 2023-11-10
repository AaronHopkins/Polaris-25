using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public int gridX;
    public int gridy;
    public bool walkable;
    public bool upgradeAvailable;
    public bool selectableTile;
    public bool weatherActive;
    public string description;
    public int terrainType;
    public string terrainName;

    public float F;
    public float G;
    public float H;
    public int TargetX;
    public int TargetY;

    public List<Node> adjacencyList = new List<Node>();

    private void Start()
    {
        FindNeighbors(this);

        terrainType = Random.Range(0, 2);

        if(gameObject.tag == "Terrain")
        {
            switch (terrainType)
            {
                case 0:
                    terrainName = "Plains";
                    description = terrainName + ": An open, craggy field. Settlers here will find it easier with all the open space.";
                    break;
                case 1:
                    terrainName = "Canyon";
                    description = terrainName + ": The high density of rock cover hides this tile better than others. Attacks on this tile will be less frequent.";
                    break;
            }
        }

    }

    public void setUpgradable()
    {
        if (selectableTile && (gameObject.tag == "Terrain" || gameObject.tag == "Resource"))
        {
            upgradeAvailable = true;
        }
    }

    public void Reset()
    {
        adjacencyList.Clear();

        F = 0;
        G = 0;
        H = 0;
    }

    public void FindNeighbors(Node target)
    {
        Reset();

        CheckTile(Vector3.up, target);
        CheckTile(Vector3.down, target);
        CheckTile(Vector3.right, target);
        CheckTile(Vector3.left, target);
    }

    public void CheckTile(Vector3 direction, Node target)
    {
        Collider2D[] collidersHit = Physics2D.OverlapCircleAll(transform.position + direction, 0.2f);

        foreach (Collider2D item in collidersHit)
        {

            Node N = item.GetComponent<Node>();
            if (N != null)
            {
                RaycastHit hit;

                if (!Physics.Raycast(N.transform.position, Vector3.back, out hit, 1) || (N == target))
                {

                    adjacencyList.Add(N);
                }
            }
        }
    }

    public void SetF()
    {
        F = G + H;
    }

    public void SetTarget(int x, int y)
    {
        TargetX = x;
        TargetY = y;
        SetH();
        SetF();
    }

    public void SetH()
    {
        H = Mathf.Sqrt(Mathf.Pow((gridX - TargetX), 2) + Mathf.Pow((gridy - TargetY), 2));
    }
}
