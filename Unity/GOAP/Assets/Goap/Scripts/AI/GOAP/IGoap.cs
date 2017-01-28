using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGoap {
    HashSet<KeyValuePair<string, object>> GetWorldState();
    HashSet<KeyValuePair<string, object>> CreateGoalState();
    void PlanFailed(HashSet<KeyValuePair<string, object>> failedGoal);
    void PlanFound(HashSet<KeyValuePair<string, object>> goal, Queue<GoapAction> action);
    void ActionsFinished();
    void PlanAborted(GoapAction aborter);
    bool MoveAgent(GoapAction nextAction);
}
