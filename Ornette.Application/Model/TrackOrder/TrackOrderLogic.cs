namespace Ornette.Application.Model.TrackOrder
{
    internal static class TrackOrderLogic
    {
        internal static ITrackOrderLogic GetLogic(bool random)
        {
            return new LinearTrackOrderLogic();
        }
    }
}
