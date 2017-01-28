using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caveman : MonoBehaviour, IGoap {
    public BackpackComponent backpack;
    public float moveSpeed = 1;

    void Start()
    {
        if(backpack == null)
        {
            backpack = GetComponent<BackpackComponent>();
        }
    }

    public HashSet<KeyValuePair<string, object>> GetWorldState()
    {
        HashSet<KeyValuePair<string, object>> worldData = new HashSet<KeyValuePair<string, object>>();

        worldData.Add(new KeyValuePair<string, object>("HasFood", (backpack.food > 0)));

        return worldData;
    }

    public HashSet<KeyValuePair<string,object>> CreateGoalState()
    {
        HashSet<KeyValuePair<string, object>> goal = new HashSet<KeyValuePair<string, object>>();

        goal.Add(new KeyValuePair<string, object>("EatFood", true));

        return goal;
    }

    public void PlanFailed(HashSet<KeyValuePair<string, object>> failedGoal)
    {
        Debug.Log("<color=red>Plan failed!</color> " + GoapAgent.PrettyPrint(failedGoal));
    }

    public void PlanFound(HashSet<KeyValuePair<string, object>> goal, Queue<GoapAction> action)
    {
        Debug.Log("<color=green>Plan found</color> " + GoapAgent.PrettyPrint(action));
    }

    public void ActionsFinished()
    {
        Debug.Log("<color=blue>Actions completed</color>");
    }

    public void PlanAborted(GoapAction aborter)
    {
        Debug.Log("<color=red>Plan Aborted</color> " + GoapAgent.PrettyPrint(aborter));
    }

    public bool MoveAgent(GoapAction nextAction)
    {
        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, nextAction.target.transform.position, step);

        if (transform.position.Equals(nextAction.target.transform.position))
        {
            nextAction.SetInRange(true);
            return true;
        }
        else
            return false;
    }
}
