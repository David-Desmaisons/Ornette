namespace Ornette.Application.Model.Descriptions
{
    public class ImageDescription
    {
        public ImageDescription(string path, string description)
        {
            Description = description;
            Path = path;
        }

        public string Description { get; }
        public string Path { get; }
    }
}
