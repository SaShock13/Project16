using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;

namespace Homework16
{
    /// <summary>
    /// Логика взаимодействия для UpdateCustomerWindow.xaml
    /// </summary>
    public partial class UpdateCustomerWindow : Window
    {
        
        public UpdateCustomerWindow()
        {
            InitializeComponent();
        }
        private void btnOk(object sender, RoutedEventArgs e)
        {
            if (tbEmail != null & tbLastName != null & tbPhone != null)
            {
                DialogResult = true;
            }

        }
    }
}
