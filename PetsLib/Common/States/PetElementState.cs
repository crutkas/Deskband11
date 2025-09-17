using System.Collections.Generic;

namespace PetsLib.Common.States
{
    /// <summary>
    /// Represents the serializable state of a pet element in the UI.
    /// </summary>
    public class PetElementState
    {
        public PetInstanceState? PetState { get; set; }
        public string? PetType { get; set; }
        public string? PetColor { get; set; }
        public string? ElementLeft { get; set; }
        public string? ElementBottom { get; set; }
        public string? PetName { get; set; }
        public string? PetFriend { get; set; }
    }
}