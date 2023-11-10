using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playbutton : MonoBehaviour
{

    public void onClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
