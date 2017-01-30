using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherBerriesAction : GoapAction
{
    bool harvested = false;
    BerryBush targetBush = null;

    float startTime = 0;
    public float harvestDuration = 2;

    public GatherBerriesAction()
    {
        AddEffect("HasFood", true);
    }

    public override bool CheckProceduralPrecondition(GameObject _agent)
    {
        BerryBush[] berries = FindObjectsOfType<BerryBush>();
        BerryBush closest = null;
        float dist = 0;

        foreach(BerryBush b in berries)
        {
            if (b.isFound)
            {
                if (closest == null)
                {
                    if (b.CanHarvest)
                    {
                        closest = b;
                        dist = Vector3.Distance(transform.position, b.transform.position);
                    }
                }
                else
                {
                    if (b.CanHarvest)
                    {
                        float dist2 = Vector3.Distance(transform.position, b.transform.position);
                        if (dist > dist2)
                        {
                            closest = b;
                            dist = dist2;
                        }
                    }
                }
            }
        }
        if (closest != null)
        {
            targetBush = closest;
            target = targetBush.gameObject;
        }

        return closest != null;
    }

    public override bool IsDone()
    {
        return harvested;
    }

    public override bool Preform(GameObject _agent)
    {
        if (startTime == 0)
            startTime = Time.time;

        if(Time.time - startTime > harvestDuration)
        {
            Caveman cm = _agent.GetComponent<Caveman>();
            cm.energy -= cost;
            targetBush.Harvest();
            BackpackComponent bp = _agent.GetComponent<BackpackComponent>();
            bp.food++;
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
        targetBush = null;
        startTime = 0;
    }
}
