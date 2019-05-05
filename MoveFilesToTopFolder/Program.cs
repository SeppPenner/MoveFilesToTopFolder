using System;
using System.IO;

namespace MoveFilesToTopFolder
{
    public class Program
    {
        public static void Main()
        {
            TrySearchDirectory(@"E:\\Others", @"E:\\Musik");
            Console.Write("Finished, exit with enter");
            Console.Read();
        }

        private static void TrySearchDirectory(string sourceDirectory, string targetDirectory)
        {
            try
            {
                SearchFilesAndDirectories(sourceDirectory, targetDirectory);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }
        }

        private static void SearchFilesAndDirectories(string sourceDirectory, string targetDirectory)
        {
            CopyFiles(sourceDirectory, targetDirectory);
            SearchDirectories(sourceDirectory, targetDirectory);
        }

        private static void CopyFiles(string sourceDirectory, string targetDirectory)
        {
            foreach (var file in Directory.GetFiles(sourceDirectory, "*.mp3"))
            {
                Console.WriteLine("Copying file from " + file + " to " + Path.Combine(targetDirectory, Path.GetFileName(file)));
                var targetFile = Path.Combine(targetDirectory, Path.GetFileName(file));
                if(!File.Exists(targetFile))
                    File.Move(file, Path.Combine(targetDirectory, Path.GetFileName(file)));
                else
                {
                    var fileParts = Path.GetFileName(file).Split('-');
                    targetFile = fileParts[0] + " (2)-" + fileParts[1];
                    File.Move(file, Path.Combine(targetDirectory, targetFile));
                }
            }
        }

        private static void SearchDirectories(string sourceDirectory, string targetDirectory)
        {
            foreach (var dir in Directory.GetDirectories(sourceDirectory))
                TrySearchDirectory(dir, targetDirectory);
        }
    }
}