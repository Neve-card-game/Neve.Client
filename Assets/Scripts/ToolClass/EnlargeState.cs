public class EnlargeState : StateTemplate<CardPlay> {
    public EnlargeState(int id,CardPlay c):base(id,c){

    }

    public override void OnEnter(params object[] args){
        
        owner.StartEnlarge();
    }

    public override void OnStay(params object[] args){

    }
    public override void OnExit(params object[] args){
        owner.EndEnlarge();
    }
 }
