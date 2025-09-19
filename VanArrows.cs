using System;

class Program
{
    static void Main()
    {
        var ArrowApp = new ArrowApp();
        ArrowApp.Run();
    }

    class ArrowApp
    {
        public void Run()
        {
            while (true)
            {
                ShowMenu();
                int option = Validators.GetValidInt("Select an option:");
                Console.Clear();

                switch (option)
                {
                    case 1: // Custom Arrow
                        CustomArrow();
                        break;

                    case 2: // Elite Arrow
                        Arrow.EliteArrowType eliteType = Validators.GetValidEnumberInput<Arrow.EliteArrowType>("Choose an elite arrow type:");
                        Arrow eliteArrow = Arrow.CreateEliteArrow(eliteType);
                        Console.Clear();
                        Console.WriteLine($"The cost of your elite arrow is: {eliteArrow.GetCost():0.00} gold.");
                        ClearScreen();
                        break;

                    case 3:
                        Console.WriteLine("Exiting the program.");
                        return;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        private void ShowMenu()
        {
            Console.WriteLine("1. Create Custom Arrow");
            Console.WriteLine("2. Create Elite Arrow");
            Console.WriteLine("3. Exit");
        }

        private static void ClearScreen()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }

        private void CustomArrow()
        {
            float shaftLength = Validators.GetValidFloatInput("Enter the shaft length (60cm - 100cm):");
            Console.Clear();
            Arrow.ArrowheadType arrowheadType = Validators.GetValidEnumberInput<Arrow.ArrowheadType>("Choose an arrowhead type:");
            Console.Clear();
            Arrow.FletchingType fletchingType = Validators.GetValidEnumberInput<Arrow.FletchingType>("Choose a fletching type:");
            Console.Clear();
            Arrow arrow = new Arrow(shaftLength, arrowheadType, fletchingType);
            Console.WriteLine($"The cost of your arrow is: {arrow.GetCost():0.00} gold.");
            ClearScreen();
        }

        public class Validators
        {
            public static int GetValidOptionMenu(string prompt)
            {
                return GetValidInt(prompt, 1, 3);
            }

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

            public static int GetValidInt(string prompt, int min = 0, int max = int.MaxValue)
            {
                int value;
                while (true)
                {
                    Console.WriteLine(prompt);
                    if (int.TryParse(Console.ReadLine(), out value) && value >= min && value <= max)
                    {
                        return value;
                    }
                    Console.WriteLine($"Please enter a valid number between {min} and {max}.");
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

            public enum EliteArrowType
            {
                EliteArrow,
                BeginnerArrow,
                MarksmanArrow
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

            public static Arrow CreateEliteArrow(EliteArrowType type)
            {
                switch (type)
                {
                    case EliteArrowType.EliteArrow:
                        return new Arrow(95, ArrowheadType.Steel, FletchingType.Plastic);
                    case EliteArrowType.BeginnerArrow:
                        return new Arrow(75, ArrowheadType.Steel, FletchingType.GooseFeathers);
                    case EliteArrowType.MarksmanArrow:
                        return new Arrow(65, ArrowheadType.Wood, FletchingType.GooseFeathers);
                    default:
                        throw new ArgumentOutOfRangeException(nameof(type), type, null);
                }
            }
        }
    }
}
