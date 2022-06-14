using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public Dictionary<int,StateBase> Machine_StateCache;

    public StateBase Machine_PreviousState;
    public StateBase Machine_CurrentState;
    
    public StateMachine(StateBase beginState){
        Machine_PreviousState = null;
        Machine_CurrentState = beginState;

        Machine_StateCache = new Dictionary<int, StateBase>();

        AddState(beginState);
        Machine_CurrentState.OnEnter();
    }

    public void AddState(StateBase state){
        if(!Machine_StateCache.ContainsKey(state.Id)){
            Machine_StateCache.Add(state.Id,state);
            state.machine = this;
        }
    }

    public void TranslateState(int id){
        if(!Machine_StateCache.ContainsKey(id)){
            return;
        }
        else if(Machine_CurrentState.Id == id){
            return;
        }
        Machine_PreviousState = Machine_CurrentState;
        Machine_CurrentState.OnExit();
        Machine_CurrentState = Machine_StateCache[id];
        Machine_CurrentState.OnEnter();
    }

    public void Update(){
        if(Machine_CurrentState != null){
            Machine_CurrentState.OnStay();
        }
    }
}
