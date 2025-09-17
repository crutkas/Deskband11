using System;

namespace PetsLib.Common.Exceptions
{
    /// <summary>
    /// Exception thrown when an invalid state transition is attempted.
    /// </summary>
    public class InvalidStateException : Exception
    {
        public string FromState { get; }
        public string PetType { get; }

        public InvalidStateException(string fromState, string petType) 
            : base($"Invalid state {fromState} for pet type {petType}")
        {
            FromState = fromState;
            PetType = petType;
        }

        public InvalidStateException(string fromState, string petType, Exception innerException) 
            : base($"Invalid state {fromState} for pet type {petType}", innerException)
        {
            FromState = fromState;
            PetType = petType;
        }
    }
}