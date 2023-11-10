using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VistonBoardSave : MonoBehaviour
{
    [SerializeField] CanvasGroup vistionBoard;
    [SerializeField] CanvasGroup windowButtons;
    [SerializeField] CanvasGroup mapGenTips;
    [SerializeField] PlayerCompany PC;
    [SerializeField] Transform planetText;
    [SerializeField] TerrainGeneration TG;

    [SerializeField] InputField comName;
    [SerializeField] InputField comCEOName;
    [SerializeField] Dropdown planet;
    [SerializeField] Dropdown strength;
    [SerializeField] Dropdown weakness;
    [SerializeField] Dropdown comType;
    [SerializeField] Text errorMessage;

    GameManager gameM;

    Canvas canvas;
    public int portrait;
    Text txt;

    private void Awake()
    {
        if(canvas == null)
            canvas = (Canvas)GameObject.FindObjectsOfType(typeof(Canvas))[0];

        if(vistionBoard == null)
            vistionBoard = canvas.transform.Find("VistionBoard_Window").GetComponent<CanvasGroup>();

        if (comCEOName == null || comName == null)
        {
            comName = transform.parent.Find("CompanyNameSelectText").GetComponent<InputField>();
            comCEOName = transform.parent.Find("NameSelectText").GetComponent<InputField>();
        }

        if (planet == null || strength == null|| weakness == null|| comType == null)
        {
            planet = transform.parent.Find("PlanetSelectText").GetComponent<Dropdown>();
            comType = transform.parent.Find("CompanyTypeText").GetComponent<Dropdown>();
            strength = transform.parent.Find("CompanyStrengthText").GetComponent<Dropdown>();
            weakness = transform.parent.Find("CompanyWeaknessText").GetComponent<Dropdown>();
        }

        if (TG == null)
            TG = GameObject.FindGameObjectWithTag("Manager").GetComponent<TerrainGeneration>();

        if (PC == null)
            PC = GameObject.FindGameObjectWithTag("Manager").GetComponent<PlayerCompany>();

        if (planetText == null)
            planetText = canvas.transform.Find("CurrentPlanet_Background");

        txt = planetText.Find("CurrentPlanet_txt").GetComponent<Text>();

        gameM = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();

        if (windowButtons == null)
            windowButtons = canvas.transform.Find("WorldCreateButtons_Background").GetComponent<CanvasGroup>();
    }



    public void oneClick()
    {
        if (comName.text != "" || comCEOName.text != "")
        {
            vistionBoard.alpha = 0f;
            vistionBoard.blocksRaycasts = false;
            vistionBoard.interactable = false;

            windowButtons.alpha = 1f;
            windowButtons.interactable = true;
            windowButtons.blocksRaycasts = true;

            

            planetText.GetComponent<CanvasGroup>().alpha = 1f;

            int intP = planet.value;
            int intS = strength.value;
            int intW = weakness.value;
            int intC = comType.value;

            if (planet.options[intP].text == "Tutorial: Moon")
            {
                mapGenTips.alpha = 1;
                mapGenTips.interactable = true;
                mapGenTips.blocksRaycasts = true;
            }

            PC.company_Name = comName.text;
            PC.company_CEOName = comCEOName.text;
            PC.planet_Name = planet.options[intP].text;
            PC.company_Strength = strength.options[intS].text;
            PC.company_Weakness = weakness.options[intW].text;
            PC.company_Type = comType.options[intC].text;
            PC.company_Portait = portrait;       

            PC.setInfo();
            TG.setPlanet(PC.planet_Name);

            gameM.difficulty = planet.value;
            gameM.questSelected = comType.value;

            TG.mapActive = true;

            txt.text = "Current: " + PC.planet_Name;
        }
        else
        {
            // Error Message
            errorMessage.text = "Please enter a CEO or station name";
        }
    }
}
