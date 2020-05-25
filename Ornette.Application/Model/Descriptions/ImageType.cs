namespace Ornette.Application.Model.Descriptions
{
    public enum ImageType
    {
        //
        // Summary:
        //     The picture is of a type other than those specified.
        Other = 0,
        //
        // Summary:
        //     The picture is a 32x32 PNG image that should be used when displaying the file
        //     in a browser.
        FileIcon = 1,
        //
        // Summary:
        //     The picture is of an icon different from TagLib.PictureType.FileIcon.
        OtherFileIcon = 2,
        //
        // Summary:
        //     The picture is of the front cover of the album.
        FrontCover = 3,
        //
        // Summary:
        //     The picture is of the back cover of the album.
        BackCover = 4,
        //
        // Summary:
        //     The picture is of a leaflet page including with the album.
        LeafletPage = 5,
        //
        // Summary:
        //     The picture is of the album or disc itself.
        Media = 6,
        //
        // Summary:
        //     The picture is of the lead artist or soloist.
        LeadArtist = 7,
        //
        // Summary:
        //     The picture is of the artist or performer.
        Artist = 8,
        //
        // Summary:
        //     The picture is of the conductor.
        Conductor = 9,
        //
        // Summary:
        //     The picture is of the band or orchestra.
        Band = 10,
        //
        // Summary:
        //     The picture is of the composer.
        Composer = 11,
        //
        // Summary:
        //     The picture is of the lyricist or text writer.
        Lyricist = 12,
        //
        // Summary:
        //     The picture is of the recording location or studio.
        RecordingLocation = 13,
        //
        // Summary:
        //     The picture is one taken during the track's recording.
        DuringRecording = 14,
        //
        // Summary:
        //     The picture is one taken during the track's performance.
        DuringPerformance = 15,
        //
        // Summary:
        //     The picture is a capture from a movie screen.
        MovieScreenCapture = 16,
        //
        // Summary:
        //     The picture is of a large, colored fish.
        ColoredFish = 17,
        //
        // Summary:
        //     The picture is an illustration related to the track.
        Illustration = 18,
        //
        // Summary:
        //     The picture contains the logo of the band or performer.
        BandLogo = 19,
        //
        // Summary:
        //     The picture is the logo of the publisher or record company.
        PublisherLogo = 20,
        //
        // Summary:
        //     In fact, this is not a Picture, but another file-type.
        NotAPicture = 255
    }
}
