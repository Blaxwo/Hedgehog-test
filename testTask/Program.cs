class HedgehogPopulation
{
    private int numRed;
    private int numGreen;
    private int numBlue;

    public HedgehogPopulation(int numRed, int numGreen, int numBlue)
    {
        this.numRed = numRed;
        this.numGreen = numGreen;
        this.numBlue = numBlue;
    }

    public int CalculateMinimumMeetings(int targetColor)
    {
        int targetCount = targetColor switch
        {
            0 => numRed,
            1 => numGreen,
            2 => numBlue,
            _ => throw new ArgumentException("Invalid color")
        };

        int firstGroupCount = targetColor switch
        {
            0 => numGreen,
            1 => numRed,
            2 => numRed,
            _ => throw new ArgumentException("Invalid color")
        };

        int secondGroupCount = targetColor switch
        {
            0 => numBlue,
            1 => numBlue,
            2 => numGreen,
            _ => throw new ArgumentException("Invalid color")
        };

        if (firstGroupCount == 0 && secondGroupCount == 0)
        {
            return -1;
        }
        
        if (IsPossibleToBecomeTargetColor(targetCount, firstGroupCount, secondGroupCount))
        {
            int largerGroupCount = firstGroupCount > secondGroupCount ? firstGroupCount : secondGroupCount;
            return largerGroupCount;
        }
        return -1;
    }

    private static bool IsPossibleToBecomeTargetColor(int targetCount, int firstGroupCount, int secondGroupCount)
    {
        int smallerCount = Math.Min(firstGroupCount, secondGroupCount);
        int largerCount = Math.Max(firstGroupCount, secondGroupCount);

        while (targetCount > 0 && smallerCount < largerCount)
        {
            smallerCount += 3;
            targetCount--;
        }
        return smallerCount == largerCount;
    }

    public static bool ValidateInput(string[] input, out int[] numsOfColors, out int targetColor)
    {
        numsOfColors = new int[3];
        targetColor = -1;

        if (input.Length != 3)
        {
            Console.WriteLine("Error: You must input exactly 3 numbers for red, green, and blue hedgehogs.");
            return false;
        }

        try
        {
            numsOfColors[0] = int.Parse(input[0]);
            numsOfColors[1] = int.Parse(input[1]);
            numsOfColors[2] = int.Parse(input[2]);

            long totalHedgehogs = 0;
            foreach (int count in numsOfColors)
            {
                if (count < 0)
                {
                    Console.WriteLine("Error: Numbers must be non-negative.");
                    return false;
                }

                totalHedgehogs += count;
                if (totalHedgehogs > int.MaxValue)
                {
                    Console.WriteLine("Error: The total number of hedgehogs exceeds the maximum allowed value.");
                    return false;
                }
            }

            if (totalHedgehogs < 1)
            {
                Console.WriteLine("Error: The total number of hedgehogs must be at least 1.");
                return false;
            }

            Console.WriteLine("Input the wanted color (0 - red, 1 - green, 2 - blue):");
            if (!int.TryParse(Console.ReadLine(), out targetColor) || targetColor < 0 || targetColor > 2)
            {
                Console.WriteLine("Error: Invalid target color! Must be 0 (red), 1 (green), or 2 (blue).");
                return false;
            }
        }
        catch (Exception)
        {
            Console.WriteLine("Error: Invalid input! Please input integers for the number of hedgehogs.");
            return false;
        }

        return true;
    }
}

class Program
{
    static void Main()
    {
        int[] numsOfColors = new int[3];
        int targetColor = -1;
        bool isValid = false;

        while (!isValid)
        {
            Console.WriteLine("Input the number of red, green and blue hedgehogs using a space:");
            string[] input = Console.ReadLine().Split();

            isValid = HedgehogPopulation.ValidateInput(input, out numsOfColors, out targetColor);
        }

        HedgehogPopulation population = new HedgehogPopulation(numsOfColors[0], numsOfColors[1], numsOfColors[2]);

        int minMeetings = population.CalculateMinimumMeetings(targetColor);

        string colorName = targetColor switch
        {
            0 => "red",
            1 => "green",
            2 => "blue",
            _ => "unknown"
        };

        Console.WriteLine($"The minimum number of meetings for hedgehogs to all become {colorName} is: {minMeetings}");
    }
}
