using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GoapAction : MonoBehaviour {

    HashSet<KeyValuePair<string, object>> preConditions;
    HashSet<KeyValuePair<string, object>> effects;

    bool inRange = false;
    public float cost = 1f;
    public float reachDistance = 2.5f;
    public GameObject target;

    public GoapAction()
    {
        preConditions = new HashSet<KeyValuePair<string, object>>();
        effects = new HashSet<KeyValuePair<string, object>>();
    }

    public void DoReset()
    {
        inRange = false;
        target = null;
        Reset();
    }

    public float ReachDistance()
    {
        return reachDistance;
    }

    public abstract void Reset();
    public abstract bool IsDone();
    public abstract bool CheckProceduralPrecondition(GameObject _agent);
    public abstract bool Preform(GameObject _agent);
    public abstract bool RequiresInRange();

    public bool IsInRange()
    {
        return inRange;
    }

    public void SetInRange(bool _inRange)
    {
        inRange = _inRange;
    }

    public void AddPrecondition(string _key, object _value)
    {
        preConditions.Add(new KeyValuePair<string, object>(_key, _value));
    }

    public void RemovePrecondition(string _key)
    {
        KeyValuePair<string, object> remove = default(KeyValuePair<string, object>);
        foreach (KeyValuePair<string, object> kvp in preConditions)
        {
            if (kvp.Key.Equals(_key))
                remove = kvp;
        }
        if (!default(KeyValuePair<string, object>).Equals(remove))
            preConditions.Remove(remove);
    }

    public void AddEffect(string _key, object _value)
    {
        effects.Add(new KeyValuePair<string, object>(_key, _value));
    }

    public void RemoveEffect(string _key)
    {
        KeyValuePair<string, object> remove = default(KeyValuePair<string, object>);
        foreach (KeyValuePair<string, object> kvp in effects)
        {
            if (kvp.Key.Equals(_key))
                remove = kvp;
        }
        if (!default(KeyValuePair<string, object>).Equals(remove))
            effects.Remove(remove);
    }

    public HashSet<KeyValuePair<string, object>> PreConditions
    {
        get
        {
            return preConditions;
        }
    }

    public HashSet<KeyValuePair<string, object>> Effects
    {
        get
        {
            return effects;
        }
    }
}
