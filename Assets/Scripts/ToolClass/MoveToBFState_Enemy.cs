using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToBFState_Enemy : StateTemplate<CardPlay_Enemy>
{
    public MoveToBFState_Enemy(int Id, CardPlay_Enemy c) : base(Id, c) { }

    public override void OnEnter(params object[] args)
    {
        owner.StartMoveToBF();
    }

    public override void OnStay(params object[] args) { }

    public override void OnExit(params object[] args) { }
}
