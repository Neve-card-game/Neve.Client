using System.Collections;
using System.Collections.Generic;


public class StateBase 
{
    public int Id { get; set;}

    public StateMachine machine;

    public StateBase(int id){
        this.Id = id;
    }

    public virtual void OnEnter(params object[] args){}
    public virtual void OnStay(params object[] args){}
    public virtual void OnExit(params object[] args){}

}
