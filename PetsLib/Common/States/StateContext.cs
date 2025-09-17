using System;

namespace PetsLib.Common.States
{
    /// <summary>
    /// Context for state operations, abstracting platform-specific concerns.
    /// </summary>
    public class StateContext(int screenWidth, int canvasHeight, Action? hideBallAction = null)
    {
        public int ScreenWidth { get; } = screenWidth;
        public int CanvasHeight { get; } = canvasHeight;
        public Action? HideBallAction { get; } = hideBallAction;
    }
}