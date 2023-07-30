using Godot;

interface IEnemy {
    public Node2D Target { get; set; }
    public Vector2 Position { get; set; }
}
