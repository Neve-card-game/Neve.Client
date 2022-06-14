
public class StateTemplate<T>:StateBase
{
    public T owner;

    public StateTemplate(int id, T Owner):base(id){
        owner = Owner;
    }
}
