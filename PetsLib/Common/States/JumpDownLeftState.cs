namespace PetsLib.Common.States
{
    /// <summary>
    /// State for jumping down from a wall or elevated position.
    /// </summary>
    internal class JumpDownLeftState(IPetType pet) : IState
    {
        public string Label => PetStates.JumpDownLeft;
        public string SpriteLabel => "fall_from_grab";
        public HorizontalDirection HorizontalDirection => HorizontalDirection.Right;
        public IPetType Pet { get; } = pet;

        public FrameResult NextFrame()
        {
            Pet.PositionBottom(Pet.Bottom - Pet.FallSpeed);
            if (Pet.Bottom <= Pet.Floor)
            {
                Pet.PositionBottom(Pet.Floor);
                return FrameResult.StateComplete;
            }
            return FrameResult.StateContinue;
        }
    }
}