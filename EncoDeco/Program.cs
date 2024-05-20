using System.Collections;
using System.Text;

const int MAX_RANGE = 4000;

const string WELCOME_MSG = @"
=========================================
     Integer Encoder/Decoder Utility
=========================================
1. Encode (CSV to TXT)
2. Decode (TXT to CSV)
3. Exit
=========================================
";

const string EXIT_MSG = @"
====================================================================
     Thanks for taking the time to review my code challenge!
====================================================================";

void DisplayMenu()
{
    Console.WriteLine(WELCOME_MSG);
}

void MainLoop()
{
    while (true)
    {
        DisplayMenu();

        Console.Write("Please choose an option: ");
        string choice = Console.ReadLine()!;

        switch (choice)
        {
            case "1":
                string inputEncodeFilePath = ListAndSelectCsvFile();
                if (inputEncodeFilePath == null) continue;
                string outputEncodeFilePath = GenerateOutputFilePath("encoded", "txt");
                Encode(inputEncodeFilePath, outputEncodeFilePath);
                break;

            case "2":
                string inputDecodeFilePath = ListAndSelectTextFile();
                if (inputDecodeFilePath == null) continue;
                string outputDecodeFilePath = GenerateOutputFilePath("decoded", "csv");
                Decode(inputDecodeFilePath, outputDecodeFilePath);
                break;

            case "3":
                Console.WriteLine(EXIT_MSG);
                return;

            default:
                Console.WriteLine("Invalid choice. Please select option 1, 2, or 3.");
                break;
        }

        Console.WriteLine("Operation completed successfully.");
    }
}

string ListAndSelectCsvFile()
{
    string directory = AppDomain.CurrentDomain.BaseDirectory;
    var csvFiles = Directory.GetFiles(directory, "*.csv");

    if (csvFiles.Length == 0)
    {
        Console.WriteLine("No CSV files found in the root folder.");
        return null!;
    }

    Console.WriteLine("Please select a file from the list below:");
    for (int i = 0; i < csvFiles.Length; i++)
    {
        Console.WriteLine($"{i + 1}. {Path.GetFileName(csvFiles[i])}");
    }

    Console.Write("Enter the number of the file you want to select: ");
    if (int.TryParse(Console.ReadLine(), out int fileNumber) && fileNumber > 0 && fileNumber <= csvFiles.Length)
    {
        return csvFiles[fileNumber - 1];
    }
    else
    {
        Console.WriteLine("Invalid selection. Please try again.");
        return null!;
    }
}

string ListAndSelectTextFile()
{
    string directory = AppDomain.CurrentDomain.BaseDirectory;
    var textFiles = Directory.GetFiles(directory, "*.txt");

    if (textFiles.Length == 0)
    {
        Console.WriteLine("No text files found in the root folder.");
        return null!;
    }

    Console.WriteLine("Please select a file from the list below:");
    for (int i = 0; i < textFiles.Length; i++)
    {
        Console.WriteLine($"{i + 1}. {Path.GetFileName(textFiles[i])}");
    }

    Console.Write("Enter the number of the file you want to select: ");
    if (int.TryParse(Console.ReadLine(), out int fileNumber) && fileNumber > 0 && fileNumber <= textFiles.Length)
    {
        return textFiles[fileNumber - 1];
    }
    else
    {
        Console.WriteLine("Invalid selection. Please try again.");
        return null!;
    }
}

void Encode(string inputFilePath, string outputFilePath)
{
    try
    {
        var fileContent = File.ReadAllText(inputFilePath);
        var integers = fileContent.Split(',').Select(int.Parse).ToList();
        var bitArray = new BitArray(MAX_RANGE);

        foreach (var number in integers)
        {
            if (number > 0 && number <= MAX_RANGE)
            {
                bitArray[number - 1] = true;  // Adjust for 1-based indexing
            }
        }

        var ranges = new StringBuilder();
        int start = -1;

        for (int i = 0; i < MAX_RANGE; i++)
        {
            if (bitArray[i])
            {
                if (start == -1)
                {
                    start = i;
                }
            }
            else
            {
                if (start != -1)
                {
                    if (ranges.Length > 0)
                    {
                        ranges.Append(",");
                    }
                    ranges.Append(start == i - 1 ? $"{start + 1}" : $"{start + 1}:{i}");  // Adjust for 1-based indexing
                    start = -1;
                }
            }
        }

        if (start != -1)
        {
            if (ranges.Length > 0)
            {
                ranges.Append(",");
            }
            ranges.Append(start == MAX_RANGE - 1 ? $"{start + 1}" : $"{start + 1}:{MAX_RANGE}");  // Adjust for 1-based indexing
        }

        // Write directly to TXT file
        File.WriteAllText(outputFilePath, ranges.ToString());
        Console.WriteLine($"Encoding successful. Output saved to {outputFilePath}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error during encoding: {ex.Message}");
    }
}

void Decode(string inputFilePath, string outputFilePath)
{
    try
    {
        var encodedString = File.ReadAllText(inputFilePath);
        var ranges = encodedString.Split(',');
        var integers = new List<int>();

        foreach (var range in ranges)
        {
            if (range.Contains(':'))
            {
                var parts = range.Split(':');
                int start = int.Parse(parts[0]);
                int end = int.Parse(parts[1]);
                for (int i = start; i <= end; i++)
                {
                    integers.Add(i);
                }
            }
            else
            {
                integers.Add(int.Parse(range));
            }
        }

        File.WriteAllText(outputFilePath, string.Join(",", integers));
        Console.WriteLine($"Decoding successful. Output saved to {outputFilePath}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error during decoding: {ex.Message}");
    }
}

string GenerateOutputFilePath(string operation, string extension)
{
    string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
    string fileName = $"{operation}_{timestamp}.{extension}";
    string directory = AppDomain.CurrentDomain.BaseDirectory;
    return Path.Combine(directory, fileName);
}

MainLoop();
