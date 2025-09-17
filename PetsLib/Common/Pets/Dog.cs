using Microsoft.UI.Xaml.Controls;
using PetsLib.Common.States;
using System.Collections.Generic;

namespace PetsLib.Common.Pets
{
    /// <summary>
    /// Dog pet type implementation with specific behaviors and state sequences.
    /// </summary>
    public class Dog : BasePetType
    {
        public static new readonly string[] PossibleColors = 
        [
            PetColors.Black,
            PetColors.Brown,
            PetColors.White,
            PetColors.Red,
            PetColors.Akita
        ];

        public static readonly string[] DogNames = 
        [
            "Bella", "Charlie", "Max", "Molly", "Coco", "Buddy", "Ruby", "Oscar", 
            "Lucy", "Bailey", "Milo", "Daisy", "Archie", "Ollie", "Rosie", "Lola", 
            "Frankie", "Toby", "Roxy", "Poppy", "Luna", "Jack", "Millie", "Teddy", 
            "Harry", "Cooper", "Bear", "Rocky", "Alfie", "Hugo", "Bonnie", "Pepper", 
            "Lily", "Leo", "Maggie", "George", "Mia", "Marley", "Harley", "Chloe", 
            "Lulu", "Jasper", "Billy", "Nala", "Monty", "Ziggy", "Winston", "Zeus", 
            "Zoe", "Stella", "Sasha", "Rusty", "Gus", "Baxter", "Dexter", "Diesel", 
            "Willow", "Barney", "Bruno", "Penny", "Honey", "Milly", "Murphy", "Holly", 
            "Benji", "Henry", "Lilly", "Pippa", "Shadow", "Sam", "Buster", "Lucky", 
            "Ellie", "Duke", "Jessie", "Cookie", "Harvey", "Bruce", "Jax", "Rex", 
            "Louie", "Bentley", "Jet", "Banjo", "Beau", "Ella", "Ralph", "Loki", 
            "Lexi", "Chester", "Sophie", "Billie", "Louis", "Cleo", "Spot", 
            "Bolt", "Ein", "Maddy", "Ghost", "Midnight", "Pumpkin", "Sparky", 
            "Linus", "Cody", "Slinky", "Toto", "Balto", "Golfo", "Pongo", 
            "Beethoven", "Hachiko", "Scooby", "Clifford", "Astro", "Goofy", 
            "Chip", "Einstein", "Fang", "Truman", "Uggie", "Bingo", "Blue", 
            "Cometa", "Krypto", "Huesos", "Odie", "Snoopy", "Aisha", "Moly", 
            "Chiquita", "Chavela", "Tramp", "Lady", "Puddles"
        ];

        public override string Emoji => "🐶";

        public override string Hello => 
            " Every dog has its day - and today is woof day! Today I just want to bark. Take me on a walk";

        public Dog(
            Image spriteElement,
            Grid collisionElement,
            TextBlock speechElement,
            Grid containerGrid,
            string size,
            double left,
            double bottom,
            string petRoot,
            double floor,
            string name,
            double speed,
            StateContext? stateContext = null)
            : base(spriteElement, collisionElement, speechElement, containerGrid, size, left, bottom, petRoot, floor, name, speed, stateContext)
        {
            Label = "dog";
            
            // Set up the state sequence specific to dogs
            _sequence = new SequenceTree
            {
                StartingState = PetStates.SitIdle,
                SequenceStates =
                [
                    new SequenceState 
                    { 
                        State = PetStates.SitIdle, 
                        PossibleNextStates =
                        [
                            PetStates.WalkRight, 
                            PetStates.RunRight, 
                            PetStates.Lie 
                        ]
                    },
                    new SequenceState 
                    { 
                        State = PetStates.Lie, 
                        PossibleNextStates =
                        [
                            PetStates.WalkRight, 
                            PetStates.RunRight 
                        ]
                    },
                    new SequenceState 
                    { 
                        State = PetStates.WalkRight, 
                        PossibleNextStates =
                        [
                            PetStates.WalkLeft, 
                            PetStates.RunLeft 
                        ]
                    },
                    new SequenceState 
                    { 
                        State = PetStates.RunRight, 
                        PossibleNextStates =
                        [
                            PetStates.WalkLeft, 
                            PetStates.RunLeft 
                        ]
                    },
                    new SequenceState 
                    { 
                        State = PetStates.WalkLeft, 
                        PossibleNextStates =
                        [
                            PetStates.SitIdle, 
                            PetStates.Lie, 
                            PetStates.WalkRight, 
                            PetStates.RunRight 
                        ]
                    },
                    new SequenceState 
                    { 
                        State = PetStates.RunLeft, 
                        PossibleNextStates =
                        [
                            PetStates.SitIdle, 
                            PetStates.Lie, 
                            PetStates.WalkRight, 
                            PetStates.RunRight 
                        ]
                    },
                    new SequenceState 
                    { 
                        State = PetStates.Chase, 
                        PossibleNextStates =
                        [
                            PetStates.IdleWithBall 
                        ]
                    },
                    new SequenceState 
                    { 
                        State = PetStates.IdleWithBall, 
                        PossibleNextStates =
                        [
                            PetStates.WalkRight, 
                            PetStates.WalkLeft, 
                            PetStates.RunLeft, 
                            PetStates.RunRight 
                        ]
                    }
                ]
            };
        }
    }
}