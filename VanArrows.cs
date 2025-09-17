    using System;

    class Program
    {
        static void Main()
        {
            float shaftLength = Validators.GetValidFloatInput("Enter the shaft length (60cm - 100cm):");
            Arrow.ArrowheadType arrowheadType = Validators.GetValidEnumberInput<Arrow.ArrowheadType>("Choose an arrowhead type:");
            Arrow.FletchingType fletchingType = Validators.GetValidEnumberInput<Arrow.FletchingType>("Choose a fletching type:");
            Arrow arrow = new Arrow(shaftLength, arrowheadType, fletchingType);
            Console.WriteLine($"The cost of your arrow is: {arrow.GetCost():0.00} gold.");
        }

        
        //Class foir InputValidator

        public class Validators
        {
            public static T GetValidEnumberInput<T>(string prompt) where T : Enum
            {
                while (true)
                {
                    Console.WriteLine(prompt);
                    foreach (var value in Enum.GetValues(typeof(T)))
                    {
                        Console.WriteLine($"{(int)value}. {value}");
                    }

                    if (int.TryParse(Console.ReadLine(), out int choice) && Enum.IsDefined(typeof(T), choice))
                    {
                        return (T)(object)choice;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please try again.");
                    }
                }
            }

                public static float GetValidFloatInput(string prompt)
            {
                while (true)
                {
                    Console.WriteLine(prompt);
                    if (float.TryParse(Console.ReadLine(), out float result) && result >= 60 && result <= 100)
                    {
                        return result;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a positive number.");
                    }
                }
            }
        }
        // Defining a new class called Arrow
            public class Arrow
            {
                private float ShaftLength { get; }
                private ArrowheadType Arrowhead { get; }
                private FletchingType Fletching { get; }

                public enum ArrowheadType
                {
                    Steel,
                    Wood,
                    Obsidian
                }

                public enum FletchingType
                {
                    Plastic,
                    TurkeyFeathers,
                    GooseFeathers
                }

                // Constructor for the Arrow class
                public Arrow(float shaftLength, ArrowheadType arrowheadType, FletchingType fletchingType)
                {
                    ShaftLength = shaftLength;
                    Arrowhead = arrowheadType;
                    Fletching = fletchingType;
                }

                // Method to calculate the cost of the arrow
                public float GetCost()
                {
                    float arrowheadCost = Arrowhead switch
                    {
                        ArrowheadType.Steel => 10.0f,
                        ArrowheadType.Wood => 3.0f,
                        ArrowheadType.Obsidian => 5.0f,
                        _ => throw new ArgumentOutOfRangeException()
                    };

                    float fletchingCost = Fletching switch
                    {
                        FletchingType.Plastic => 10.0f,
                        FletchingType.TurkeyFeathers => 5.0f,
                        FletchingType.GooseFeathers => 3.0f,
                        _ => throw new ArgumentOutOfRangeException()
                    };

                    float shaftCost = ShaftLength * 0.05f;

                    return arrowheadCost + fletchingCost + shaftCost;
                }
            }
    }