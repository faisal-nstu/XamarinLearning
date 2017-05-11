using MyXamarinLearnings.Models;
using Xamarin.Forms;

namespace MyXamarinLearnings.Views
{
    public partial class Page2 : ContentPage
    {
        public Page2()
        {
            InitializeComponent();
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var prediction = e.SelectedItem as Prediction;
            if (prediction != null) LocationEntry.Text = prediction.description;
        }
    }
}
