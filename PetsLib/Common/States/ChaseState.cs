namespace PetsLib.Common.States
{
    /// <summary>
    /// State for chasing a ball on the screen.
    /// </summary>
    internal class ChaseState(IPetType pet, BallState ballState, StateContext ctx) : IState
    {
        public string Label => PetStates.Chase;
        public string SpriteLabel => "run";
        public HorizontalDirection HorizontalDirection { get; private set; } = HorizontalDirection.Left;
        public IPetType Pet { get; } = pet;

        private readonly BallState _ballState = ballState;
        private readonly StateContext _ctx = ctx;

        public FrameResult NextFrame()
        {
            if (_ballState.Paused)
                return FrameResult.StateCancel;

            if (Pet.Left > _ballState.Cx)
            {
                HorizontalDirection = HorizontalDirection.Left;
                Pet.PositionLeft(Pet.Left - Pet.Speed);
            }
            else
            {
                HorizontalDirection = HorizontalDirection.Right;
                Pet.PositionLeft(Pet.Left + Pet.Speed);
            }

            if (_ctx.CanvasHeight - _ballState.Cy < Pet.Width + Pet.Floor &&
                _ballState.Cx < Pet.Left && Pet.Left < _ballState.Cx + 15)
            {
                _ctx.HideBallAction?.Invoke();
                _ballState.Paused = true;
                return FrameResult.StateComplete;
            }

            return FrameResult.StateContinue;
        }
    }
}