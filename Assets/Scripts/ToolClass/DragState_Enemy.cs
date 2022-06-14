using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragState_Enemy : StateTemplate<CardPlay_Enemy>
{
    public DragState_Enemy(int Id,CardPlay_Enemy c):base(Id,c){}

    public override void OnEnter(params object[] args)
    {
       //owner.StartDrag();
    }

    public override void OnStay(params object[] args)
    {
        //owner.StayDrag();
    }

    public override void OnExit(params object[] args)
    {

    }
}
