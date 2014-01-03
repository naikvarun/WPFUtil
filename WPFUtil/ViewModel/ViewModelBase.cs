namespace Com.NaikVarun.WPFUtil.ViewModel
{
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.Windows;

    public class ViewModelBase: INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private bool _isInDesign = DesignerProperties.GetIsInDesignMode(new DependencyObject());

        protected bool IsInDesignMode
        {
            get
            {
                return _isInDesign;
            }
        }

        protected void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            OnPropertyChanged(ExtractPropertyName(propertyExpression));  
        }

        private String ExtractPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            return ((MemberExpression)propertyExpression.Body).Member.Name;
        }

        private void OnPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
