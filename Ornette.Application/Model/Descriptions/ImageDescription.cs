namespace Ornette.Application.Model.Descriptions
{
    public class ImageDescription
    {
        public ImageDescription(string path, string description, ImageType imageType)
        {
            Description = description;
            Path = path;
            Type = imageType;
        }

        public string Description { get; }
        public string Path { get; }
        public ImageType Type { get; }
    }
}
