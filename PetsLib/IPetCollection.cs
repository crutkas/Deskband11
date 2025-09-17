using System.Collections.Generic;

namespace PetsLib
{
    /// <summary>
    /// Interface for managing a collection of pet elements.
    /// </summary>
    public interface IPetCollection
    {
        /// <summary>
        /// Gets the collection of pets.
        /// </summary>
        IReadOnlyList<PetElement> Pets { get; }

        /// <summary>
        /// Adds a pet to the collection.
        /// </summary>
        /// <param name="pet">The pet element to add.</param>
        void Push(PetElement pet);

        /// <summary>
        /// Removes all pets from the collection.
        /// </summary>
        void Reset();

        /// <summary>
        /// Attempts to create friendships between compatible pets.
        /// </summary>
        void SeekNewFriends();

        /// <summary>
        /// Locates a pet by name.
        /// </summary>
        /// <param name="name">The name of the pet to find.</param>
        /// <returns>The pet element if found, otherwise null.</returns>
        PetElement? Locate(string name);

        /// <summary>
        /// Locates a pet by name, type, and color.
        /// </summary>
        /// <param name="name">The name of the pet.</param>
        /// <param name="type">The type of the pet.</param>
        /// <param name="color">The color of the pet.</param>
        /// <returns>The pet element if found, otherwise null.</returns>
        PetElement? LocatePet(string name, string type, string color);

        /// <summary>
        /// Removes a specific pet from the collection.
        /// </summary>
        /// <param name="pet">The pet element to remove.</param>
        void Remove(PetElement pet);
    }
}