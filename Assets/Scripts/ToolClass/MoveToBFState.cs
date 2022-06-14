using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToBFState : StateTemplate<CardPlay>
{
    public MoveToBFState(int Id, CardPlay c) : base(Id, c) { }

    public override void OnEnter(params object[] args)
    {
        owner.StartMoveToBF();
    }

    public override void OnStay(params object[] args) { }

    public override void OnExit(params object[] args) { }
}
