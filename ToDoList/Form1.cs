using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToDoList
{
    public partial class Form1 : Form
    {
        private List<Task> tasks;
        private int editingIndex = -1;

        public Form1()
        {
            InitializeComponent();
            tasks = new List<Task>();
            InitializeCategories();
        }

        private void InitializeCategories()
        {
            comboBoxCategory.Items.AddRange(new string[] {
                "Работа", "Учеба", "Личное", "Прочее"
            });
            comboBoxCategory.SelectedIndex = 0;
            numericUpDownPriority.Minimum = 1;
            numericUpDownPriority.Maximum = 5;
            numericUpDownPriority.Value = 3;
        }

        private void RefreshTasksList()
        {
            listBoxTasks.Items.Clear();
            foreach (var task in tasks)
            {
                listBoxTasks.Items.Add(task);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxTitle.Text))
            {
                MessageBox.Show("Введите название задачи!");
                return;
            }

            Task newTask = new Task(
                textBoxTitle.Text.Trim(),
                dateTimePickerDueDate.Value,
                comboBoxCategory.SelectedItem.ToString(),
                (int)numericUpDownPriority.Value
            );

            tasks.Add(newTask);
            RefreshTasksList();
            textBoxTitle.Clear();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (listBoxTasks.SelectedIndex >= 0)
            {
                int selectedIndex = listBoxTasks.SelectedIndex;
                tasks.RemoveAt(selectedIndex);
                RefreshTasksList();
            }
            else
            {
                MessageBox.Show("Выберите задачу для удаления!");
            }
        }

        private void listBoxTasks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxTasks.SelectedIndex >= 0)
            {
                Task selectedTask = tasks[listBoxTasks.SelectedIndex];
                checkBoxCompleted.Checked = selectedTask.IsCompleted;
            }
        }

        private void checkBoxCompleted_CheckedChanged(object sender, EventArgs e)
        {
            if (listBoxTasks.SelectedIndex >= 0)
            {
                Task selectedTask = tasks[listBoxTasks.SelectedIndex];
                selectedTask.IsCompleted = checkBoxCompleted.Checked;
                RefreshTasksList();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tasks = tasks.OrderBy(t => t.Title).ToList();
            RefreshTasksList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tasks = tasks.OrderBy(t => t.Category).ToList();
            RefreshTasksList();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tasks = tasks.OrderBy(t => t.Priority).ToList();
            RefreshTasksList();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            tasks = tasks.OrderBy(t => t.DueDate).ToList();
            RefreshTasksList();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (listBoxTasks.SelectedIndex >= 0)
            {
                editingIndex = listBoxTasks.SelectedIndex;
                Task task = tasks[editingIndex];

                textBoxTitle.Text = task.Title;
                dateTimePickerDueDate.Value = task.DueDate;
                numericUpDownPriority.Value = task.Priority;
                comboBoxCategory.SelectedItem = task.Category;

                textBoxTitle.Focus();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (editingIndex >= 0 && !string.IsNullOrWhiteSpace(textBoxTitle.Text))
            {
                tasks[editingIndex].Title = textBoxTitle.Text.Trim();
                tasks[editingIndex].DueDate = dateTimePickerDueDate.Value;
                tasks[editingIndex].Category = comboBoxCategory.SelectedItem?.ToString();
                tasks[editingIndex].Priority = (int)numericUpDownPriority.Value;

                editingIndex = -1;
                RefreshTasksList();
                textBoxTitle.Clear();
            }
        }
    }
}    