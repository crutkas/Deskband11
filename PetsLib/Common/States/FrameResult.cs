namespace PetsLib.Common.States
{
    /// <summary>
    /// Represents the result of a state machine frame update.
    /// </summary>
    public enum FrameResult
    {
        StateContinue,  // Continue in the current state
        StateComplete,  // State is complete, transition to next
        StateCancel     // State was cancelled, handle appropriately
    }
}