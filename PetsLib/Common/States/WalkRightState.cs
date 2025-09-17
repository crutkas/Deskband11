using System;

namespace PetsLib.Common.States
{
    /// <summary>
    /// State for walking to the right side of the screen.
    /// </summary>
    internal class WalkRightState(IPetType pet, int screenWidth) : IState
    {
        public string Label => PetStates.WalkRight;
        public string SpriteLabel => "walk";
        public HorizontalDirection HorizontalDirection => HorizontalDirection.Right;
        public IPetType Pet { get; } = pet;

        private readonly int _leftBoundary = (int)Math.Floor(screenWidth * 0.95);
        protected virtual double SpeedMultiplier => 1.0;
        private int _idleCounter = 0;
        protected virtual int HoldTime => 60;

        public virtual FrameResult NextFrame()
        {
            _idleCounter++;
            var newLeft = Pet.Left + Pet.Speed * SpeedMultiplier;
            var maxRight = _leftBoundary - Pet.Width;
            Pet.PositionLeft(newLeft > maxRight ? maxRight : newLeft);

            if (Pet.IsMoving && Pet.Left >= maxRight)
                return FrameResult.StateComplete;
            if (!Pet.IsMoving && _idleCounter > HoldTime)
                return FrameResult.StateComplete;

            return FrameResult.StateContinue;
        }
    }
}