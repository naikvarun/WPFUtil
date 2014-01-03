namespace Com.NaikVarun.WPFUtil.AttachedProperties
{
    using System;
    using System.Windows;


    /// <summary>
    /// An internal class to handle listening for an event and executing a command, 
    /// when a Command is assigned to a particular DependencyProperty.
    /// </summary>
    class ExecuteCommandOnRoutedBehaviour: ExecuteCommandBehaviour
    {

        private readonly RoutedEvent _routedEvent;

        public ExecuteCommandOnRoutedBehaviour(RoutedEvent routedEvent)
        {
            _routedEvent = routedEvent;
        }

        /// <summary>
        /// Handles attaching or Detaching Event handlers when a Command is assigned or unassigned.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        protected override void AdjustEventHandlers(DependencyObject sender, object oldValue, object newValue)
        {
            UIElement element = sender as UIElement;
            if (element != null)
            {
                if (oldValue != null)
                {
                    element.RemoveHandler(_routedEvent, new RoutedEventHandler(EventHandler));
                }
                if (newValue != null)
                {
                    element.AddHandler(_routedEvent, new RoutedEventHandler(EventHandler));
                }
            }

        }

        protected void EventHandler(object sender, RoutedEventArgs e)
        {
            HandleEvent(sender, e);
        }
    }
}
