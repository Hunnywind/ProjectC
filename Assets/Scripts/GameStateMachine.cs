
public class GameStateMachine<T> where T : class
{
    private T owner_entity;
    private GameState<T> current_state;

    public void Init(T _owner, GameState<T> initState)
    {
        owner_entity = _owner;
        ChangeState(initState);
    }
    public void Update()
    {
        if (current_state != null)
        {
            current_state.Update(owner_entity);
            return;
        }
    }
    public void Touch(float x, float y)
    {
        if (current_state != null)
        {
            current_state.Touch(owner_entity, x, y);
            return;
        }
    }
    public void ChangeState(GameState<T> newState)
    {
        if (newState == null) return;
        if (current_state != null)
        {
            current_state.Exit(owner_entity);
        }
        current_state = newState;
        current_state.Enter(owner_entity);
    }
    public void Set_CurrentState(GameState<T> state)
    {
        current_state = state;
    }
    public GameState<T> Get_CurrentState()
    {
        return current_state;
    }
    public string GetStateName()
    {
        return current_state.GetStateName();
    }
}