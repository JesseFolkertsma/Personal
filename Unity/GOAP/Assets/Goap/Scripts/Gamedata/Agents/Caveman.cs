using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Caveman : MonoBehaviour, IGoap {

    [Header("Caveman Stats")]
    public float health = 100;
    public float hunger = 100;
    public float energy = 100;
    public float warmth = 100;
    public bool sheltered = false;

    [Header("Caveman variables")]
    public float moveSpeed = 1;
    public float energyDrainRate = .1f;

    public BackpackComponent backpack;
    NavMeshAgent navAgent;

    void Start()
    {
        if(backpack == null)
        {
            backpack = GetComponent<BackpackComponent>();
        }
        navAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        HandleStats();
    }

    void HandleStats()
    {
        Mathf.Clamp(health, 0f, 100f);
        Mathf.Clamp(hunger, 0f, 100f);
        Mathf.Clamp(energy, 0f, 100f);
        Mathf.Clamp(warmth, 0f, 100f);

        energy -= energyDrainRate * Time.deltaTime;
    }

    public HashSet<KeyValuePair<string, object>> GetWorldState()
    {
        HashSet<KeyValuePair<string, object>> worldData = new HashSet<KeyValuePair<string, object>>();

        worldData.Add(new KeyValuePair<string, object>("HasFood", (backpack.food > 0)));
        worldData.Add(new KeyValuePair<string, object>("HasRawFood", (backpack.rawFood > 0)));
        worldData.Add(new KeyValuePair<string, object>("HasWeapon", (backpack.spears > 0)));
        worldData.Add(new KeyValuePair<string, object>("HasWood", (backpack.wood > 0)));
        worldData.Add(new KeyValuePair<string, object>("HasShelter", sheltered));

        KeyValuePair<string, object>[] rwd = GameManager.instance.GetRealWorldData(transform);

        foreach(KeyValuePair<string,object> kvp in rwd)
        {
            worldData.Add(kvp);
        }

        return worldData;
    }

    public HashSet<KeyValuePair<string,object>> CreateGoalState()
    {
        HashSet<KeyValuePair<string, object>> goal = new HashSet<KeyValuePair<string, object>>();

        if (hunger < 60)
        {
            goal.Add(new KeyValuePair<string, object>("EatFood", true));
            //goal.Add(GetFoodGoal());
        }
        else if (health < 50)
        {
            goal.Add(new KeyValuePair<string, object>("RestoreHealth", true));
            print("Current goal: restoreHP");
        }
        else if (energy < 30)
        {
            goal.Add(new KeyValuePair<string, object>("Sleep", true));
            print("Current goal: sleep");
        }


        return goal;
    }

    KeyValuePair<string, object> GetFoodGoal()
    {
        if (DoesExist(typeof(BerryBush)) || DoesExist(typeof(Carcass)))
        {
            print("Current goal: eat");
            return new KeyValuePair<string, object>("EatFood", true);
        }
        else
        {
            print("Current goal: kill");
            return new KeyValuePair<string, object>("KillAnimal", true);
        }
    }

    bool DoesExist(Type _type)
    {
        bool found = false;

        if(_type == typeof(BerryBush))
        {
            BerryBush[] bushes = FindObjectsOfType<BerryBush>();
            foreach(BerryBush b in bushes)
            {
                if (b.isFound)
                {
                    if (b.CanHarvest)
                    {
                        found = true;
                        break;
                    }
                }
            }
        }

        else if (FindObjectsOfType(_type).Length > 0)
        {
            found = true;
        }

        return found;
    }

    public bool MoveAgent(GoapAction nextAction)
    {
        //float step = moveSpeed * Time.deltaTime;
        //transform.position = Vector3.MoveTowards(transform.position, nextAction.target.transform.position, step);
        navAgent.Resume();
        navAgent.SetDestination(nextAction.target.transform.position);

        if (Vector3.Distance(transform.position, nextAction.target.transform.position) < nextAction.ReachDistance())
        {
            nextAction.SetInRange(true);
            navAgent.Stop();

            return true;
        }
        else
            return false;
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
}
