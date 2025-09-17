using System;

namespace PetsLib
{
    /// <summary>
    /// Exception thrown when an invalid pet operation occurs.
    /// </summary>
    public class InvalidPetException : Exception
    {
        public InvalidPetException() : base()
        {
        }

        public InvalidPetException(string? message) : base(message)
        {
        }

        public InvalidPetException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}