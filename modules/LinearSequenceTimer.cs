using System;
using Godot;

public readonly struct LinearSequenceTimerRunConfig {
    public LinearSequenceTimerRunConfig(double createInterval, double destroyInterval, Node rootNode, Action createFn, Action destroyFn) {
        CreateInterval = createInterval;
        DestroyInterval = destroyInterval;
        RootNode = rootNode;
        CreateFunc = createFn;
        DestroyFunc = destroyFn;
    }

    public double CreateInterval { get; init; }
    public double DestroyInterval { get; init; }
    public Node RootNode { get; init; }
    public Action CreateFunc { get; init; }
    public Action DestroyFunc { get; init; }
}

public class LinearSequenceTimer {
    private bool canRun;
    private LinearSequenceTimerRunConfig config;

    public LinearSequenceTimer(LinearSequenceTimerRunConfig config) {
        canRun = true;
        this.config = config;
    }

    public void Run() {
        if (!canRun) {
            return;
        }

        canRun = false;

        var createTimer = new Timer {
            OneShot = true,
            Autostart = true,
            WaitTime = config.CreateInterval
        };
        createTimer.Timeout += () => {
            config.CreateFunc();

            var destroyTimer = new Timer {
                OneShot = true,
                Autostart = true,
                WaitTime = config.DestroyInterval
            };
            destroyTimer.Timeout += () => {
                config.DestroyFunc();
                canRun = true;
            };
            config.RootNode.AddChild(destroyTimer);
        };
        config.RootNode.AddChild(createTimer);
    }
}
