namespace Ornette.Application.Model.Descriptions
{
    public class ImageDescription
    {
        public ImageDescription(string uri, string description, ImageType imageType)
        {
            Description = description;
            Uri = uri;
            Type = imageType;
        }

        public string Description { get; }
        public string Uri { get; }
        public ImageType Type { get; }
    }
}
