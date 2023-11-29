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
    /// Логика взаимодействия для AddPurchaseWindow.xaml
    /// </summary>
    public partial class AddPurchaseWindow : Window
    {        
        public DataRow purchaseRow;
        public AddPurchaseWindow()
        {
            InitializeComponent();
            
            
        }

        private void btnBuyOK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
