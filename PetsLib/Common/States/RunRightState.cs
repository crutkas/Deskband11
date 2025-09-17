namespace PetsLib.Common.States
{
    /// <summary>
    /// State for running to the right at increased speed.
    /// </summary>
    internal class RunRightState(IPetType pet, int screenWidth) : WalkRightState(pet, screenWidth)
    {
        protected override double SpeedMultiplier => 1.6;
        protected override int HoldTime => 130;
        public new static string Label => PetStates.RunRight;
        public new static string SpriteLabel => "walk_fast";
    }
}