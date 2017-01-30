using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookFoodAction : GoapAction {

    bool cooked = false;
    CampFire targetFire = null;

    float startTime = 0;
    public float cookDuration = 2;

    public CookFoodAction()
    {
        AddPrecondition("HasRawFood", true);
        AddPrecondition("FireExists", true);
        AddEffect("HasFood", true);
    }

    public override bool CheckProceduralPrecondition(GameObject _agent)
    {
        CampFire[] fires = FindObjectsOfType<CampFire>();
        CampFire closest = null;
        float dist = 0;

        foreach (CampFire f in fires)
        {
            if (f.isFound)
            {
                if (closest == null)
                {
                    closest = f;
                    dist = Vector3.Distance(transform.position, f.transform.position);
                }
                else
                {
                    float dist2 = Vector3.Distance(transform.position, f.transform.position);
                    if (dist > dist2)
                    {
                        closest = f;
                        dist = dist2;
                    }
                }
            }
        }
        if (closest != null)
        {
            targetFire = closest;
            target = targetFire.gameObject;
        }

        //return closest != null;
        return true;
    }

    public override bool IsDone()
    {
        return cooked;
    }

    public override bool Preform(GameObject _agent)
    {
        if (startTime == 0)
            startTime = Time.time;

        if (Time.time - startTime > cookDuration)
        {
            Caveman cm = _agent.GetComponent<Caveman>();
            cm.energy -= cost;
            BackpackComponent bp = _agent.GetComponent<BackpackComponent>();
            bp.rawFood--;
            bp.food++;
            cooked = true;
        }
        return true;
    }

    public override bool RequiresInRange()
    {
        return true;
    }

    public override void Reset()
    {
        cooked = false;
        targetFire = null;
        startTime = 0;
    }
}
