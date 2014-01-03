namespace Com.NaikVarun.WPFUtil.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Input;

    internal class CommandManagerHelper
    {
        /// <summary>
        ///     CallWeakReferenceHandlers
        /// </summary>
        /// <param name="handlers"></param>
        internal static void CallWeakReferenceHandlers(List<WeakReference> handlers)
        {
            if (handlers != null)
            {

                // Take a snapshot of handlers before we call out to them since 
                // the handlers could cause the array to be modified while we are reading it.
                EventHandler[] callees = new EventHandler[handlers.Count];
                int count = 0;
                for (int i = handlers.Count - 1; i >= 0; i--)
                {
                    WeakReference reference = handlers[i];
                    EventHandler handler = reference.Target as EventHandler;
                    if (handler == null)
                    {
                        // Clean up old handlers that have been collected
                        handlers.RemoveAt(i);
                    }
                    else
                    {
                        callees[count] = handler;
                        count++;
                    }
                }

                // Call the handlers that we snapshotted
                for (int i = 0; i < count; i++)
                {
                    EventHandler handler = callees[i];
                    handler(null, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        ///     AddHandlersToRequerySuggested
        /// </summary>
        /// <param name="handlers"></param>
        internal static void AddHandlersToRequerySuggested(List<WeakReference> handlers)
        {

            if (handlers != null)
            {
                foreach (WeakReference handlerRef in handlers)
                {
                    EventHandler handler = handlerRef.Target as EventHandler;
                    if (handler != null)
                    {
                        CommandManager.RequerySuggested += handler;
                    }
                }
            }
        }

        /// <summary>
        ///     RemoveHandlersToRequerySuggested
        /// </summary>
        /// <param name="handlers"></param>
        internal static void RemoveHandlersToRequerySuggested(List<WeakReference> handlers)
        {

            if (handlers != null)
            {
                foreach (WeakReference handlerRef in handlers)
                {
                    EventHandler handler = handlerRef.Target as EventHandler;
                    if (handler != null)
                    {
                        CommandManager.RequerySuggested -= handler;
                    }
                }
            }
        }

        /// <summary>
        ///     AddWeakReferenceHandler
        /// </summary>
        /// <param name="handlers"></param>
        /// <param name="handler"></param>
        internal static void AddWeakReferenceHandler(ref List<WeakReference> handlers, EventHandler handler)
        {
            AddWeakReferenceHandler(ref handlers, handler, -1);
        }

        /// <summary>
        ///     AddWeakReferenceHandlers
        /// </summary>
        /// <param name="handlers"></param>
        /// <param name="handler"></param>
        /// <param name="defaultSize"></param>
        internal static void AddWeakReferenceHandler(ref List<WeakReference> handlers, EventHandler handler, int defaultSize)
        {
            if (handlers == null)
            {
                handlers = (defaultSize > 0 ? new List<WeakReference>(defaultSize) : new List<WeakReference>());
                handlers.Add(new WeakReference(handler));
            }
        }

        /// <summary>
        ///     RemoveWeakReferenceHandler
        /// </summary>
        /// <param name="handlers"></param>
        /// <param name="handler"></param>
        internal static void RemoveWeakReferenceHandler(List<WeakReference> handlers, EventHandler handler)
        {
            if (handlers != null)
            {
                for (int i = handlers.Count - 1; i >= 0; i--)
                {
                    WeakReference reference = handlers[i];
                    EventHandler existingHandler = reference.Target as EventHandler;
                    if ((existingHandler != null) || (existingHandler == handler))
                    {
                        // Clean up old handlers that have been collected in addition to handler that is to be removed
                        handlers.RemoveAt(i);
                    }
                }
            }
        }
    }
}
