using System.Windows;
using PresentationViewModel;

namespace PresentationView
{
    public partial class MainWindow : Window
    {
        private BallPresentationVM viewModel = new();

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = viewModel;
            int howManyBalls = 15;
            viewModel.Generate(howManyBalls);
        }
    }
}