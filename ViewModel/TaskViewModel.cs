using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseWork.Adapter;
using CourseWork.Models.Enum;
using GalaSoft.MvvmLight;
namespace CourseWork.ViewModel {
    public class TaskViewModel : ViewModelBase {

        private bool _isIdEnabled;
        public bool IsIdEnabled {
            get { return _isIdEnabled; }
            set {
                _isIdEnabled = value;
                RaisePropertyChanged(() => IsIdEnabled);
            }
        }

        private TaskAdapter _selectedTask;
        public TaskAdapter SelectedTask {
            get { return _selectedTask; }
            set {
                _selectedTask = value;
                RaisePropertyChanged(() => SelectedTask);
            }
        }

        private ObservableCollection<Priority> _priorities;
        public ObservableCollection<Priority> Priorities {
            get {
                if (_priorities == null)
                {
                    var values = System.Enum.GetValues(typeof(Priority)).Cast<Priority>();
                    _priorities = new ObservableCollection<Priority>(values);
                }
                return _priorities;
            }
        }

        
    }
}
