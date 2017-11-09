public abstract class GameState<T> where T : class
{
    public abstract void Enter(T entity);
    public abstract void Update(T entity);
    public abstract void Exit(T entity);
    public abstract void Touch(T entity, float x, float y);
    public string GetStateName() { return STATE_NAME; }
    protected string STATE_NAME;
}