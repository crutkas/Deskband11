using System;

namespace PetsLib.Common
{
    // Converted former enums to static classes with string constants.
    public static class PetColors
    {
        public static readonly string Brown = "brown";
        public static readonly string Lightbrown = "lightbrown";
        public static readonly string Black = "black";
        public static readonly string Green = "green";
        public static readonly string Blue = "blue";
        public static readonly string Yellow = "yellow";
        public static readonly string Gray = "gray";
        public static readonly string Purple = "purple";
        public static readonly string Red = "red";
        public static readonly string White = "white";
        public static readonly string Orange = "orange";
        public static readonly string Akita = "akita";
        public static readonly string SocksBlack = "socks black";
        public static readonly string SocksBeige = "socks beige";
        public static readonly string SocksBrown = "socks brown";
        public static readonly string PaintBeige = "paint beige";
        public static readonly string PaintBlack = "paint black";
        public static readonly string PaintBrown = "paint brown";
        public static readonly string Magical = "magical";
        public static readonly string Warrior = "warrior";
        public static readonly string Null = "null";
    }

    public static class PetTypes
    {
        public static readonly string Bunny = "bunny";
        public static readonly string Cat = "cat";
        public static readonly string Chicken = "chicken";
        public static readonly string Clippy = "clippy";
        public static readonly string Cockatiel = "cockatiel";
        public static readonly string Crab = "crab";
        public static readonly string Dog = "dog";
        public static readonly string Deno = "deno";
        public static readonly string Fox = "fox";
        public static readonly string Frog = "frog";
        public static readonly string Horse = "horse";
        public static readonly string Mod = "mod";
        public static readonly string Morph = "morph";
        public static readonly string Panda = "panda";
        public static readonly string Rat = "rat";
        public static readonly string Rocky = "rocky";
        public static readonly string Rubberduck = "rubber-duck";
        public static readonly string Snail = "snail";
        public static readonly string Snake = "snake";
        public static readonly string Squirrel = "squirrel";
        public static readonly string Totoro = "totoro";
        public static readonly string Turtle = "turtle";
        public static readonly string Zappy = "zappy";
        public static readonly string Null = "null";
    }

    public static class PetSpeeds
    {
        // Originally numeric; now represented as strings per instruction.
        public static readonly int Still = 0;
        public static readonly int VerySlow = 1;
        public static readonly int Slow = 2;
        public static readonly int Normal = 3;
        public static readonly int Fast = 4;
        public static readonly int VeryFast = 5;
    }

    public static class PetSizes
    {
        public static readonly string Nano = "nano";
        public static readonly string Small = "small";
        public static readonly string Medium = "medium";
        public static readonly string Large = "large";
    }

    public static class ExtPositions
    {
        public static readonly string Panel = "panel";
        public static readonly string Explorer = "explorer";
    }

    public static class Themes
    {
        public static readonly string None = "none";
        public static readonly string Forest = "forest";
        public static readonly string Castle = "castle";
        public static readonly string Beach = "beach";
        public static readonly string Winter = "winter";
    }

    public static class ColorThemeKinds
    {
        // Numeric in TS; converted to string constants.
        public static readonly string Light = "1";
        public static readonly string Dark = "2";
        public static readonly string HighContrast = "3";
        public static readonly string HighContrastLight = "4";
    }

    public class WebviewMessage(string text, string command)
    {
        public string Text { get; } = text;
        public string Command { get; } = command;
    }

    public static class PetData
    {
        public static readonly string[] AllPets =
        [
            PetTypes.Bunny,
            PetTypes.Cat,
            PetTypes.Chicken,
            PetTypes.Clippy,
            PetTypes.Cockatiel,
            PetTypes.Crab,
            PetTypes.Dog,
            PetTypes.Deno,
            PetTypes.Fox,
            PetTypes.Frog,
            PetTypes.Horse,
            PetTypes.Mod,
            PetTypes.Morph,
            PetTypes.Panda,
            PetTypes.Rat,
            PetTypes.Rocky,
            PetTypes.Rubberduck,
            PetTypes.Snail,
            PetTypes.Snake,
            PetTypes.Squirrel,
            PetTypes.Totoro,
            PetTypes.Turtle,
            PetTypes.Zappy,
        ];

        public static readonly string[] AllColors =
        [
            PetColors.Black,
            PetColors.Brown,
            PetColors.Lightbrown,
            PetColors.Green,
            PetColors.Yellow,
            PetColors.Gray,
            PetColors.Purple,
            PetColors.Red,
            PetColors.White,
            PetColors.Orange,
            PetColors.Akita,
            PetColors.SocksBlack,
            PetColors.SocksBeige,
            PetColors.SocksBrown,
            PetColors.PaintBeige,
            PetColors.PaintBlack,
            PetColors.PaintBrown,
            PetColors.Magical,
            PetColors.Warrior,
        ];

        public static readonly string[] AllScales =
        [
            PetSizes.Nano,
            PetSizes.Small,
            PetSizes.Medium,
            PetSizes.Large,
        ];

        public static readonly string[] AllThemes =
        [
            Themes.None,
            Themes.Forest,
            Themes.Castle,
            Themes.Beach,
            Themes.Winter,
        ];
    }
}
