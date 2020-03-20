using System.Windows;
using PostSharp.Patterns.Model;
namespace WpfFrontend
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    [NotifyPropertyChanged]
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new QuizViewModel();

            textBox.Focus();
        }
    }
}
