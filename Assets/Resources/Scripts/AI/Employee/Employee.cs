using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Employee : EmployeeStats
{

    private List<Trait> traitList = new List<Trait>();
    private int traitAmount;

    public void genEmployee()
    {
        traitAmount = Random.Range(1, 3);

        for (int i = 0; i < traitAmount; i++)
        {
            Trait newTrait = new Trait();

            newTrait.randTrait();
            traitList.Add(newTrait);
        }
    }

    public List<Trait> getTraitList()
    {
        return traitList;
    }

}
