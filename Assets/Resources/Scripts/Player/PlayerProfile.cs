using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProfile : MonoBehaviour
{
    private int playerLevel = 1;
    //private int exp = 0;
    private string playerName;

    public void setPlayerName(string n)
    {
        playerName = n;
    }

    public string getPlayerName()
    {
        return playerName;
    }

    public int getLevel()
    {
        return playerLevel;
    }



    public void levelUpPlayer()
    {
        playerLevel++;
    }
}
