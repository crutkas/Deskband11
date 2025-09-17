namespace PetsLib.Common.States
{
    /// <summary>
    /// State for chasing another pet (friend).
    /// </summary>
    internal class ChaseFriendState(IPetType pet) : IState
    {
        public string Label => PetStates.ChaseFriend;
        public string SpriteLabel => "run";
        public HorizontalDirection HorizontalDirection { get; private set; } = HorizontalDirection.Left;
        public IPetType Pet { get; } = pet;

        public FrameResult NextFrame()
        {
            if (!Pet.HasFriend || Pet.Friend is null || !Pet.Friend.IsPlaying)
                return FrameResult.StateCancel;

            if (Pet.Left > Pet.Friend.Left)
            {
                HorizontalDirection = HorizontalDirection.Left;
                Pet.PositionLeft(Pet.Left - Pet.Speed);
            }
            else
            {
                HorizontalDirection = HorizontalDirection.Right;
                Pet.PositionLeft(Pet.Left + Pet.Speed);
            }

            return FrameResult.StateContinue;
        }
    }
}