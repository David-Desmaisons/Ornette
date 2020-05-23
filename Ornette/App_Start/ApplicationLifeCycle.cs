using System.ComponentModel;
using Neutronium.BuildingBlocks.Application.LifeCycleHook;
using Neutronium.BuildingBlocks.Application.Navigation;
using Neutronium.BuildingBlocks.Application.WindowServices;
using Neutronium.BuildingBlocks.ApplicationTools;

namespace Ornette.UI
{
    /// <summary>
    /// Application lifecycle implementation that will be used as a singleton.
    /// To be used to react to navigation and closing events.
    /// </summary>
    public class ApplicationLifeCycle : IApplicationLifeCycle
    {
        private readonly IMessageBox _MessageBox;
        private readonly IApplication _Application;

        public ApplicationLifeCycle(IMessageBox messageBox, IApplication application)
        {
            _MessageBox = messageBox;
            _Application = application;
        }

        /// <summary>
        /// Sent before navigation, allows cancellation and redirect
        /// </summary>
        /// <param name="routingEvent"></param>
        public void OnNavigating(RoutingEventArgs routingEvent)
        {
        }

        /// <summary>
        /// Sent after navigation
        /// </summary>
        /// <param name="routedEvent"></param>
        public void OnNavigated(RoutedEventArgs routedEvent)
        {
        }

        /// <summary>
        /// Sent on application closing, allows cancellation
        /// </summary>
        /// <param name="cancelEvent"></param>
        public async void OnClosing(CancelEventArgs cancelEvent)
        {
            cancelEvent.Cancel = true;
            var confirmationMessage = new ConfirmationMessage(Resource.ConfirmationNeeded, Resource.DoYouWantToCloseApplication, Resource.Ok, Resource.Cancel);
            var close = await _MessageBox.ShowMessage(confirmationMessage);
            if (close)
                _Application.ForceClose();
        }

        /// <summary>
        /// Sent on session closing, allows cancellation
        /// </summary>
        /// <param name="cancelEvent"></param>
        public void OnSessionEnding(CancelEventArgs cancelEvent)
        {
        }

        /// <summary>
        /// Sent on application closed
        /// </summary>
        public void OnClosed()
        {
        }
    }
}
