using Godot;

public partial class Events : Node2D {
    [Signal]
    public delegate void EnemyDiedEventHandler(Vector2 deathPosition);

    private static Events eventInstance = null;

    public static Events getInstance() {
        if (eventInstance == null) {
            eventInstance = new Events();
        }
        return eventInstance;
    }
}
