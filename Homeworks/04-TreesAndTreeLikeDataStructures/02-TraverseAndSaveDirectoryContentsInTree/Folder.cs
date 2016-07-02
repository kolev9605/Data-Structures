namespace _02_TraverseAndSaveDirectoryContentsInTree
{
    public class Folder
    {
        public string Name { get; set; }

        public string Path { get; set; }

        public File[] Files { get; set; }

        public Folder[] ChildFolders { get; set; }

        public long SizeOfFolder { get; set; }
    }
}
