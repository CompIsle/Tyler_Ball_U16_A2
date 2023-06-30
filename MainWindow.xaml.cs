using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace ToDoList
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private List<Task> tasks;
        private bool showIncompleteOnly;

        public List<Task> Tasks
        {
            get
            {
                if (showIncompleteOnly)
                    return tasks.FindAll(task => !task.Completed);      // shows the incomplete tasks
                else
                    return tasks;
            }
        }
        private void CompleteTask_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Task task = button.DataContext as Task;

            if (task != null)
            {
                task.Completed = true;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Tasks"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindow()
        {
            InitializeComponent();
            tasks = new List<Task>();
            showIncompleteOnly = false;
            DataContext = this;
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            string title = txtTitle.Text;
            string description = txtDescription.Text;
            DateTime? dueDate = dpDueDate.SelectedDate;

            if (!string.IsNullOrEmpty(title) && dueDate.HasValue)
            {
                Task newTask = new Task(title, description, dueDate.Value);
                tasks.Add(newTask);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Tasks"));
                ResetInputs();
            }
            else
            {
                MessageBox.Show("Please enter title and due date.");           
            }
            //If user has entered no title or date
        }

        // resets the text boxes
        private void ResetInputs()
        {
            txtTitle.Text = string.Empty;
            txtDescription.Text = string.Empty;
            dpDueDate.SelectedDate = null;
        }

        private void ShowIncomplete_Checked(object sender, RoutedEventArgs e)
        {
            showIncompleteOnly = true;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Tasks"));      
        }

        private void ShowIncomplete_Unchecked(object sender, RoutedEventArgs e)
        {
            showIncompleteOnly = false;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Tasks"));
        }
    }
 // represents the task as title, description, due date and completion ststus
    public class Task      
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool Completed { get; set; }

        public Task(string title, string description, DateTime dueDate)
        {
            Title = title;
            Description = description;
            DueDate = dueDate;
            Completed = false;
        }
    }
}

