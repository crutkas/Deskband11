namespace PetsLib.Common.States
{
    /// <summary>
    /// Helper methods for state management and resolution.
    /// </summary>
    public static class StateHelpers
    {
        public static bool IsStateAboveGround(string state) =>
            state == PetStates.ClimbWallLeft ||
            state == PetStates.WallDigLeft ||
            state == PetStates.WallNap ||
            state == PetStates.JumpDownLeft ||
            state == PetStates.Land ||
            state == PetStates.WallHangLeft;

        public static IState ResolveState(string state, IPetType pet, StateContext ctx)
        {
            return state switch
            {
                var s when s == PetStates.SitIdle => new SitIdleState(pet),
                var s when s == PetStates.WalkRight => new WalkRightState(pet, ctx.ScreenWidth),
                var s when s == PetStates.WalkLeft => new WalkLeftState(pet),
                var s when s == PetStates.RunRight => new RunRightState(pet, ctx.ScreenWidth),
                var s when s == PetStates.RunLeft => new RunLeftState(pet),
                var s when s == PetStates.Lie => new LieState(pet),
                var s when s == PetStates.WallHangLeft => new WallHangLeftState(pet),
                var s when s == PetStates.WallDigLeft => new WallDigLeftState(pet),
                var s when s == PetStates.WallNap => new WallNapState(pet),
                var s when s == PetStates.ClimbWallLeft => new ClimbWallLeftState(pet),
                var s when s == PetStates.JumpDownLeft => new JumpDownLeftState(pet),
                var s when s == PetStates.Land => new LandState(pet),
                var s when s == PetStates.Swipe => new SwipeState(pet),
                var s when s == PetStates.IdleWithBall => new IdleWithBallState(pet),
                var s when s == PetStates.ChaseFriend => new ChaseFriendState(pet),
                var s when s == PetStates.StandRight => new StandRightState(pet),
                var s when s == PetStates.StandLeft => new StandLeftState(pet),
                _ => new SitIdleState(pet)
            };
        }
    }
}