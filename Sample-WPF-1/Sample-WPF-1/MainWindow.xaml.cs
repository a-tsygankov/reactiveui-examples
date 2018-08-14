using ReactiveUI;
using System.Windows;

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

            //ViewModel = new ViewModelClass(); 
            DataContext = new ViewModelClass(); 


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
