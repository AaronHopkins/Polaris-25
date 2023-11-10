using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class TextToScrollView : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string text = File.ReadAllText(Application.streamingAssetsPath + "/GameData/" + "/NetworthLog.txt");

        gameObject.GetComponent<Text>().text = text;
    }

}
