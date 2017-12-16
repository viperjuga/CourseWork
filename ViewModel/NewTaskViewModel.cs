using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using CourseWork.Adapter;
using CourseWork.Models.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace CourseWork.ViewModel {
    public class NewTaskViewModel : ViewModelBase {
        public NewTaskViewModel() {
            _newTask = new TaskAdapter(new Task());
        }

        private TaskAdapter _newTask;
        public TaskAdapter NewTask
        {
            get { return _newTask; }
            set
            {
                _newTask = value;
                RaisePropertyChanged(() => NewTask);
            }
        }

        private bool _ok;
        public bool Ok {
            get { return _ok; }
            set {
                _ok = value;
                RaisePropertyChanged(() => Ok);
            }
        }

        private RelayCommand<Window> _confirmCommand;
        public RelayCommand<Window> ConfirmCommand {
            get {
                if (_confirmCommand == null) {
                    _confirmCommand = new RelayCommand<Window>(
                        win => {
                            _ok = true;
                            win.Close();
                        },
                   win => true);
                }
                return _confirmCommand;
            }
        }

        private RelayCommand<Window> _cancelCommand;
        public RelayCommand<Window> CancelCommand {
            get {
                if (_cancelCommand == null) {
                    _cancelCommand = new RelayCommand<Window>(
                        win => {
                            _ok = false;
                            win.Close();
                        },
                   win => true);
                }
                return _cancelCommand;
            }
        }
    }
}
