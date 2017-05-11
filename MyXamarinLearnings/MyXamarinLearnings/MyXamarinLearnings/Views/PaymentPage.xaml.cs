using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace MyXamarinLearnings.Views
{
    public partial class PaymentPage : ContentPage
    {
        private int stackCount = 0;
        List<string> types = new List<string>() { "Travel", "Food", "Retail", "Favourite", "Teach", "Emergency" };
        public PaymentPage()
        {
            InitializeComponent();
        }

        private void AddClicked(object sender, EventArgs e)
        {
            var newGrid = new Grid()
            {
                ColumnDefinitions = new ColumnDefinitionCollection()
                {
                    new ColumnDefinition() { Width = new GridLength(3,GridUnitType.Star)},
                    new ColumnDefinition() { Width = new GridLength(1,GridUnitType.Star)},
                    new ColumnDefinition() { Width = new GridLength(1,GridUnitType.Star)}
                },
                Margin = new Thickness(10,0)
            };
            Picker typePicker = new Picker();
            typePicker.StyleId = "TPicker_" + stackCount;
            typePicker.Title = "Select Type";
            
            foreach (var type in types)
            {
                typePicker.Items.Add(type);
            }
            Grid.SetColumn(typePicker, 0);
            newGrid.Children.Add(typePicker);

            Entry qEntry = new Entry();
            qEntry.StyleId = "QEntry_"+stackCount;
            Grid.SetColumn(qEntry,1);
            newGrid.Children.Add(qEntry);

            Entry rmEntry = new Entry();
            rmEntry.StyleId = "RmEntry_" + stackCount;
            Grid.SetColumn(rmEntry, 2);
            newGrid.Children.Add(rmEntry);

            ServiceStack.Children.Add(newGrid);
            stackCount++;
        }

        private void SaveClicked(object sender, EventArgs e)
        {
            int i = 0;
            foreach (View rowView in ServiceStack.Children)
            {
                var rowGrid = (Grid) rowView;
                foreach (View entryView in rowGrid.Children)
                {
                    string qId = "QEntry_" + i;
                    string rmId = "RmEntry_" + i;
                    string tId = "TPicker_" + i;
                    if (entryView.StyleId == qId)
                    {
                        var qentry = (Entry)entryView;
                        Debug.WriteLine("Q" + i + ": " + qentry.Text);
                    }
                    if (entryView.StyleId == rmId)
                    {
                        var rentry = (Entry)entryView;
                        Debug.WriteLine("RM" + i + ": " + rentry.Text);
                    }
                    if (entryView.StyleId == tId)
                    {
                        var tentry = (Picker)entryView;
                        Debug.WriteLine("Type" + i + ": " + types[tentry.SelectedIndex]);
                    }
                }
                i++;
            }
        }
    }
}
