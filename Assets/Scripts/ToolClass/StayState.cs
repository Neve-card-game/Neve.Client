using UnityEngine;

public class StayState : StateTemplate<CardPlay>
{
    public StayState(int id, CardPlay c) : base(id, c) { }

    public override void OnEnter(params object[] args)
    {
        if (machine.Machine_PreviousState != null && machine.Machine_PreviousState.Id == 1)
        {
            owner.StartShrink();
        }
    }

    public override void OnStay(params object[] args) { }

    public override void OnExit(params object[] args)
    {

        owner.transform.position = owner.OriginalPosition;
        owner.transform.rotation = owner.OriginalRotation;
    }
}

