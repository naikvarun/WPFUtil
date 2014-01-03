namespace Com.NaikVarun.WPFUtil.AttachedProperties
{
    using System;
    using System.Windows;
    using System.Windows.Input;

    internal abstract class ExecuteCommandBehaviour
    {
        protected DependencyProperty _property;
        protected abstract void AdjustEventHandlers(DependencyObject sender, object oldValue, object newValue);


        protected void HandleEvent(object sender, EventArgs e)
        {
            DependencyObject dp = sender as DependencyObject;
            if (dp != null)
            {
                ICommand command = dp.GetValue(_property) as ICommand;
                if (command != null)
                {
                    if (command.CanExecute(e))
                    {
                        command.Execute(e);
                    }
                }
            }
        }

        /// <summary>
        /// Listens for a change in the DependencyProperty that we are assigned to, 
        /// and adjusts the EventHandlers accordingly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void PropertyChangedHandler(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        { 
            // the first time the property changes,
            // make a note of which property we are supposed
            // be watching
            if (_property == null)
            {
                _property = e.Property;
            }
            object oldValue = e.OldValue;
            object newValue = e.NewValue;

            AdjustEventHandlers(sender, oldValue, newValue);
        }

    }
}
