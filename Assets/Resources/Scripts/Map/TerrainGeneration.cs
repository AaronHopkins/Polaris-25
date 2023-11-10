using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public class TerrainGeneration : MonoBehaviour
{
    [Range(0, 100)]
    public int iniChance;
    [Range(1, 8)]
    public int birthLimit;
    [Range(1, 8)]
    public int deathLimit;

    [Range(1, 10)]
    public int numR;

    private int[,] terrainMap;
    public Vector3Int tmpSize;
    public int miniralAvrage;
    public int farmableAvrage;

    public Tilemap topMap;
    public Tilemap botMap;
    public Tilemap midMap;
    public Tilemap companyMap;

    public Tile topTile;
    public Tile midTile;
    public Tile botTile;
    public Tile bCTile;
    public Tile sCTile;
    public Tile pCTile;

    public GameObject[] nodePrefab;
    public GameObject[] mountinPrefab;
    public GameObject[] ironPrefab;
    public GameObject[] copperPrefab;
    public GameObject[] farmablePrefab;
    public GameObject[] housingPrefab;
    public GameObject[] smallCompanyPrefab;
    public GameObject[] bigCompanyPrefab;
    public GameObject[] playerCompanyPrefab;

    [SerializeField] Sprite[] boughtCompany;

    public List<GameObject> nodes;
    private GameObject gridOfNodes;
    public string currentPlanet;

    public int planetInt;

    int maxBigCompanies;
    int maxSmallCompanies;
    int maxResorcesAmount;

    public int maxTurns = 0;

    int sCAmount = 0;
    int bCAmount = 0;

    private int countR = 0;
    private int count = 1;

    int width;
    int height;

    GameObject playerCompanyTile;
    GameObject bigCompanyTiles;
    GameObject smallCompanyTiles;
    GameObject terrainTiles;
    GameObject resourceTiles;
    GameObject mountinTiles;

    public bool mapActive = false;
    public bool pCplaced = false;
    public bool pCOnMap = false;

    Camera mainCam;

    private void Awake()
    {
        nodes = new List<GameObject>();
        mainCam = Camera.main;
    }

    public void setPCPlaced(bool i)
    {
        pCplaced = i;
    }

    public void deleteNode(GameObject g)
    {
        Destroy(g);
    }

    public void placePlayerCompany(int x, int y)
    {
        companyMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), pCTile);

        GameObject pC = (GameObject)Instantiate(playerCompanyPrefab[planetInt], new Vector3((-x + width / 2) + 0.5f, (-y + height / 2) + 0.5f, 0), Quaternion.Euler(0, 0, 0));
        PlayerCompanyTile p = pC.GetComponent<PlayerCompanyTile>();
        p.gridX = x;
        p.gridy = y;
        p.walkable = true;
        p.gameObject.GetComponent<PlayerCompanyBuild>().initCA();
        nodes.Add(pC);

        pC.name = "PlayerCompany " + x.ToString() + ":" + y.ToString();
        pC.transform.SetParent(playerCompanyTile.transform);
    }

    public void setPlanet(string planet)
    {
        currentPlanet = planet;

        switch (currentPlanet)
        {
            case "Tutorial: Moon":
                maxBigCompanies = 2;
                maxSmallCompanies = 6;
                tmpSize.x = 40;
                tmpSize.y = 30;
                maxResorcesAmount = 40;
                miniralAvrage = 70;
                farmableAvrage = 30;
                mainCam.orthographicSize = 20;
                planetInt = 0;
                maxTurns = 60;
                break;
            case "Normal: Mars":
                maxBigCompanies = 4;
                maxSmallCompanies = 12;
                tmpSize.x = 60;
                tmpSize.y = 50;
                maxResorcesAmount = 80;
                miniralAvrage = 30;
                farmableAvrage = 70;
                mainCam.orthographicSize = 35;
                planetInt = 1;
                maxTurns = 90;
                break;
            case "Easy: Moon":
                maxBigCompanies = 2;
                maxSmallCompanies = 6;
                tmpSize.x = 40;
                tmpSize.y = 30;
                maxResorcesAmount = 40;
                miniralAvrage = 70;
                farmableAvrage = 30;
                mainCam.orthographicSize = 20;
                planetInt = 0;
                maxTurns = 60;
                break;
            case "Hard: Jupiter":
                maxBigCompanies = 6;
                maxSmallCompanies = 18;
                tmpSize.x = 90;
                tmpSize.y = 80;
                maxResorcesAmount = 160;
                miniralAvrage = 50;
                farmableAvrage = 50;
                mainCam.orthographicSize = 55;
                planetInt = 2;
                maxTurns = 120;
                break;
        }
        rerollMap();
    }


    public void rerollMap()
    {
        pCOnMap = false;
        sCAmount = 0;
        clearMap(true);
        doGen(numR);
    }

    public Sprite companyBought()
    {
        return boughtCompany[planetInt];
    }

    public void doGen(int nu)
    {
        clearMap(false);
        width = tmpSize.x;
        height = tmpSize.y;

        if (terrainMap == null)
        {
            terrainMap = new int[width, height];
            initPos();
        }


        for (int i = 0; i < nu; i++)
        {
            terrainMap = genTilePos(terrainMap);
        }

        int rCount = 0;
        int sCPlaced = 0;

        gridOfNodes = new GameObject();
        gridOfNodes.name = "Grid Of Nodes";

        mountinTiles = new GameObject();
        mountinTiles.transform.SetParent(gridOfNodes.transform);
        mountinTiles.name = "Mountin Tiles";

        resourceTiles = new GameObject();
        resourceTiles.transform.SetParent(gridOfNodes.transform);
        resourceTiles.name = "Resource Tiles";

        terrainTiles = new GameObject();
        terrainTiles.transform.SetParent(gridOfNodes.transform);
        terrainTiles.name = "Terrain Tiles";

        smallCompanyTiles = new GameObject();
        smallCompanyTiles.transform.SetParent(gridOfNodes.transform);
        smallCompanyTiles.name = "Small Company Tiles";

        bigCompanyTiles = new GameObject();
        bigCompanyTiles.transform.SetParent(gridOfNodes.transform);
        bigCompanyTiles.name = "Big Company Tiles";

        playerCompanyTile = new GameObject();
        playerCompanyTile.transform.SetParent(gridOfNodes.transform);
        playerCompanyTile.name = "Player Company Tiles";

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {

                if (terrainMap[x,y] == 2)
                {
                    if(rCount < 3 && sCPlaced < 3)
                    {
                        spawnR(x, y);
                        rCount++;
                    }
                    else if (rCount == 3)
                    {
                        spawnSC(x,y);
                        sCPlaced++;
                        rCount = 0;
                    }
                    else if (sCPlaced == 3)
                    {
                        spawnBC(x, y);
                        sCPlaced = 0;
                    }

           
                }
                else if (terrainMap[x, y] == 1)
                {
                    GameObject mountin;
                    int rand;

                    switch (planetInt)
                    {
                        case 0:
                            rand = Random.Range(planetInt, planetInt + 2);
                            mountin = (GameObject)Instantiate(mountinPrefab[rand], new Vector3((-x + width / 2) + 0.5f, (-y + height / 2) + 0.5f, 0), Quaternion.Euler(0, 0, 0));
                            break;
                        case 1:
                            rand = Random.Range(planetInt + 2, planetInt + 4);
                            mountin = (GameObject)Instantiate(mountinPrefab[rand], new Vector3((-x + width / 2) + 0.5f, (-y + height / 2) + 0.5f, 0), Quaternion.Euler(0, 0, 0));
                            break;
                        case 2:
                            rand = Random.Range(planetInt + 4, planetInt + 6);
                            mountin = (GameObject)Instantiate(mountinPrefab[rand], new Vector3((-x + width / 2) + 0.5f, (-y + height / 2) + 0.5f, 0), Quaternion.Euler(0, 0, 0));
                            break;
                        default:
                            rand = Random.Range(planetInt, planetInt + 2);
                            mountin = (GameObject)Instantiate(mountinPrefab[rand], new Vector3((-x + width / 2) + 0.5f, (-y + height / 2) + 0.5f, 0), Quaternion.Euler(0, 0, 0));
                            break;
                    }

                    topMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), topTile);
                    
                    MountinTile m = mountin.GetComponent<MountinTile>();
                    m.gridX = x;
                    m.gridy = y;
                    m.walkable = false;
                    nodes.Add(mountin);

                    mountin.name = "Mountin " + x.ToString() + ":" + y.ToString();
                    mountin.transform.SetParent(mountinTiles.transform);
                }
                else
                {
                    int rand;
                    GameObject node;

                    switch (planetInt)
                    {
                        case 0:
                            rand = Random.Range(planetInt, planetInt + 1);
                            node = (GameObject)Instantiate(nodePrefab[rand], new Vector3((-x + width / 2) + 0.5f, (-y + height / 2) + 0.5f, 0), Quaternion.Euler(0, 0, 0));
                            break;
                        case 1:
                            rand = Random.Range(planetInt + 1, planetInt + 2);
                            node = (GameObject)Instantiate(nodePrefab[rand], new Vector3((-x + width / 2) + 0.5f, (-y + height / 2) + 0.5f, 0), Quaternion.Euler(0, 0, 0));
                            break;
                        case 2:
                            rand = Random.Range(planetInt + 2, planetInt + 3);
                            node = (GameObject)Instantiate(nodePrefab[rand], new Vector3((-x + width / 2) + 0.5f, (-y + height / 2) + 0.5f, 0), Quaternion.Euler(0, 0, 0));
                            break;
                        default:
                            rand = Random.Range(planetInt, planetInt + 1);
                            node = (GameObject)Instantiate(nodePrefab[rand], new Vector3((-x + width / 2) + 0.5f, (-y + height / 2) + 0.5f, 0), Quaternion.Euler(0, 0, 0));
                            break;
                    }
                    
                    botMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), botTile);
                    
                    Node n = node.GetComponent<Node>();
                    n.gridX = x;
                    n.gridy = y;
                    n.walkable = true;
                    nodes.Add(node);

                    node.name = "Terrain " + x.ToString() + ":" + y.ToString();
                    node.transform.SetParent(terrainTiles.transform);
                }    
            }
        }

        
    }

    public void initPos()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                terrainMap[x, y] = Random.Range(1, 101) < iniChance ? 1 : 0;
            }
        }
    }

    public int[,] genTilePos(int[,] oldMap)
    {
        int[,] newMap = new int[width, height];
        int neighb;
        BoundsInt myB = new BoundsInt(-1, -1, 0, 3, 3, 1);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                neighb = 0;
                foreach (var b in myB.allPositionsWithin)
                {
                    if (b.x == 0 && b.y == 0) continue;
                    if (x + b.x >= 0 && x + b.x < width && y + b.y >= 0 && y + b.y < height)
                    {
                        neighb += oldMap[x + b.x, y + b.y];
                    }
                    else
                    {
                        neighb++;
                    }
                }

                if (oldMap[x, y] == 1)
                {
                    if (neighb < deathLimit) newMap[x, y] = 0;

                    else
                    {
                            newMap[x, y] = 1;
                    }
                }

                if (oldMap[x, y] == 0)
                {
                    if (neighb > birthLimit) newMap[x, y] = 1;

                    else
                    {
                        int rand = Random.Range(1, 30);

                        if (rand == 1 && countR < maxResorcesAmount)
                        {
                            newMap[x, y] = 2;
                            countR++;
                        }
                        else
                        {
                            newMap[x, y] = 0;
                        }                        
                    }
                }
            }
        }
        return newMap;
    }

    private void spawnR(int x, int y)
    {
        midMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), midTile);
        int rand = Random.Range(1, 100);
        GameObject resource;
        if (rand <= miniralAvrage)
        {
            if (count % 2 == 0)
            {
                resource = (GameObject)Instantiate(copperPrefab[planetInt], new Vector3((-x + width / 2) + 0.5f, (-y + height / 2) + 0.5f, 0), Quaternion.Euler(0, 0, 0));
                Copper r = resource.GetComponent<Copper>();
                r.gridX = x;
                r.gridy = y;
                r.walkable = true;
                r.genCopper();
                nodes.Add(resource);
                count++;
            }
            else
            {
                resource = (GameObject)Instantiate(ironPrefab[planetInt], new Vector3((-x + width / 2) + 0.5f, (-y + height / 2) + 0.5f, 0), Quaternion.Euler(0, 0, 0));
                Iron r = resource.GetComponent<Iron>();
                r.gridX = x;
                r.gridy = y;
                r.walkable = true;
                r.genIron();
                nodes.Add(resource);
                count++;
            }

        }
        else
        {
            
            resource = (GameObject)Instantiate(farmablePrefab[planetInt], new Vector3((-x + width / 2) + 0.5f, (-y + height / 2) + 0.5f, 0), Quaternion.Euler(0, 0, 0));
            Farmable r = resource.GetComponent<Farmable>();
            r.gridX = x;
            r.gridy = y;
            r.walkable = true;
            r.genFarm();
            nodes.Add(resource);
        }

        resource.name = "Resource " + x.ToString() + ":" + y.ToString();
        resource.transform.SetParent(resourceTiles.transform);
    }

    private void spawnSC(int x, int y) 
    {
        if (sCAmount < maxSmallCompanies)
        {
            companyMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), sCTile);

            GameObject sC = (GameObject)Instantiate(smallCompanyPrefab[planetInt], new Vector3((-x + width / 2) + 0.5f, (-y + height / 2) + 0.5f, 0), Quaternion.Euler(0, 0, 0));
            SmallCompanyTile s = sC.GetComponent<SmallCompanyTile>();
            s.gridX = x;
            s.gridy = y;
            s.walkable = true;
            s.GetComponent<CompanyActions>().initCA();
            nodes.Add(sC);

            sC.name = "SmallCompany " + x.ToString() + ":" + y.ToString();
            sC.transform.SetParent(smallCompanyTiles.transform);
            sCAmount++;
        }
        else
        {
            int rand;
            GameObject node;

            switch (planetInt)
            {
                case 0:
                    rand = Random.Range(planetInt, planetInt + 1);
                    node = (GameObject)Instantiate(nodePrefab[rand], new Vector3((-x + width / 2) + 0.5f, (-y + height / 2) + 0.5f, 0), Quaternion.Euler(0, 0, 0));
                    break;
                case 1:
                    rand = Random.Range(planetInt + 1, planetInt + 2);
                    node = (GameObject)Instantiate(nodePrefab[rand], new Vector3((-x + width / 2) + 0.5f, (-y + height / 2) + 0.5f, 0), Quaternion.Euler(0, 0, 0));
                    break;
                case 2:
                    rand = Random.Range(planetInt + 2, planetInt + 3);
                    node = (GameObject)Instantiate(nodePrefab[rand], new Vector3((-x + width / 2) + 0.5f, (-y + height / 2) + 0.5f, 0), Quaternion.Euler(0, 0, 0));
                    break;
                default:
                    rand = Random.Range(planetInt, planetInt + 1);
                    node = (GameObject)Instantiate(nodePrefab[rand], new Vector3((-x + width / 2) + 0.5f, (-y + height / 2) + 0.5f, 0), Quaternion.Euler(0, 0, 0));
                    break;
            }

            botMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), botTile);

            Node n = node.GetComponent<Node>();
            n.gridX = x;
            n.gridy = y;
            n.walkable = true;
            nodes.Add(node);

            node.name = "Terrain " + x.ToString() + ":" + y.ToString();
            node.transform.SetParent(terrainTiles.transform);
        }
    }

    private void spawnBC(int x,int y)
    {
        if (bCAmount < maxBigCompanies)
        {
            companyMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), bCTile);
            int rand;
            GameObject bC;

            switch (planetInt)
            {
                case 0:
                    rand = Random.Range(planetInt, planetInt + 1);
                    bC = (GameObject)Instantiate(bigCompanyPrefab[rand], new Vector3((-x + width / 2) + 0.5f, (-y + height / 2) + 0.5f, 0), Quaternion.Euler(0, 0, 0));
                    break;
                case 1:
                    rand = Random.Range(planetInt + 1, planetInt + 2);
                    bC = (GameObject)Instantiate(bigCompanyPrefab[rand], new Vector3((-x + width / 2) + 0.5f, (-y + height / 2) + 0.5f, 0), Quaternion.Euler(0, 0, 0));
                    break;
                case 2:
                    rand = Random.Range(planetInt + 2, planetInt + 3);
                    bC = (GameObject)Instantiate(bigCompanyPrefab[rand], new Vector3((-x + width / 2) + 0.5f, (-y + height / 2) + 0.5f, 0), Quaternion.Euler(0, 0, 0));
                    break;
                default:
                    rand = Random.Range(planetInt, planetInt + 1);
                    bC = (GameObject)Instantiate(bigCompanyPrefab[rand], new Vector3((-x + width / 2) + 0.5f, (-y + height / 2) + 0.5f, 0), Quaternion.Euler(0, 0, 0));
                    break;
            }

            BigCompanyTile b = bC.GetComponent<BigCompanyTile>();
            b.gridX = x;
            b.gridy = y;
            b.walkable = true;
            b.GetComponent<CompanyActions>().initCA();
            nodes.Add(bC);

            bC.name = "BigCompany " + x.ToString() + ":" + y.ToString();
            bC.transform.SetParent(bigCompanyTiles.transform);
            bCAmount++;
        }
        else
        {
            int rand;
            GameObject node;

            switch (planetInt)
            {
                case 0:
                    rand = Random.Range(planetInt, planetInt + 1);
                    node = (GameObject)Instantiate(nodePrefab[rand], new Vector3((-x + width / 2) + 0.5f, (-y + height / 2) + 0.5f, 0), Quaternion.Euler(0, 0, 0));
                    break;
                case 1:
                    rand = Random.Range(planetInt + 1, planetInt + 2);
                    node = (GameObject)Instantiate(nodePrefab[rand], new Vector3((-x + width / 2) + 0.5f, (-y + height / 2) + 0.5f, 0), Quaternion.Euler(0, 0, 0));
                    break;
                case 2:
                    rand = Random.Range(planetInt + 2, planetInt + 3);
                    node = (GameObject)Instantiate(nodePrefab[rand], new Vector3((-x + width / 2) + 0.5f, (-y + height / 2) + 0.5f, 0), Quaternion.Euler(0, 0, 0));
                    break;
                default:
                    rand = Random.Range(planetInt, planetInt + 1);
                    node = (GameObject)Instantiate(nodePrefab[rand], new Vector3((-x + width / 2) + 0.5f, (-y + height / 2) + 0.5f, 0), Quaternion.Euler(0, 0, 0));
                    break;
            }

            botMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), botTile);

            Node n = node.GetComponent<Node>();
            n.gridX = x;
            n.gridy = y;
            n.walkable = true;
            nodes.Add(node);

            node.name = "Terrain " + x.ToString() + ":" + y.ToString();
            node.transform.SetParent(terrainTiles.transform);
        }

    }

    public void clearMap(bool complete)
    {
        topMap.ClearAllTiles();
        midMap.ClearAllTiles();
        botMap.ClearAllTiles();
        companyMap.ClearAllTiles();

        for(int x = 0; x < nodes.Count; x++)
        {
            Destroy(nodes[x]); 
        }

        Destroy(gridOfNodes);
        nodes.Clear();

        countR = 0;

        if (complete)
        {
            terrainMap = null;
        }
    }

}