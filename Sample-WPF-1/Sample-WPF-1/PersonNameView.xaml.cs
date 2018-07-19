using System.Windows;
using System.Windows.Controls;
using ReactiveUI;

namespace Sample_WPF_1
{
    /// <summary>
    /// Interaction logic for PersonNameView.xaml
    /// </summary>
    public partial class PersonNameView : IViewFor<PersonName>
    {
        public PersonNameView()
        {
            InitializeComponent();
        }

        #region ViewModel property boilerplate


        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (PersonName)value; }
        }

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register("ViewModel", typeof(PersonName), typeof(PersonNameView));

        public PersonName ViewModel
        {
            get { return (PersonName)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        #endregion
    }
}
