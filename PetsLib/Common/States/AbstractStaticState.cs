
namespace PetsLib.Common.States
{
    /// <summary>
    /// Abstract base class for static states that hold for a fixed duration.
    /// </summary>
    public abstract class AbstractStaticState(IPetType pet) : IState
    {
        public virtual string Label => PetStates.SitIdle;
        public int IdleCounter { get; private set; }
        public virtual string SpriteLabel => "idle";
        public virtual int HoldTime => 50;
        public virtual HorizontalDirection HorizontalDirection => HorizontalDirection.Left;
        public IPetType Pet { get; } = pet;

        public virtual FrameResult NextFrame()
        {
            IdleCounter++;
            return IdleCounter > HoldTime ? FrameResult.StateComplete : FrameResult.StateContinue;
        }
    }
}