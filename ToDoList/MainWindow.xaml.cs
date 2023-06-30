using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
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
                    return tasks.FindAll(task => !task.Completed);
                else
                    return tasks;
            }
        }

        private void UpdateTask_Click(object sender, RoutedEventArgs e)
        {
            // Find a task in the list
            Task taskToUpdate = lstTasks.SelectedItem as Task;

            // Check if a task is selected
            if (taskToUpdate != null)
            {
                // Update the description
                taskToUpdate.Description = "Updated Description";

                // Update the due date
                taskToUpdate.DueDate = DateTime.Now.AddDays(7);

                // Trigger the PropertyChanged event to update the UI
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Tasks"));
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

            Task task = new Task("Sample Task", "Sample Description", DateTime.Now);
            task.DueDate = DateTime.Now.AddDays(7); // Changing the due date
            task.Description = "Updated Description"; // Changing the description
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

        }

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

    public class Task : INotifyPropertyChanged
    {
        private string description;
        private DateTime dueDate;
        private bool completed;

        public string Title { get; set; }

        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                NotifyPropertyChanged();
            }
        }

        public DateTime DueDate
        {
            get { return dueDate; }
            set
            {
                dueDate = value;
                NotifyPropertyChanged();
            }
        }

        public bool Completed
        {
            get { return completed; }
            set
            {
                completed = value;
                NotifyPropertyChanged();
            }
        }

        public Task(string title, string description, DateTime dueDate)
        {
            Title = title;
            Description = description;
            DueDate = dueDate;
            Completed = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
