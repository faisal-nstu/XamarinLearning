using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;
using Xamarin.Forms;

namespace CityMimicXMApp.Controls
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
                AccountsTabH.IsVisible = true;
            }
            else if (TabNo == 2)
            {
                TransfersTabH.IsVisible = true;
            }
            else if (TabNo == 3)
            {
                PaymentsTabH.IsVisible = true;
            }
            else if (TabNo == 4)
            {
                MoreTabH.IsVisible = true;
            }
        }

        public TabControl()
        {
            InitializeComponent();
            
            AccountsTab.GestureRecognizers.Add(new TapGestureRecognizer() { Command = new Command(o => TappedCallback(1)) });
            TransfersTab.GestureRecognizers.Add(new TapGestureRecognizer() { Command = new Command(o => TappedCallback(2)) });
            PaymentsTab.GestureRecognizers.Add(new TapGestureRecognizer() { Command = new Command(o => TappedCallback(3)) });
            MoreTab.GestureRecognizers.Add(new TapGestureRecognizer() { Command = new Command(o => TappedCallback(4)) });
        }



        private void TappedCallback(object tabNoObj)
        {
            int tabNo = (int) tabNoObj;
            if (tabNo == TabNo) return;

            string tabName = string.Empty;

            if (tabNo == 1)
            {
                tabName = "AccountPage";
            }
            else if (tabNo == 2)
            {
                //tabName = "TransfersPage";
            }
            else if (tabNo == 3)
            {
                tabName = "PaymentPage";
            }
            else if (tabNo == 4)
            {
                //tabName = "MorePage";
            }
            if(!string.IsNullOrEmpty(tabName)) TabtappedCommand.Execute(tabName);
        }
    }
}
