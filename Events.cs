using Godot;

public partial class Events : Node2D {
    [Signal]
    public delegate void EnemyDiedEventHandler(Vector2 deathPosition);
    [Signal]
    public delegate void ExperienceGemAcquiredEventHandler();

    private static Events eventInstance = null;

    public static Events I {
        get {
            if (eventInstance == null) {
                eventInstance = new Events();
            }
            return eventInstance;
        }
    }
}
