namespace _02_TraverseAndSaveDirectoryContentsInTree
{
    public class File
    {
        public File(string name, string path, long size)
        {
            this.Name = name;
            this.Size = size;
            this.Path = path;
        }

        public string Name { get; set; }

        public string Path { get; set; }

        public long Size { get; set; }
    }
}
