namespace PetsLib.Common.States
{
    /// <summary>
    /// Interface for pet state implementations.
    /// </summary>
    public interface IState
    {
        string Label { get; }
        string SpriteLabel { get; }
        HorizontalDirection HorizontalDirection { get; }
        IPetType Pet { get; }
        FrameResult NextFrame();
    }
}