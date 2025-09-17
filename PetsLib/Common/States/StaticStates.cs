namespace PetsLib.Common.States
{
    /// <summary>
    /// Collection of simple static states that inherit from AbstractStaticState.
    /// </summary>

    internal class SitIdleState(IPetType pet) : AbstractStaticState(pet)
    {
        public override string Label => PetStates.SitIdle;
        public override string SpriteLabel => "idle";
        public override HorizontalDirection HorizontalDirection => HorizontalDirection.Right;
        public override int HoldTime => 50;
    }

    internal class LieState(IPetType pet) : AbstractStaticState(pet)
    {
        public override string Label => PetStates.Lie;
        public override string SpriteLabel => "lie";
        public override HorizontalDirection HorizontalDirection => HorizontalDirection.Right;
        public override int HoldTime => 50;
    }

    internal class WallHangLeftState(IPetType pet) : AbstractStaticState(pet)
    {
        public override string Label => PetStates.WallHangLeft;
        public override string SpriteLabel => "wallgrab";
        public override HorizontalDirection HorizontalDirection => HorizontalDirection.Left;
    }

    internal class WallDigLeftState(IPetType pet) : AbstractStaticState(pet)
    {
        public override string Label => PetStates.WallDigLeft;
        public override string SpriteLabel => "walldig";
        public override HorizontalDirection HorizontalDirection => HorizontalDirection.Left;
        public override int HoldTime => 60;
    }

    internal class WallNapState(IPetType pet) : AbstractStaticState(pet)
    {
        public override string Label => PetStates.WallNap;
        public override string SpriteLabel => "wallnap";
        public override HorizontalDirection HorizontalDirection => HorizontalDirection.Right;
    }

    internal class LandState(IPetType pet) : AbstractStaticState(pet)
    {
        public override string Label => PetStates.Land;
        public override string SpriteLabel => "land";
        public override int HoldTime => 10;
    }

    internal class SwipeState(IPetType pet) : AbstractStaticState(pet)
    {
        public override string Label => PetStates.Swipe;
        public override string SpriteLabel => "swipe";
        public override HorizontalDirection HorizontalDirection => HorizontalDirection.Natural;
        public override int HoldTime => 15;
    }

    internal class IdleWithBallState(IPetType pet) : AbstractStaticState(pet)
    {
        public override string Label => PetStates.IdleWithBall;
        public override string SpriteLabel => "with_ball";
        public override int HoldTime => 30;
    }

    internal class StandRightState(IPetType pet) : AbstractStaticState(pet)
    {
        public override string Label => PetStates.StandRight;
        public override string SpriteLabel => "stand";
        public override HorizontalDirection HorizontalDirection => HorizontalDirection.Right;
        public override int HoldTime => 60;
    }

    internal class StandLeftState(IPetType pet) : AbstractStaticState(pet)
    {
        public override string Label => PetStates.StandLeft;
        public override string SpriteLabel => "stand";
        public override HorizontalDirection HorizontalDirection => HorizontalDirection.Left;
        public override int HoldTime => 60;
    }
}