using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;

public class GameManager : MonoBehaviour
{

    [SerializeField] Transform infoText;
    //[SerializeField] Transform titleText;

    [SerializeField] Canvas canvas;
    [SerializeField] CanvasGroup TipsC;
    [SerializeField] CanvasGroup TipsE;
    [SerializeField] CanvasGroup companyProfile;
    [SerializeField] CanvasGroup enemyProfile;
    [SerializeField] CanvasGroup vistionBoard;
    [SerializeField] CanvasGroup upgradeWindow;
    [SerializeField] PlayerCompany pCompany;

    public GameObject currentTileSelected;
    public Vector2 playerPos;

    string filePathNet, filePathInfo;

    public int questSelected;

    public GameObject selectedPrefab;
    GameObject selectedTile;
    public EnemyCompany selecyedEnemy;

    //Text titleTxt;
    Text infoTxt;

    private TerrainGeneration TG;
    private TurnManagemet tm;

    Text txt;
    public bool gameStart = false;

    int tipCountC = 0;
    int tipCountE = 0;

    public int difficulty;
    bool loseOfFood = false;

    Transform infowindow;
    Transform topHUD;
    Transform currencyUI;

    [TextArea] public string textToShow;

    private void Awake()
    {
        if (canvas == null)
        {
            canvas = (Canvas)GameObject.FindObjectsOfType(typeof(Canvas))[1];
        }

        TG = gameObject.GetComponent<TerrainGeneration>();
        TG.mapActive = false;
        infowindow = canvas.transform.Find("InfoWindow");

        if (tm == null)
            tm = gameObject.GetComponent<TurnManagemet>();

        upgradeWindow = canvas.transform.Find("UpgradeWindow").GetComponent<CanvasGroup>();

        if (infoText == null)
        {
            infoText = infowindow.Find("InfoBackground");
        }

        infoTxt = infoText.GetComponent<Text>();

        topHUD = canvas.transform.Find("Top_HUD");

        if (companyProfile == null)
            companyProfile = canvas.transform.Find("CompanyProfile_Window").GetComponent<CanvasGroup>();
        
        if (enemyProfile == null)
            enemyProfile = canvas.transform.Find("EnemyProfile_Window").GetComponent<CanvasGroup>();

        if (vistionBoard == null)
            vistionBoard = canvas.transform.Find("VistionBoard_Window").GetComponent<CanvasGroup>();

        if (pCompany == null)
            pCompany = this.gameObject.GetComponent<PlayerCompany>();

        currencyUI = topHUD.Find("IncomeUI_Background").Find("Currency_Background");

        Directory.CreateDirectory(Application.streamingAssetsPath + "/GameData/");

        filePathNet = Application.streamingAssetsPath + "/GameData/" + "/NetworthLog.txt";
        filePathInfo = Application.streamingAssetsPath + "/GameData/" + "/PlayerInfo.txt";
    } 

    private void Update()
    {

        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if(TG.pCplaced && Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.collider.transform.tag == "Terrain")
                {
                    int x = hit.collider.GetComponent<Node>().gridX;
                    int y = hit.collider.GetComponent<Node>().gridy;

                    playerPos = hit.collider.transform.position;
                    TG.deleteNode(hit.collider.gameObject);
                    TG.placePlayerCompany(x,y);
                    TG.pCplaced = false;
                    TG.pCOnMap = true;
                    textToShow = "Company Placed at " + x + ":" + y ;
                    infoTxt.text = textToShow;
                }
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null && gameStart)
            {

                if (hit.collider.transform.tag == "Player Company")
                {

                    if (TG.currentPlanet == "Tutorial: Moon" && tipCountC == 0)
                    {
                        TipsC.alpha = 1;
                        TipsC.interactable = true;
                        TipsC.blocksRaycasts = true;
                        tipCountC++;
                    }

                    companyProfile.alpha = 1;
                    companyProfile.blocksRaycasts = true;
                    companyProfile.interactable = true;
                    currentTileSelected = hit.collider.gameObject;
                }
                else if (hit.collider.transform.tag == "Enemy Company")
                {
                    if (TG.currentPlanet == "Tutorial: Moon" && tipCountE == 0)
                    {
                        TipsE.alpha = 1;
                        TipsE.interactable = true;
                        TipsE.blocksRaycasts = true;
                        tipCountE++;
                    }

                    enemyProfile.alpha = 1;
                    enemyProfile.blocksRaycasts = true;
                    enemyProfile.interactable = true;
                    currentTileSelected = hit.collider.gameObject;
                    hit.collider.transform.GetComponent<EnemyCompany>().setProfileInfo();
                    selecyedEnemy = hit.collider.transform.GetComponent<EnemyCompany>();
                }
                else
                {
                    if (hit.collider.transform.GetComponent<Node>().selectableTile && gameStart == true)
                    {

                        if (hit.collider.transform.GetComponent<Node>().upgradeAvailable)
                        {

                            upgradeWindow.alpha = 1;
                            upgradeWindow.interactable = true;
                            upgradeWindow.blocksRaycasts = true;

                            switch (hit.collider.tag)
                            {
                                case "Terrain":
                                    break;

                                case "Resource":
                                    break;
                            }
                        }
                        else
                        {
                            switch (hit.collider.tag)
                            {
                                case "Mountin":
                                    break;
                            }
                        }

                        currentTileSelected = hit.collider.gameObject;
                        if(selectedTile == null)
                        {
                            selectedTile = (GameObject)Instantiate(selectedPrefab, hit.collider.transform.position, Quaternion.Euler(0, 0, 0));
                        }
                        else
                        {
                            Destroy(selectedTile);
                            selectedTile = (GameObject)Instantiate(selectedPrefab, hit.collider.transform.position, Quaternion.Euler(0, 0, 0));
                        }

                    }

                    infoTxt.text = hit.collider.transform.GetComponent<Node>().description;
                    infowindow.GetComponent<CanvasGroup>().alpha = 1;
                    infowindow.GetComponent<CanvasGroup>().blocksRaycasts = true;
                    infowindow.GetComponent<CanvasGroup>().interactable = true;
                }
            }
        }



        if(loseOfFood || (tm.currentTurn >= TG.maxTurns + 1 && gameStart == true))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(2);
            File.WriteAllText(filePathNet, "\n Net Worth each turn: \n\n");
            File.WriteAllText(filePathInfo, "Player Info: \n");

            fillInFile();            
            refreshEditorProjectWindow();

        }
    }

    public bool getLoseOfFood()
    {
        return loseOfFood;
    }

    public void loseOfFoodChange()
    {
        loseOfFood = true;
    }

    public void starGame()
    {
        gameStart = true;

        Vector3 pos = new Vector3(playerPos.x, playerPos.y, -10f);
        Camera.main.orthographicSize = 10;
        Camera.main.transform.position = pos;

        topHUD.Find("IncomeUI_Background").GetComponent<CanvasGroup>().alpha = 1;
        topHUD.Find("MainTitle_Background").GetComponent<CanvasGroup>().alpha = 0;

        CanvasGroup endTurn = canvas.transform.Find("EndTurn_Background").GetComponent<CanvasGroup>();
        endTurn.alpha = 1;
        endTurn.interactable = true;
        endTurn.blocksRaycasts = true;

        switch (difficulty)
        {
            case 0:
                //Easy
                pCompany.setTotal(5000, 'M');
                pCompany.setTotal(50, 'I');
                pCompany.setTotal(50, 'C');
                pCompany.setTotal(70, 'F');
                break;
            case 1:
                //Normal
                pCompany.setTotal(2500, 'M');
                pCompany.setTotal(35, 'I');
                pCompany.setTotal(35, 'C');
                pCompany.setTotal(60, 'F');
                break;
            case 2:
                //Hard
                pCompany.setTotal(1000, 'M');
                pCompany.setTotal(25, 'I');
                pCompany.setTotal(25, 'C');
                pCompany.setTotal(40, 'F');
                break;
            default:
                pCompany.setTotal(2500, 'M');
                pCompany.setTotal(35, 'I');
                pCompany.setTotal(35, 'C');
                pCompany.setTotal(60, 'F');
                break;
        }

        pCompany.setStartingNetWorth();

        switch (questSelected)
        {
            case 0:

                break;
            case 1:

                break;
            case 2:

                break;
            case 3:

                break;
        }
    }

    public void displayInfo(string t)
    {
        infoTxt.text = t;
        infowindow.GetComponent<CanvasGroup>().alpha = 1;
        infowindow.GetComponent<CanvasGroup>().blocksRaycasts = true;
        infowindow.GetComponent<CanvasGroup>().interactable = true;
    }

    void fillInFile()
    {
        //Net worth Log
        List<int> turns = pCompany.getTurnArray();
        List<int> networth = pCompany.getNetworthArray();

        foreach (int i in turns)
        {
            File.AppendAllText(filePathNet, turns[i-1].ToString() + "   : " + networth[i-1].ToString() + "\n");
        }

        //PlayerInfo

        File.AppendAllText(filePathInfo, pCompany.company_CEOName + "\n");
        File.AppendAllText(filePathInfo, pCompany.company_Name + "\n");
        File.AppendAllText(filePathInfo, pCompany.planet_Name + "\n");
        File.AppendAllText(filePathInfo, pCompany.company_Type + "  Complete: " + pCompany.questComplete + "\n");
        File.AppendAllText(filePathInfo, pCompany.company_Strength + "\n");
        File.AppendAllText(filePathInfo, pCompany.company_Weakness + "\n");
        File.AppendAllText(filePathInfo, "Total:" + pCompany.getNetWorth() + "\n");
        
        //keep Portrait at the end of file
        File.AppendAllText(filePathInfo, pCompany.company_Portait + "\n");

    }

    void refreshEditorProjectWindow()
    {
    #if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
    #endif
    }
}
