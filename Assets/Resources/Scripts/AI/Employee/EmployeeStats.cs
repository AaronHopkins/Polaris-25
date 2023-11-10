using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeStats : MonoBehaviour
{
    protected int r1;
    protected int r2;
    protected int l1;
    protected int l2;

    protected int genStat(int min, int max)
    {
        int result;

        result = Random.Range(min, max);

        return result;
    }

    protected int addToStats(int stat, int num)
    {
        int result;

        result = stat + num;

        return result;
    }
}
