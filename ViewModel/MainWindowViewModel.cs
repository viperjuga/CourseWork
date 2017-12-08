using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using CourseWork.Adapter;
using CourseWork.Enum;
using CourseWork.Model;
using CourseWork.View;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace CourseWork.ViewModel {
    public class MainWindowViewModel : ViewModelBase {
        public MainWindowViewModel() {
            //TODO:Load tasks from file
            _tasks = new ObservableCollection<TaskAdapter>()
            {
                new TaskAdapter(new Task(){ Id = 0, Name = "Make course work", Priority = Priority.Major, Description = "We need to start it now", StartTime = DateTime.Now, EndTime = DateTime.ParseExact("20122017","ddMMyyyy", CultureInfo.CurrentCulture)})
            };
        }


        private ObservableCollection<TaskAdapter> _tasks;
        public ObservableCollection<TaskAdapter> Tasks {
            get {
                if (_tasks == null) {
                    _tasks = new ObservableCollection<TaskAdapter>();
                }
                return _tasks;
            }
        }

        private TaskAdapter _seletedTask;
        public TaskAdapter SelectedTask {
            get { return _seletedTask; }
            set {
                _seletedTask = value;
                RaisePropertyChanged(() => SelectedTask);
            }
        }

        private RelayCommand _addCommand;
        public RelayCommand AddCommand {
            get {
                if (_addCommand == null) {
                    _addCommand = new RelayCommand(
                        () => {
                            var newTaskWindow = new NewTaskWindow();
                            newTaskWindow.ShowDialog();
                            var context = (NewTaskViewModel)newTaskWindow.DataContext;
                            if (context.Ok) {
                                Tasks.Add(context.NewTask);
                                MessageBox.Show("Task succesffully added.", "Add", MessageBoxButton.OK,
                                    MessageBoxImage.Information);
                            }
                        },
                    () => true);
                }
                return _addCommand;
            }
        }
    }
}
