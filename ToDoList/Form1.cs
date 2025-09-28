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

        public Form1()
        {
            InitializeComponent();
            tasks = new List<Task>();
            InitializeCategories();
        }

        private void InitializeCategories()
        {
            comboBoxCategory.Items.AddRange(new string[] {
                "Работа", "Учеба", "Личное", "Здоровье", "Дом"
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
    }
}
