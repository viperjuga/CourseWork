using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CourseWork.Models.Enum;
using CourseWork.Models.Model;
using GalaSoft.MvvmLight;

namespace CourseWork.Adapter {
    public class TaskAdapter : ObservableObject {
        public TaskAdapter(Task model)
        {
            _id = model.Id;
            _name = model.Name;
            _description = model.Description;
            _startTime = model.StartTime;
            _endTime = model.EndTime;
            _priority = model.Priority;
        }

        private Guid _id;
        public Guid Id {
            get { return _id; }
            set {
                _id = value;
                RaisePropertyChanged(() => Id);
            }
        }

        private string _name;
        public string Name {
            get { return _name; }
            set {
                _name = value;
                RaisePropertyChanged(() => Name);
            }
        }

        private string _description;
        public string Description {
            get { return _description; }
            set {
                _description = value;
                RaisePropertyChanged(() => Description);
            }
        }

        private DateTime _startTime;
        public DateTime StartTime {
            get { return _startTime; }
            set {
                _startTime = value;
                RaisePropertyChanged(() => StartTime);
            }
        }

        private DateTime _endTime;
        public DateTime EndTime {
            get { return _endTime; }
            set {
                _endTime = value;
                RaisePropertyChanged(() => EndTime);
            }
        }

        private Priority _priority;
        public Priority Priority {
            get { return _priority; }
            set {
                _priority = value;
                RaisePropertyChanged(() => Priority);
            }
        }

        
    }
}
