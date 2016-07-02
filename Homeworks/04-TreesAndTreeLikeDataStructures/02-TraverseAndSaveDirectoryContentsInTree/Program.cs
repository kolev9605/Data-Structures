namespace _02_TraverseAndSaveDirectoryContentsInTree
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    class Program
    {
        static void Main()
        {
            string dir = "C:\\MyData\\Games\\Installs";
            Folder rootFolder = new Folder();

            AddDirectories(dir, rootFolder);
            PrintDirectory(rootFolder);
            List<long> sizesOfFolders = new List<long>();
            CalculateSizeOfSubtree(rootFolder, sizesOfFolders);
            Console.WriteLine("The size of {0} is {1}Mb.",
                rootFolder.Name,
                GetMbFromBytes(sizesOfFolders.Sum()));
        }

        private static long GetMbFromBytes(long bytesSize)
        {
            long mbSize = bytesSize / 1024 / 1024;
            while (mbSize < 0)
            {
                mbSize *= 1024;
            }

            return mbSize;
        }

        private static void CalculateSizeOfSubtree(Folder rootFolder, IList<long> list)
        {
            list.Add(rootFolder.SizeOfFolder);
            foreach (var childFolder in rootFolder.ChildFolders)
            {
                CalculateSizeOfSubtree(childFolder, list);
            }
        }

        private static void PrintDirectory(Folder rootFolder, int indent = 0)
        {
            foreach (var file in rootFolder.Files)
            {
                Console.WriteLine("{0} * {1}, Size: {2}Mb", new string(' ', 2 * indent), file.Name, file.Size / 1024 / 1024);
            }

            foreach (var folder in rootFolder.ChildFolders)
            {
                Console.WriteLine("{0} -> {1}:", new string(' ', 2 * indent), folder.Name);
                PrintDirectory(folder, indent + 1);
            }
        }

        static void AddDirectories(string rootDir, Folder rootFolder)
        {
            DirectoryInfo info = new DirectoryInfo(rootDir);
            rootFolder.Name = info.Name;
            rootFolder.Path = info.FullName;

            //adding the files
            var files = info.GetFiles();
            var lisfOfFiles = new List<File>();
            foreach (var file in files)
            {
                File currentFile = new File(file.Name, file.FullName, file.Length);
                lisfOfFiles.Add(currentFile);
            }

            rootFolder.Files = lisfOfFiles.ToArray();
            rootFolder.SizeOfFolder = rootFolder.Files.Sum(x => x.Size);

            //adding the dirs
            var dirs = info.GetDirectories();
            var listOfDirs = new List<Folder>();
            foreach (var dir in dirs)
            {
                Folder currentFolder = new Folder();
                listOfDirs.Add(currentFolder);
                currentFolder.Name = dir.Name;
                currentFolder.Path = dir.FullName;
                AddDirectories(rootDir + "\\" + dir, currentFolder);
            }

            rootFolder.ChildFolders = listOfDirs.ToArray();
        }
    }
}
