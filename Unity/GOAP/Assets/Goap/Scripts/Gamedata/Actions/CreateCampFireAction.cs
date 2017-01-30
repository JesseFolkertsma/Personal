using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCampFireAction : GoapAction {

    bool made = false;

    float startTime = 0;
    public float makeDuration = 3;

    public CreateCampFireAction()
    {
        AddPrecondition("HasShelter", true);
        AddPrecondition("HasWood", true);
        AddEffect("FireExists", true);
    }

    public override bool CheckProceduralPrecondition(GameObject _agent)
    {
        return true;
    }

    public override bool IsDone()
    {
        return made;
    }

    public override bool Preform(GameObject _agent)
    {
        if (startTime == 0)
            startTime = Time.time;

        if (Time.time - startTime > makeDuration)
        {
            Caveman cm = _agent.GetComponent<Caveman>();
            cm.energy -= cost;
            BackpackComponent bp = GetComponent<BackpackComponent>();
            bp.wood--;
            GameObject prefab = Resources.Load<GameObject>("CampFire");
            Instantiate(prefab, transform.position + transform.forward * 2 - transform.up, transform.rotation);
            made = true;
        }
        return true;
    }

    public override bool RequiresInRange()
    {
        return false;
    }

    public override void Reset()
    {
        made = false;
        startTime = 0;
    }
}
