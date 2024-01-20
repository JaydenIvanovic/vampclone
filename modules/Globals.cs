using Godot;

namespace Globals {
    public static class GameState {
        public static uint ExperienceGained { get; set; }
        public static uint Kills { get; set; }
        public static uint NumProjectileCountUpgrades { get; set; }
        public static uint NumGarlicUpgrades { get; set; }
        public static uint NumOrbs { get; set; }
        public static uint PlayerHealthAmount { get; set; }
    }
}
