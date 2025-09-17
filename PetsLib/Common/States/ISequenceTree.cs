using System.Collections.Generic;

namespace PetsLib.Common.States
{
    /// <summary>
    /// Represents a sequence state with possible transitions.
    /// </summary>
    public class SequenceState
    {
        public string State { get; set; } = string.Empty;
        public List<string> PossibleNextStates { get; set; } = [];
    }

    /// <summary>
    /// Defines the sequence tree for pet state transitions.
    /// </summary>
    public interface ISequenceTree
    {
        string StartingState { get; }
        List<SequenceState> SequenceStates { get; }
    }

    /// <summary>
    /// Default implementation of sequence tree.
    /// </summary>
    public class SequenceTree : ISequenceTree
    {
        public string StartingState { get; set; } = PetStates.SitIdle;
        public List<SequenceState> SequenceStates { get; set; } = [];
    }
}