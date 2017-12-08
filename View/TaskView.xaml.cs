using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CourseWork.Adapter;
using CourseWork.ViewModel;

namespace CourseWork.View {
    /// <summary>
    /// Interaction logic for TaskView.xaml
    /// </summary>
    public partial class TaskView : UserControl {
        public TaskView() {
            InitializeComponent();
        }

        public TaskAdapter Task {
            get { return (TaskAdapter)GetValue(TaskProperty); }
            set { SetValue(TaskProperty, value); }
        }

        public static readonly DependencyProperty TaskProperty =
            DependencyProperty.Register("Task", typeof(TaskAdapter), typeof(TaskView), new PropertyMetadata(null, (o, e) => {
                var obj = (TaskView)o;
                var val = (TaskAdapter)e.NewValue;
                var context = (TaskViewModel)obj.DataContext;
                context.SelectedTask = val;
            }));

        public bool IsIdEnabled {
            get { return (bool)GetValue(IsIdEnabledProperty); }
            set { SetValue(IsIdEnabledProperty, value); }
        }

        public static readonly DependencyProperty IsIdEnabledProperty =
            DependencyProperty.Register("IsIdEnabled", typeof(bool), typeof(TaskView), new PropertyMetadata(false, (o, e) => {
                var obj = (TaskView)o;
                var val = (bool)e.NewValue;
                var context = (TaskViewModel)obj.DataContext;
                context.IsIdEnabled = val;
            }));
    }
}
