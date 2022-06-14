using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragState : StateTemplate<CardPlay>
{
    
    public DragState(int Id,CardPlay c):base(Id,c){}

    public override void OnEnter(params object[] args)
    {
       owner.StartDrag();
    }

    public override void OnStay(params object[] args)
    {
        owner.StayDrag();
    }

    public override void OnExit(params object[] args)
    {
        
    }
}
