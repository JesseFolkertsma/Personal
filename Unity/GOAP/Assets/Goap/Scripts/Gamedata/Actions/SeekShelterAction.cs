using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekShelterAction : GoapAction
{

    bool shelter = false;
    Cave targetCave = null;

    public SeekShelterAction()
    {
        AddEffect("HasShelter", true);
    }

    public override bool CheckProceduralPrecondition(GameObject _agent)
    {
        Cave[] caves = FindObjectsOfType<Cave>();
        Cave closest = null;
        float dist = 0;

        foreach (Cave c in caves)
        {
            if (c.isFound)
            {
                if (closest == null)
                {
                    closest = c;
                    dist = Vector3.Distance(transform.position, c.transform.position);
                }
                else
                {
                    float dist2 = Vector3.Distance(transform.position, c.transform.position);
                    if (dist > dist2)
                    {
                        closest = c;
                        dist = dist2;
                    }
                }
            }
        }
        if (closest != null)
        {
            targetCave = closest;
            target = targetCave.gameObject;
        }

        return closest != null;
    }

    public override bool IsDone()
    {
        return shelter;
    }

    public override bool Preform(GameObject _agent)
    {
        Caveman cm = _agent.GetComponent<Caveman>();
        cm.energy -= cost;
        shelter = true;
        return true;
    }

    public override bool RequiresInRange()
    {
        return true;
    }

    public override void Reset()
    {
        shelter = false;
        targetCave = null;
    }
}
