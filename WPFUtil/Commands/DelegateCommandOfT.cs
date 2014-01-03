namespace Com.NaikVarun.WPFUtil.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Input;


    public class DelegateCommand<T> : ICommand
    {
        #region Data
        private readonly Action<T> _executeMethod = null;
        private readonly Func<T, bool> _canExecuteMethod = null;
        private bool _isAutomaticRequeryDisabled = false;
        private List<WeakReference> _canExecuteChangeHandlers;
        #endregion

        #region Constructors

        public DelegateCommand(Action<T> executeMethod)
            : this(executeMethod, null, false)
        { }

        public DelegateCommand(Action<T> executeMethod, Func<T, bool> canExecuteMethod)
            : this(executeMethod, canExecuteMethod, false)
        { }

        public DelegateCommand(Action<T> executeMethod, Func<T, bool> canExecutedMethod, bool isAutomaticRequeryDisabled)
        {
            if (executeMethod == null)
            {
                throw new ArgumentNullException("executeMethod");
            }

            _executeMethod = executeMethod;
            _canExecuteMethod = canExecutedMethod;
            _isAutomaticRequeryDisabled = isAutomaticRequeryDisabled;
        }
        #endregion

        #region Public Method
        /// <summary>
        ///     Method to determine if command can be executed
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(T parameter)
        {
            if (_canExecuteMethod != null)
            {
                _canExecuteMethod(parameter);
            }
            return true;
        }

        /// <summary>
        ///     Execution of the command
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(T parameter)
        {
            if (_executeMethod != null)
            {
                _executeMethod(parameter);
            }
        }

        /// <summary>
        ///     Raises the CanExecuteChanged event
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            OnCanExecuteChanged();
        }

        /// <summary>
        ///     Protected virtual method to raise CanExecuteChagned event
        /// </summary>
        protected virtual void OnCanExecuteChanged()
        {
            CommandManagerHelper.CallWeakReferenceHandlers(_canExecuteChangeHandlers);
        }

        /// <summary>
        ///     Property to enable or disable CommandManager's automatic requery on this command
        /// </summary>
        public bool IsAutomaticRequeryDisabled
        {
            get
            {
                return _isAutomaticRequeryDisabled;
            }
            set
            {
                if (_isAutomaticRequeryDisabled != value)
                {
                    if (value)
                    {
                        CommandManagerHelper.RemoveHandlersToRequerySuggested(_canExecuteChangeHandlers);
                    }
                    else
                    {
                        CommandManagerHelper.AddHandlersToRequerySuggested(_canExecuteChangeHandlers);
                    }
                    _isAutomaticRequeryDisabled = value;
                }
            }
        }
        #endregion

        #region ICommand Members
        /// <summary>
        ///     ICommand.CanExecuteChanged implementation
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (!_isAutomaticRequeryDisabled)
                {
                    CommandManager.RequerySuggested += value;
                }
                CommandManagerHelper.AddWeakReferenceHandler(ref _canExecuteChangeHandlers, value, 2);
            }
            remove
            {
                if (!_isAutomaticRequeryDisabled)
                {
                    CommandManager.RequerySuggested -= value;
                }
                CommandManagerHelper.RemoveWeakReferenceHandler(_canExecuteChangeHandlers, value);
            }
        }


        /// <summary>
        ///     ICommand.CanExecute
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        bool ICommand.CanExecute(object parameter)
        {
            // If T is of value type and parameter is not set yet, then return false if CanExecute delegate exists, 
            // else return true
            if (parameter == null && typeof(T).IsValueType)
            {
                return _canExecuteMethod == null;
            }
            return CanExecute((T)parameter);
        }

        /// <summary>
        ///     ICommand.Execute
        /// </summary>
        /// <param name="parameter"></param>
        void ICommand.Execute(object parameter)
        {
            Execute((T)parameter);
        }

        #endregion

    }
}
