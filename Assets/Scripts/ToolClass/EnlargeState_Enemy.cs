public class EnlargeState_Enemy : StateTemplate<CardPlay_Enemy>
{
    public EnlargeState_Enemy(int id, CardPlay_Enemy c) : base(id, c) { }

    public override void OnEnter(params object[] args)
    {
        owner.StartEnlarge();
    }

    public override void OnStay(params object[] args) { }

    public override void OnExit(params object[] args)
    {
        owner.EndEnlarge();
    }
}
