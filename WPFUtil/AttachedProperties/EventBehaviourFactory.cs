namespace Com.NaikVarun.WPFUtil.AttachedProperties
{
    using System;
    using System.Windows;
    using System.Windows.Input;


    public static class EventBehaviourFactory
    {
        public static DependencyProperty CreateCommandExecutionEventBehaviour(RoutedEvent routedEvent, String propertyName, Type ownerType)
        {
            DependencyProperty property = DependencyProperty.RegisterAttached(propertyName, typeof(ICommand), ownerType, new PropertyMetadata(null, new ExecuteCommandOnRoutedBehaviour(routedEvent).PropertyChangedHandler));

            return property;
        }
    }
}
