using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class InfoDisplay : MonoBehaviour
{

    [SerializeField] Text[] textBox;
    [SerializeField] Image portraitDisplay = null;
    [SerializeField] Sprite[] characters;
    string[] info;

    // Start is called before the first frame update
    void Start()
    {
        info = File.ReadAllLines(Application.streamingAssetsPath + "/GameData/" + "/PlayerInfo.txt");

        for (int i = 0; i < info.Length; i++)
        {
            if (i == 0)
            {
                
            }
            else if (i == info.Length - 1)
            {
                portraitDisplay.GetComponent<Image>().sprite = characters[int.Parse(info[i]) - 1];
            }
            else
            {
                textBox[i-1].text = info[i];
            }
        }
    }


}
