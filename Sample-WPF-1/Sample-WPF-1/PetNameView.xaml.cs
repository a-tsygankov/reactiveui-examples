using System.Windows;
using System.Windows.Controls;
using ReactiveUI;

namespace Sample_WPF_1
{
    /// <summary>
    /// Interaction logic for PetNameView.xaml
    /// </summary>
    public partial class PetNameView : IViewFor<PetName>
    {
        public PetNameView()
        {
            InitializeComponent();
            this.WhenActivated(d =>
            {
                d(this.Bind(ViewModel, vm => vm.Text, v => v.txtName.Text));
            });
        }

        #region ViewModel property boilerplate


        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (PetName)value; }
        }

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register("ViewModel", typeof(PetName), typeof(PetNameView));

        public PetName ViewModel
        {
            get { return (PetName)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        #endregion
    }
}
