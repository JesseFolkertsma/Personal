using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootCarcassAction : GoapAction {

    bool harvested = false;
    Carcass targetCarcass = null;

    float startTime = 0;
    public float harvestDuration = 2;

    public LootCarcassAction()
    {
        AddPrecondition("CarcassExists", true);
        AddEffect("HasRawFood", true);
    }

    public override bool CheckProceduralPrecondition(GameObject _agent)
    {
        Carcass[] carcasses = FindObjectsOfType<Carcass>();
        Carcass closest = null;
        float dist = 0;

        foreach (Carcass a in carcasses)
        {
            if (a.isFound)
            {
                if (closest == null)
                {
                    closest = a;
                    dist = Vector3.Distance(transform.position, a.transform.position);
                }
                else
                {
                    float dist2 = Vector3.Distance(transform.position, a.transform.position);
                    if (dist > dist2)
                    {
                        closest = a;
                        dist = dist2;
                    }
                }
            }
        }
        if (closest != null)
        {
            targetCarcass = closest;
            target = targetCarcass.gameObject;
        }

        //return closest != null;
        return true;
    }

    public override bool IsDone()
    {
        return harvested;
    }

    public override bool Preform(GameObject _agent)
    {
        if (startTime == 0)
            startTime = Time.time;

        if (Time.time - startTime > harvestDuration)
        {
            Caveman cm = _agent.GetComponent<Caveman>();
            cm.energy -= cost;
            targetCarcass.Loot();
            BackpackComponent bp = _agent.GetComponent<BackpackComponent>();
            bp.rawFood++;
            harvested = true;
        }
        return true;
    }

    public override bool RequiresInRange()
    {
        return true;
    }

    public override void Reset()
    {
        harvested = false;
        targetCarcass = null;
        startTime = 0;
    }
}
