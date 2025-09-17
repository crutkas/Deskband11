namespace PetsLib.Common.States
{
    /// <summary>
    /// State for running to the left at increased speed.
    /// </summary>
    internal class RunLeftState(IPetType pet) : WalkLeftState(pet)
    {
        protected override double SpeedMultiplier => 1.6;
        protected override int HoldTime => 130;
        public new static string Label => PetStates.RunLeft;
        public new static string SpriteLabel => "walk_fast";
    }
}