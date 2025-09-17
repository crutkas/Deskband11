using System.Collections.Generic;

namespace PetsLib.Common.States
{
    /// <summary>
    /// Represents the state of the entire pet panel.
    /// </summary>
    public class PetPanelState
    {
        public List<PetElementState>? PetStates { get; set; }
        public int? PetCounter { get; set; }
    }
}