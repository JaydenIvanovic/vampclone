using Godot;

public partial class Events : Node2D {
    [Signal] public delegate void EnemyDiedEventHandler(Vector2 deathPosition);
    [Signal] public delegate void ExperienceGemAcquiredEventHandler();
    [Signal] public delegate void PlayerDiedEventHandler();
    [Signal] public delegate void PlayerSelectingLevelUpEventHandler();
    [Signal] public delegate void PlayerSelectedLevelUpEventHandler();

    private static Events eventInstance = null;

    public static Events I {
        get {
            return eventInstance;
        }
    }

    // Can be used on game start / load to reconstruct event bus with new game tree objects
    public static void Init() {
        eventInstance = new Events();
    }
}
