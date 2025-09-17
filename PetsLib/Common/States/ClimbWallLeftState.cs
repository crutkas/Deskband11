namespace PetsLib.Common.States
{
    /// <summary>
    /// State for climbing up a wall on the left side.
    /// </summary>
    internal class ClimbWallLeftState(IPetType pet) : IState
    {
        public string Label => PetStates.ClimbWallLeft;
        public string SpriteLabel => "wallclimb";
        public HorizontalDirection HorizontalDirection => HorizontalDirection.Left;
        public IPetType Pet { get; } = pet;

        public FrameResult NextFrame()
        {
            Pet.PositionBottom(Pet.Bottom + Pet.ClimbSpeed);
            if (Pet.Bottom >= Pet.ClimbHeight)
                return FrameResult.StateComplete;
            return FrameResult.StateContinue;
        }
    }
}