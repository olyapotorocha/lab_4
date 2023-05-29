using System;
using System.IO;

namespace lab__4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                ShowHelp();
                return;
            }

            foreach (string directoryPath in args)
            {
                if (!Directory.Exists(directoryPath))
                {
                    Console.WriteLine("The specified directory does not exist.");
                    return;
                }
                if (args.Length > 1 && args[1] == "-p")
                {
                    string[] subdirectories = Directory.GetDirectories(directoryPath);
                    Console.WriteLine("Subdirectories:");

                    foreach (string subdirectory in subdirectories)
                    {
                        Console.WriteLine(subdirectory);
                    }

                    return;
                }

                    long totalSize = CalculateDirectorySize(directoryPath);

                Console.WriteLine($"Amount of subdirectories bytes: {totalSize}");
            }
        }

        static long CalculateDirectorySize(string directoryPath)
        {
            long totalSize = 0;

            try
            {
                string[] files = Directory.GetFiles(directoryPath);

                foreach (string file in files)
                {
                    FileInfo fileInfo = new FileInfo(file);

                    
                    if ((fileInfo.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden||
                        (fileInfo.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly ||
                        (fileInfo.Attributes & FileAttributes.Archive) == FileAttributes.Archive)
                    {
                        totalSize += fileInfo.Length;
                    }
                }

                string[] subdirectories = Directory.GetDirectories(directoryPath);

                foreach (string subdirectory in subdirectories)
                {
                    totalSize += CalculateDirectorySize(subdirectory);
                }
            }
            catch (UnauthorizedAccessException)
            {
                
                Console.WriteLine($"Error: {directoryPath}");
            }

            return totalSize;
        }

        static void ShowHelp()
        {
            Console.WriteLine("DirectorySizeCalculator <directoryPath>");
            Console.WriteLine();
            Console.WriteLine("Calculates the size of subdirectories in the specified directory.");
        }

    }
}
    

