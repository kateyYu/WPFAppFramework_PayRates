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
    /// Interaction logic for Result.xaml
    /// </summary>
    public partial class Result : Window
    {
        public Result(string resultvalue)
        {
            InitializeComponent();
            ResultLabel.Content = resultvalue;
        }
    }
}
