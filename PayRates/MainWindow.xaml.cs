/*
Group-4
    Kurian, Eldho,
    Mittal, Tanya,
    Pou, Aileen,
    Yu, Katey,
 */
using System.Windows;

namespace PayRates
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        VM vm = new VM();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = vm;
        }
        private void HighestPayRate_Click(object sender, RoutedEventArgs e)
        {
            string max = string.Concat("Highest Salary: $", Db.GetInstance().MaximumQuery().ToString("0.00"));
            Result rw = new Result(max);
            rw.ShowDialog();
        }
        private void LowestPayRate_Click(object sender, RoutedEventArgs e)
        {
            string min = string.Concat("Lowest Salary: $", Db.GetInstance().MinimumQuery().ToString("0.00"));
            Result rw = new Result(min);
            rw.ShowDialog();
        }
    }
}
