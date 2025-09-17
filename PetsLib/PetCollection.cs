using System;
using System.Collections.Generic;
using System.Linq;
using PetsLib.Common;

namespace PetsLib
{
    /// <summary>
    /// Manages a collection of pet elements with friendship and interaction capabilities.
    /// </summary>
    public class PetCollection : IPetCollection
    {
        private readonly List<PetElement> _pets;

        public PetCollection()
        {
            _pets = [];
        }

        /// <summary>
        /// Gets a read-only view of the pets collection.
        /// </summary>
        public IReadOnlyList<PetElement> Pets => _pets.AsReadOnly();

        /// <summary>
        /// Adds a pet to the collection.
        /// </summary>
        /// <param name="pet">The pet element to add.</param>
        public void Push(PetElement pet)
        {
            if (pet == null)
                throw new ArgumentNullException(nameof(pet));
            
            _pets.Add(pet);
        }

        /// <summary>
        /// Removes all pets from the collection and cleans up resources.
        /// </summary>
        public void Reset()
        {
            foreach (var pet in _pets)
            {
                pet.Remove();
            }
            _pets.Clear();
        }

        /// <summary>
        /// Locates a pet by name.
        /// </summary>
        /// <param name="name">The name of the pet to find.</param>
        /// <returns>The pet element if found, otherwise null.</returns>
        public PetElement? Locate(string name)
        {
            if (string.IsNullOrEmpty(name))
                return null;

            return _pets.FirstOrDefault(collection => collection.Pet.Name == name);
        }

        /// <summary>
        /// Locates a pet by name, type, and color.
        /// </summary>
        /// <param name="name">The name of the pet.</param>
        /// <param name="type">The type of the pet.</param>
        /// <param name="color">The color of the pet.</param>
        /// <returns>The pet element if found, otherwise null.</returns>
        public PetElement? LocatePet(string name, string type, string color)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(type) || string.IsNullOrEmpty(color))
                return null;

            return _pets.FirstOrDefault(collection =>
                collection.Pet.Name == name &&
                collection.Type == type &&
                collection.Color == color);
        }

        /// <summary>
        /// Removes a specific pet from the collection.
        /// </summary>
        /// <param name="targetPet">The pet element to remove.</param>
        public void Remove(PetElement targetPet)
        {
            if (targetPet == null)
                return;

            // Find and remove the pet
            var petToRemove = _pets.FirstOrDefault(pet => pet == targetPet);
            if (petToRemove != null)
            {
                petToRemove.Remove();
                _pets.Remove(petToRemove);
            }
        }

        /// <summary>
        /// Attempts to create friendships between compatible pets in the collection.
        /// </summary>
        public void SeekNewFriends()
        {
            if (_pets.Count <= 1)
                return; // You can't be friends with yourself

            var friendlessPets = _pets.Where(pet => !pet.Pet.HasFriend).ToList();
            
            if (friendlessPets.Count <= 1)
                return; // Nobody to be friends with

            foreach (var lonelyPet in friendlessPets)
            {
                var potentialFriends = friendlessPets.Where(pet => pet != lonelyPet).ToList();
                
                foreach (var potentialFriend in potentialFriends)
                {
                    if (!potentialFriend.Pet.CanChase)
                        continue; // Pet is busy doing something else

                    // Check if pets are close enough to become friends
                    if (potentialFriend.Pet.Left > lonelyPet.Pet.Left &&
                        potentialFriend.Pet.Left < lonelyPet.Pet.Left + lonelyPet.Pet.Width)
                    {
                        // We found a possible new friend
                        System.Diagnostics.Debug.WriteLine(
                            $"{lonelyPet.Pet.Name} wants to be friends with {potentialFriend.Pet.Name}.");

                        if (lonelyPet.Pet.MakeFriendsWith(potentialFriend.Pet))
                        {
                            potentialFriend.Pet.ShowSpeechBubble("❤️", 2000);
                            lonelyPet.Pet.ShowSpeechBubble("❤️", 2000);
                        }
                    }
                }
            }
        }
    }
}