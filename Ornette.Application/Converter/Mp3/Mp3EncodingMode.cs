namespace Ornette.Application.Converter.Mp3
{
    public enum Mp3EncodingMode
    {
        //
        // Summary:
        //     Default is JointStereo or Stereo depending on bitrate.
        Default = 0,
        //
        // Summary:
        //     Stereo
        Stereo = 1,
        //
        // Summary:
        //     Force LR stereo on all frames
        JointStereo = 2,
        //
        // Summary:
        //     Force MS stereo on all frames
        ForcedJointStereo = 3,
        //
        // Summary:
        //     Mono
        Mono = 4,
        //
        // Summary:
        //     Dual Mono
        DualMono = 5
    }
}