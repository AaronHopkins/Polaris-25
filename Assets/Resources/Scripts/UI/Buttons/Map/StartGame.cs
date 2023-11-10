using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    [SerializeField] TerrainGeneration TG;
    [SerializeField] GameManager GM;
    [SerializeField] CanvasGroup worldChangeButtons;
    [SerializeField] CanvasGroup tips;

    void Start()
    {
        if (worldChangeButtons == null)
        {
            Transform perant = transform.parent;
            while (perant == null)
            {
                worldChangeButtons = perant.GetComponent<CanvasGroup>();
                if (worldChangeButtons == null)
                {
                    break;
                }
                perant = perant.parent;
            }
        }

        if (TG == null)
            TG = GameObject.FindGameObjectWithTag("Manager").GetComponent<TerrainGeneration>();

        if (GM == null)
            GM = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
    }

    public void onClick()
    {
        if (TG.pCOnMap)
        {
            if (TG.currentPlanet == "Tutorial: Moon")
            {
                tips.alpha = 1;
                tips.interactable = true;
                tips.blocksRaycasts = true;
            }

            GM.starGame();
            worldChangeButtons.alpha = 0;
            worldChangeButtons.interactable = false;
            worldChangeButtons.blocksRaycasts = false;
        }
    }
}
