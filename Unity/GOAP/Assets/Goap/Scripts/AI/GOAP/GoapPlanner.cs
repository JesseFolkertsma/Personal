using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoapPlanner : MonoBehaviour {
    public Queue<GoapAction> Plan(GameObject _agent, HashSet<GoapAction> _availableActions, HashSet<KeyValuePair<string, object>> _worldState, HashSet<KeyValuePair<string, object>> _goal)
    {
        foreach(GoapAction a in _availableActions)
        {
            a.DoReset();
        }

        HashSet<GoapAction> useableActions = new HashSet<GoapAction>();
        foreach(GoapAction a in _availableActions)
        {
            if (a.CheckProceduralPrecondition(_agent))
                useableActions.Add(a);
        }

        List<Node> leaves = new List<Node>();

        Node start = new Node(null, 0, _worldState, null);
        bool succes = BuildGraph(start, leaves, useableActions, _goal);

        if (!succes)
        {
            Debug.Log(_agent.gameObject.name + "'s GOAPAgent could not find a plan!");
            return null;
        }

        Node cheapest = null;
        foreach(Node leaf in leaves)
        {
            if (cheapest == null)
                cheapest = leaf;
            else
            {
                if (leaf.runningCost < cheapest.runningCost)
                    cheapest = leaf;
            }
        }

        List<GoapAction> result = new List<GoapAction>();
        Node n = cheapest;
        while(n != null)
        {
            if(n.action != null)
            {
                result.Insert(0, n.action);
            }
            n = n.parent;
        }

        Queue<GoapAction> queue = new Queue<GoapAction>();
        foreach(GoapAction a in result)
        {
            queue.Enqueue(a);
        }

        return queue;
    }

    bool BuildGraph(Node _parent, List<Node> _leaves, HashSet<GoapAction> _useableActions, HashSet<KeyValuePair<string, object>> _goal)
    {
        bool foundOne = false;

        foreach(GoapAction action in _useableActions)
        {
            if(InState(action.PreConditions, _parent.state))
            {
                HashSet<KeyValuePair<string, object>> currentState = PopulateState(_parent.state, action.Effects);
                Node node = new Node(_parent, _parent.runningCost + action.cost, currentState, action);

                if(InState(_goal, currentState))
                {
                    _leaves.Add(node);
                    foundOne = true;
                }
                else
                {
                    HashSet<GoapAction> subSet = ActionSubset(_useableActions, action);
                    bool found = BuildGraph(node, _leaves, subSet, _goal);
                    if (found)
                        foundOne = true;
                }
            }
        }

        return foundOne;
    }

    HashSet<GoapAction> ActionSubset(HashSet<GoapAction> _actions, GoapAction _removeMe)
    {
        HashSet<GoapAction> subset = new HashSet<GoapAction>();
        foreach(GoapAction a in _actions)
        {
            if (!a.Equals(_removeMe))
                subset.Add(a);
        }
        return subset;
    }

    bool InState (HashSet<KeyValuePair<string, object>> _check, HashSet<KeyValuePair<string, object>> _state)
    {
        bool allMatch = true;
        foreach(KeyValuePair<string,object> t in _check)
        {
            bool match = false;
            foreach(KeyValuePair<string,object> s in _state)
            {
                if (s.Equals(t))
                {
                    match = true;
                    break;
                }
            }
            if (!match)
                allMatch = false;
        }
        return allMatch;
    }

    HashSet<KeyValuePair<string, object>> PopulateState(HashSet<KeyValuePair<string, object>> _currentState, HashSet<KeyValuePair<string, object>> _stateChange)
    {
        HashSet<KeyValuePair<string, object>> state = new HashSet<KeyValuePair<string, object>>();
        foreach(KeyValuePair<string,object> s in _currentState)
        {
            state.Add(new KeyValuePair<string, object>(s.Key, s.Value));
        }

        foreach(KeyValuePair<string,object> change in _stateChange)
        {
            bool exists = false;
            foreach(KeyValuePair<string,object> s in state)
            {
                if (s.Equals(change))
                {
                    exists = true;
                    break;
                }
            }

            if (exists)
            {
                state.RemoveWhere((KeyValuePair<string, object> kvp) => { return kvp.Key.Equals(change.Key); });
                KeyValuePair<string, object> updated = new KeyValuePair<string, object>(change.Key, change.Value);
                state.Add(updated);
            }

            else
            {
                state.Add(new KeyValuePair<string, object>(change.Key, change.Value));
            }
        }
        return state;
    }
}

class Node
{
    public Node parent;
    public float runningCost;
    public HashSet<KeyValuePair<string, object>> state;
    public GoapAction action;

    public Node(Node _parent, float _runningCost, HashSet<KeyValuePair<string, object>> _state, GoapAction _action)
    {
        parent = _parent;
        runningCost = _runningCost;
        state = _state;
        action = _action;
    }
}
