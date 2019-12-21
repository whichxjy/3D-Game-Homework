public class PlayerEventManager {
    // singleton instance
    private static PlayerEventManager instance;

    // move event
    public delegate void MoveAction(float verticalAxis, float horizontalAxis);
    public static event MoveAction OnMove;

    public static PlayerEventManager GetInstance() {
        if (instance == null) {
            instance = new PlayerEventManager();
        }
        return instance;
    }

    public void Move(float verticalAxis, float horizontalAxis) {
        OnMove?.Invoke(verticalAxis, horizontalAxis);
    }
}
