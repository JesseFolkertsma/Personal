using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatAction : GoapAction
{

    bool hasEaten = true;

    public EatAction()
    {
        AddPrecondition("HasFood", true);
        AddEffect("EatFood", true);
    }

    public override bool CheckProceduralPrecondition(GameObject _agent)
    {
        return true;
    }

    public override bool IsDone()
    {
        return hasEaten;
    }

    public override bool Preform(GameObject _agent)
    {
        GetComponent<BackpackComponent>().food -= 1;
        print("EatFood");
        hasEaten = true;
        return true;
    }

    public override bool RequiresInRange()
    {
        return false;
    }

    public override void Reset()
    {
        hasEaten = false;
    }
}
