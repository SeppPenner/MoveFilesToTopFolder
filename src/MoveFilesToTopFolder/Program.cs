// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//   The main program.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MoveFilesToTopFolder
{
    using System;
    using System.IO;

    /// <summary>
    /// The main program.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main method.
        /// </summary>
        public static void Main()
        {
            TrySearchDirectory(@"E:\\Others", @"E:\\Musik");
            Console.Write("Finished, exit with enter");
            Console.Read();
        }

        /// <summary>
        /// Tries to search the directory.
        /// </summary>
        /// <param name="sourceDirectory">The source directory.</param>
        /// <param name="targetDirectory">The target directory.</param>
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

        /// <summary>
        /// Searches files and directories.
        /// </summary>
        /// <param name="sourceDirectory">The source directory.</param>
        /// <param name="targetDirectory">The target directory.</param>
        private static void SearchFilesAndDirectories(string sourceDirectory, string targetDirectory)
        {
            CopyFiles(sourceDirectory, targetDirectory);
            SearchDirectories(sourceDirectory, targetDirectory);
        }

        /// <summary>
        /// Copies the files.
        /// </summary>
        /// <param name="sourceDirectory">The source directory.</param>
        /// <param name="targetDirectory">The target directory.</param>
        private static void CopyFiles(string sourceDirectory, string targetDirectory)
        {
            foreach (var file in Directory.GetFiles(sourceDirectory, "*.mp3"))
            {
                Console.WriteLine("Copying file from " + file + " to " + Path.Combine(targetDirectory, Path.GetFileName(file)));
                var targetFile = Path.Combine(targetDirectory, Path.GetFileName(file));

                if (!File.Exists(targetFile))
                {
                    File.Move(file, Path.Combine(targetDirectory, Path.GetFileName(file)));
                }
                else
                {
                    var fileParts = Path.GetFileName(file).Split('-');
                    targetFile = fileParts[0] + " (2)-" + fileParts[1];
                    File.Move(file, Path.Combine(targetDirectory, targetFile));
                }
            }
        }

        /// <summary>
        /// Searches the directories.
        /// </summary>
        /// <param name="sourceDirectory">The source directory.</param>
        /// <param name="targetDirectory">The target directory.</param>
        private static void SearchDirectories(string sourceDirectory, string targetDirectory)
        {
            foreach (var dir in Directory.GetDirectories(sourceDirectory))
            {
                TrySearchDirectory(dir, targetDirectory);
            }
        }
    }
}