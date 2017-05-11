using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;
using Xamarin.Forms;

namespace MyXamarinLearnings.Controls
{
    public partial class TabControl : ContentView
    {

        public ICommand TabtappedCommand
        {
            get { return ( ICommand)base.GetValue(TabtappedCommandProperty); }
            set { base.SetValue(TabtappedCommandProperty, value); }
        }
        public static BindableProperty TabtappedCommandProperty = BindableProperty.Create(
                                                            propertyName: "TabtappedCommand",
                                                            returnType: typeof( ICommand),
                                                            declaringType: typeof(TabControl),
                                                            defaultValue: null);



        public int TabNo
        {
            get { return (int)base.GetValue(TabNoProperty); }
            set { base.SetValue(TabNoProperty, value); }
        }
        public static BindableProperty TabNoProperty = BindableProperty.Create(
                                                            propertyName: "TabNo",
                                                            returnType: typeof(int),
                                                            declaringType: typeof(TabControl),
                                                            defaultValue: 0,
                                                            defaultBindingMode: BindingMode.TwoWay,
                                                            propertyChanged:
                                                                (o, value, newValue) =>
                                                                    HandleButtonPropertyChanged(o, value, newValue));

        private static void HandleButtonPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var b = bindable as TabControl;
            b?.SetTab();
        }


        
        private void SetTab()
        {
            if (TabNo == 1)
            {
                Tab1H.IsVisible = true;
            }
            else if (TabNo == 2)
            {
                Tab2H.IsVisible = true;
            }
            else if (TabNo == 3)
            {
                Tab3H.IsVisible = true;
            }
            else if (TabNo == 4)
            {
                Tab4H.IsVisible = true;
            }
        }

        public TabControl()
        {
            InitializeComponent();
            
            Tab1.GestureRecognizers.Add(new TapGestureRecognizer() { Command = new Command(o => TappedCallback(1)) });
            Tab2.GestureRecognizers.Add(new TapGestureRecognizer() { Command = new Command(o => TappedCallback(2)) });
            Tab3.GestureRecognizers.Add(new TapGestureRecognizer() { Command = new Command(o => TappedCallback(3)) });
            Tab4.GestureRecognizers.Add(new TapGestureRecognizer() { Command = new Command(o => TappedCallback(4)) });
        }



        private void TappedCallback(object tabNoObj)
        {
            int tabNo = (int) tabNoObj;
            if (tabNo == TabNo) return;

            string tabName = string.Empty;

            if (tabNo == 1)
            {
                tabName = "Page1";
            }
            else if (tabNo == 2)
            {
                tabName = "Page2";
                
            }
            else if (tabNo == 3)
            {
                tabName = "Page3";
            }
            else if (tabNo == 4)
            {
                tabName = "Page4";
            }
            if(!string.IsNullOrEmpty(tabName)) TabtappedCommand.Execute(tabName);
        }
    }
}
