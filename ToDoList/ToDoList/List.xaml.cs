using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace ToDoList
{
    public partial class List : PhoneApplicationPage
    {
        public List()
        {
            InitializeComponent();
        }

        private void addTaskButton_Click(object sender, RoutedEventArgs e)
        {
            if (toDoBox.Text != "")
            {
                toDoList.Items.Add(toDoBox.Text + " " + choseDate.ValueString + " " + choseTime.ValueString);
                toDoBox.Text = "";

            }
            else
            {
                MessageBox.Show("Please enter a text");
            }
        }

        private void toDoBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}