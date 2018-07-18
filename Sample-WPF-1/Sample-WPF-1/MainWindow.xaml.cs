using System;
using System.Collections.Generic;
using System.Linq;
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
using ReactiveUI;

namespace Sample_WPF_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IViewFor<ViewModelClass>
    {
        public MainWindow()
        {
            InitializeComponent();

            ViewModel = new ViewModelClass();

            this.WhenActivated(d =>
            {
                d(this.Bind(ViewModel, vm => vm.FirstName, v => v.txtFirstName.Text));
                d(this.Bind(ViewModel, vm => vm.LastName, v => v.txtLastName.Text));
                d(this.OneWayBind(ViewModel, vm => vm.FullName, v => v.txtFullName.Text));

            });

        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (ViewModelClass)value; }
        }

        //public static readonly DependencyProperty ViewModelProperty = 
        //    DependencyProperty.Register("ViewModel", typeof(ViewModelClass), typeof(MainWindow), new PropertyMetadata(default(ViewModelClass)));
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register("ViewModel", typeof(ViewModelClass), typeof(MainWindow));

        public ViewModelClass ViewModel
        {
            get { return (ViewModelClass)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }
    }
}
