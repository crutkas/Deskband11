using System;
using Microsoft.UI.Xaml.Controls;
using PetsLib.Common;
using PetsLib.Common.States;

namespace PetsLib
{
    /// <summary>
    /// Represents a pet element in the WinUI environment with its visual components.
    /// </summary>
    public class PetElement(
        Image element,
        Grid collision,
        TextBlock speech,
        IPetType pet,
        string color,
        string type)
    {
        public Image Element { get; } = element ?? throw new ArgumentNullException(nameof(element));
        public Grid Collision { get; } = collision ?? throw new ArgumentNullException(nameof(collision));
        public TextBlock Speech { get; } = speech ?? throw new ArgumentNullException(nameof(speech));
        public IPetType Pet { get; } = pet ?? throw new ArgumentNullException(nameof(pet));
        public string Color { get; private set; } = color ?? throw new ArgumentNullException(nameof(color));
        public string Type { get; private set; } = type ?? throw new ArgumentNullException(nameof(type));

        /// <summary>
        /// Removes the pet element and cleans up resources.
        /// </summary>
        public void Remove()
        {
            // Clean up the pet first
            Pet.Remove();
            
            // Reset color and type to null values  
            Color = PetColors.Null ?? string.Empty;
            Type = PetTypes.Null ?? string.Empty;
            
            // Note: In WinUI, we typically don't need to manually remove elements 
            // from their parent containers as this is handled by the parent Grid
            // or the BasePetType.Remove() method
        }
    }
}