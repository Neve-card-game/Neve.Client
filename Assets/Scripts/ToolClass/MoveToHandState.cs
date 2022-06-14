using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToHandState : StateTemplate<CardPlay>
{
    public MoveToHandState(int Id, CardPlay c) : base(Id, c) { }

    public override void OnEnter(params object[] args)
    {
        owner.StartMoveToHand();
    }

    public override void OnStay(params object[] args) { }

    public override void OnExit(params object[] args)
    {
        owner.OriginalPosition = owner.transform.position;
        owner.OriginalRotation = owner.transform.rotation;
    }
}
