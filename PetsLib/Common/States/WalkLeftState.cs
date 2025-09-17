namespace PetsLib.Common.States
{
    /// <summary>
    /// State for walking to the left side of the screen.
    /// </summary>
    internal class WalkLeftState(IPetType pet) : IState
    {
        public string Label => PetStates.WalkLeft;
        public string SpriteLabel => "walk";
        public HorizontalDirection HorizontalDirection => HorizontalDirection.Left;
        public IPetType Pet { get; } = pet;

        protected virtual double SpeedMultiplier => 1.0;
        private int _idleCounter = 0;
        protected virtual int HoldTime => 60;

        public virtual FrameResult NextFrame()
        {
            _idleCounter++;
            var newLeft = Pet.Left - Pet.Speed * SpeedMultiplier;
            Pet.PositionLeft(newLeft < 0 ? 0 : newLeft);

            if (Pet.IsMoving && Pet.Left <= 0)
                return FrameResult.StateComplete;
            if (!Pet.IsMoving && _idleCounter > HoldTime)
                return FrameResult.StateComplete;

            return FrameResult.StateContinue;
        }
    }
}