using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data;
using System.Configuration;
using Npgsql;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        Task task = new Task();
        Task taskFromGrid = new Task();
        List<Task> tasks = new List<Task>();
        List<Comments> comments = new List<Comments>();
        List<User> users = new List<User>();
        Comments updatedComments = new Comments();
        Comments changeComments = new Comments();
        Comments commentFromGrid = new Comments();

        private ObservableCollection<String> statusCollection = new ObservableCollection<string>();
        private ObservableCollection<String> typeCollection = new ObservableCollection<string>();
        private ObservableCollection<String> userCollection = new ObservableCollection<string>();
        public ObservableCollection<User> Users { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            PopulateDataGrid();
            PopulateUserName();

            StatusCollection.Add("Completed");
            StatusCollection.Add("Uncompleted");

            TypeCollection.Add("Low priority");
            TypeCollection.Add("Middle priority");
            TypeCollection.Add("High priority");

            using (var context = new TaskManagementContext())
            {
                var tasks = context.Tasks.ToArray();
            }
        }

        public ObservableCollection<string> StatusCollection
        {
            get { return statusCollection; }
            set
            {

                statusCollection = value;
                NotifyPropertyChanged();
            } 
        }

        public ObservableCollection<string> TypeCollection
        {
            get { return typeCollection; }
            set
            {

                typeCollection = value;
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<string> UserCollection
        {
            get { return userCollection; }
            set
            {
                typeCollection = value;
                NotifyPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private void InsertBtn_Clicked(object sender, RoutedEventArgs e)
        {
            task.TaskName = InputBoxTaskName.Text;
            task.CreatedDate = InputBoxCreatedDate.DisplayDate;
            task.RequiredByDate = InputBoxRequiredByDate.DisplayDate;
            task.TaskDescription = InputBoxTaskDescription.Text;
            task.TaskStatus = InputBoxTaskStatus.Text;
            task.TaskType = InputBoxTaskType.Text;
            task.User = InputBoxUserName.Text;
            task.NextActionDate = InputBoxNextActionDate.DisplayDate;

            using (TaskManagementContext db = new TaskManagementContext())
            {
                db.Tasks.Add(task);
                db.SaveChanges();
            }
            task = null;

            Clear();
        }

        private void PopulateDataGrid()
        {
            using (var context = new TaskManagementContext())
            {
                tasks = context.Tasks.ToList();
            }
            McDataGrid.ItemsSource = tasks;
        }

        private void McDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            foreach (var item in e.AddedCells)
            {
                try
                {
                    var col = item.Column as DataGridColumn;
                    var fc = col.GetCellContent(item.Item);
                    taskFromGrid.TaskID = Int32.Parse((fc as TextBlock).Text);
                }
                catch (Exception c) {
                }
            }
            PopulateDataGridComments();
        }

        private void PopulateDataGridComments()
        {
            using (var context = new TaskManagementContext())
            {
                comments = context.Comments
                                .Where(s => s.Task.TaskID == taskFromGrid.TaskID).ToList();
            }
            CommentDataGrid.ItemsSource = comments;
        }

        private void PopulateUserName()
        {
            using (var context = new TaskManagementContext())
            {
                users = context.Users.ToList();
            }

            foreach(User user in users)
            {
                UserCollection.Add(user.UserName);
            }
        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            if (changeComments != null)
            {
                changeComments.CommentID = Int32.Parse(InputBoxCommentID.Text);
                changeComments.Comment = InputBoxComment.Text;
                changeComments.DateAddedComment = InputBoxDateAddedComment.DisplayDate;
                changeComments.ReminderDateComment = InputBoxReminderComment.DisplayDate;
            }

            using (TaskManagementContext context = new TaskManagementContext())
            {
                {
                    Comments std = context.Comments.
                        Where(s => s.CommentID == changeComments.CommentID).FirstOrDefault();
                    std.CommentID = changeComments.CommentID;
                    std.Comment = changeComments.Comment;
                    std.DateAddedComment = changeComments.DateAddedComment;
                    std.ReminderDateComment = changeComments.ReminderDateComment;

                    context.SaveChanges();
                }
            }
            PopulateDataGridComments();
            Clear();
        }

        private void CommentDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            foreach (var item in e.AddedCells)
            {
                try
                {
                    var col = item.Column as DataGridColumn;
                    var fc = col.GetCellContent(item.Item);
                    commentFromGrid.CommentID = Int32.Parse((fc as TextBlock).Text);
                }
                catch (Exception c)
                {
                }
                using (var context = new TaskManagementContext())
                {
                    changeComments = context.Comments
                                    .Where(s => s.CommentID == commentFromGrid.CommentID).FirstOrDefault();
                }
                if (changeComments != null)
                {
                    InputBoxCommentID.Text = changeComments.CommentID.ToString();
                    InputBoxComment.Text = changeComments.Comment.ToString();
                    InputBoxDateAddedComment.SelectedDate = DateTime.Parse(changeComments.DateAddedComment.ToString());
                    InputBoxReminderComment.SelectedDate = DateTime.Parse(changeComments.ReminderDateComment.ToString());
                }

            }
        }

        public void Clear() {

            InputBoxTaskName.Text = "";
            InputBoxTaskDescription.Text = "";
            InputBoxTaskStatus.Text = "";
            InputBoxTaskType.Text = "";
            InputBoxUserName.Text = "";
            InputBoxCommentID.Text = "";
            InputBoxComment.Text = "";


        }

        
    }


}

