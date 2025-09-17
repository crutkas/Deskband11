namespace PetsLib.Common.States
{
    /// <summary>
    /// Core interface defining the behavior and properties of a pet.
    /// </summary>
    public interface IPetType
    {
        void NextFrame();

        bool CanSwipe { get; }
        bool CanChase { get; }
        void Swipe();
        void Chase(BallState ballState);

        double Speed { get; }
        double ClimbSpeed { get; }
        double ClimbHeight { get; }
        double FallSpeed { get; }
        bool IsMoving { get; }
        string Hello { get; }

        // State API
        PetInstanceState GetState();
        void RecoverState(PetInstanceState state);
        void RecoverFriend(IPetType friend);

        // Positioning
        double Bottom { get; }
        double Left { get; }
        void PositionBottom(double bottom);
        void PositionLeft(double left);
        double Width { get; }
        double Floor { get; }

        // Friends
        string Name { get; }
        string Emoji { get; }
        bool HasFriend { get; }
        IPetType? Friend { get; }
        bool MakeFriendsWith(IPetType friend);
        bool IsPlaying { get; }

        void ShowSpeechBubble(string message, int durationMs);
        void Remove();
    }
}