namespace FunctionalFun.ViewModel
{
    using System;
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Input;
    using Com.NaikVarun.WPFUtil.Commands;
    using Com.NaikVarun.WPFUtil.ViewModel;

    public class ViewModel : ViewModelBase
    {
        #region Delegate No Parameters
        private int _sliderValue;
        public int SliderValue
        {
            get
            {
                return _sliderValue;
            }
            set
            {
                _sliderValue = value;
                RaisePropertyChanged(() => this.SliderValue);
            }
        }

        private bool _canExecuteResetCommand = true;
        public bool CanExecuteResetCommand
        {
            get
            {
                return _canExecuteResetCommand;
            }
            set
            {
                _canExecuteResetCommand = value;
                RaisePropertyChanged(() => this.CanExecuteResetCommand);
            }
        }


        private ICommand _resetCommand = null;

        public ICommand ResetCommand
        {
            get
            {
                if (_resetCommand == null)
                {
                    _resetCommand = new DelegateCommand(ExecuteReset, CanExecuteReset);
                }
                return _resetCommand;
            }
        }

        public void ExecuteReset()
        {
            SliderValue = 0;
            MessageBox.Show("Command Executed. Slider Reset to 0");
        }

        public bool CanExecuteReset()
        {
            return CanExecuteResetCommand;
        }

        #endregion

        #region Delete With Parameters
        private ObservableCollection<Person> _people = new ObservableCollection<Person> {
                                                                                        new Person{ FName = "FName1", LName = "LName1"},
                                                                                        new Person{ FName = "FName2", LName = "LName2"},
                                                                                        new Person{ FName = "FName3", LName = "LName3"},
                                                                                        new Person{ FName = "FName4", LName = "LName4"},
                                                                                        new Person{ FName = "FName5", LName = "LName5"}
                                                                                        };
        public ObservableCollection<Person> People
        {
            get
            {
                return _people;
            }
            set
            {
                _people = value;
                RaisePropertyChanged(() => People);
            }
        }

        private Person _selectedPerson = null;
        public Person SelectedPerson
        {
            get
            {
                return _selectedPerson;
            }
            set
            {
                _selectedPerson = value;
                RaisePropertyChanged(() => SelectedPerson);
            }
        }


        private ICommand _loadCommand;
        public ICommand LoadCommand
        {
            get
            {
                if (_loadCommand == null)
                {
                    _loadCommand = new DelegateCommand<Person>(LoadPerson, CanLoadPerson);
                }
                return _loadCommand;
            }
        }

        private bool CanLoadPerson(Person person)
        {
            return (person != null);
        }

        private void LoadPerson(Person person)
        {
            MessageBox.Show("First Name: "+person.FName+"\nLast Name: " + person.LName);
        }


        public class Person
        {
            public String FName { get; set; }
            public String LName { get; set; }
            public String Name
            {
                get
                {
                    return LName + ", " + FName;
                }
            }
        }


        #endregion


    }
}
