using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using CourseWork.Adapter;
using CourseWork.FileWorker.Helper;
using CourseWork.Helper;
using CourseWork.Models.Enum;
using CourseWork.Models.Model;
using CourseWork.View;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;

namespace CourseWork.ViewModel {
    public class MainWindowViewModel : ViewModelBase {
        public MainWindowViewModel() {
            var tasks = CourseWorkFileWorker.LoadFromFile();
            foreach (var task in tasks) {
                Tasks.Add(new TaskAdapter(task));
            }
        }


        private ObservableCollection<TaskAdapter> _tasks;
        public ObservableCollection<TaskAdapter> Tasks {
            get {
                if (_tasks == null) {
                    _tasks = new ObservableCollection<TaskAdapter>();
                    _tasks.CollectionChanged += (sender, args) => {
                        DeadlineTasks.Clear();
                        Tasks.OrderBy(x => x.EndTime)
                        .Take(3)
                        .ToList().ForEach(e => {
                            DeadlineTasks.Add(e);
                        });
                        RaisePropertyChanged(() => DeadlineTasks);
                    };
                }
                return _tasks;
            }
        }

        public ObservableCollection<TaskAdapter> PreviewTasks {
            get {
                if (!string.IsNullOrWhiteSpace(SearchText)) {
                    return new ObservableCollection<TaskAdapter>(Tasks.Where(e =>
                        e.Name.ToLower().Contains(SearchText.ToLower()) ||
                        e.Id.ToString().ToLower().Contains(SearchText.ToLower()) ||
                        e.Description.ToLower().Contains(SearchText.ToLower()) ||
                        e.Priority.ToString().ToLower().Contains(SearchText.ToLower()) ||
                        e.StartTime.ToString("ddMMyyyyHHmm").ToLower().Contains(SearchText.ToLower()) ||
                        e.EndTime.ToString("ddMMyyyyHHmm").ToLower().Contains(SearchText.ToLower())).ToList());
                    ;
                }
                return Tasks;
            }
        }

        private ObservableCollection<TaskAdapter> _deadlinesTasks;
        public ObservableCollection<TaskAdapter> DeadlineTasks {
            get {
                if (_deadlinesTasks == null) {
                    _deadlinesTasks = new ObservableCollection<TaskAdapter>();
                }
                return _deadlinesTasks;
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

        private string _searchText;
        public string SearchText {
            get { return _searchText; }
            set {
                _searchText = value;
                RaisePropertyChanged(() => PreviewTasks);
                RaisePropertyChanged(() => SearchText);
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

        private RelayCommand _deleteCommand;
        public RelayCommand DeleteCommand {
            get {
                if (_deleteCommand == null) {
                    _deleteCommand = new RelayCommand(
                        () => {
                            var result = MessageBox.Show("Are you sure?", "Sure", MessageBoxButton.YesNo);
                            if (result == MessageBoxResult.Yes) {
                                Tasks.Remove(SelectedTask);
                            }
                            MessageBox.Show("Task succesffully deleted.", "Remove", MessageBoxButton.OK,
                                MessageBoxImage.Information);
                        }, () => true
                        );
                }
                return _deleteCommand;
            }
        }

        public void SaveData() {
            CourseWorkFileWorker.SaveToFile(Tasks.Select(e => e.ToTask()).ToList());
        }

        private RelayCommand _reportCommand;
        public RelayCommand ReportCommand {
            get {
                if (_reportCommand == null) {
                    _reportCommand = new RelayCommand(
                        () => {
                            var dlg = new SaveFileDialog {
                                FileName = "Report",
                                DefaultExt = ".html",
                                Filter = "HyperText Markup Language documents (.html)|*.html"
                            };

                            var result = dlg.ShowDialog();

                            if (result == true) {
                                string filename = dlg.FileName;
                               
                                CourseWorkFileWorker.GenerateHtmlDoc(filename, Tasks.Select(e => e.ToTask()).ToList());
                                MessageBox.Show("Report succesffully created.", "Report", MessageBoxButton.OK,
                                    MessageBoxImage.Information);
                            }


                        }, () => true
                    );
                }
                return _reportCommand;
            }
        }
    }
}
