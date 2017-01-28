using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM {

    private Stack<FSMState> stateStack = new Stack<FSMState>();

    public delegate void FSMState(FSM _fsm, GameObject _gameObject);

    public void Update(GameObject _gameObject)
    {
        if(stateStack.Peek() != null)
            stateStack.Peek().Invoke(this, _gameObject);
    }

    public void PushState(FSMState _state)
    {
        stateStack.Push(_state);
    }

    public void PopState()
    {
        stateStack.Pop();
    }
}
