using ReactiveUI;
using System.Windows;
using Splat;

namespace Sample_WPF_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IViewFor<ViewModelClass>
    {
        public MainWindow()
        {
            Locator.CurrentMutable.Register(() => new PetNameView(), typeof(IViewFor<PetName>));
            Locator.CurrentMutable.Register(() => new PersonNameView(), typeof(IViewFor<PersonName>));

            InitializeComponent();

            ViewModel = new ViewModelClass();

            this.WhenActivated(d =>
            {
                d(this.Bind(ViewModel, vm => vm.FirstName, v => v.txtFirstName.Text));
                d(this.Bind(ViewModel, vm => vm.LastName, v => v.txtLastName.Text));
                d(this.OneWayBind(ViewModel, vm => vm.FullName, v => v.txtFullName.Text));
                d(this.OneWayBind(ViewModel, vm => vm.NameList, v => v.listNames.ItemsSource));

                d(this.BindCommand(ViewModel, vm => vm.AddName, v => v.btnAdd));
                d(this.BindCommand(ViewModel, vm => vm.AddNameAsync, v => v.btnAddAAsync));
                d(this.BindCommand(ViewModel, vm => vm.AddNameAsyncCancel, v => v.btnCancel));

                d(this.WhenAnyObservable(x => x.ViewModel.AddNameAsync.IsExecuting)
                    .BindTo(commandProgressBar, pb => pb.IsIndeterminate));

            });

        }

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register("ViewModel", typeof(ViewModelClass), typeof(MainWindow));

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (ViewModelClass)value; }
        }

        public ViewModelClass ViewModel
        {
            get { return (ViewModelClass)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }
    }
}
