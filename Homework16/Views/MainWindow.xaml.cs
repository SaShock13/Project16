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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Homework16


    
{
    #region TODO
    //todo: Разработайте приложение, в котором будет подключено два разных источника данных: MSSQLLocalDB и MS Access.
    //todo: Организуйте подключение, выведите строку подключения и статус подключения к этим источникам данных.
    //todo: Вывод данных выполните в графическом интерфейсе.Также необходимо учесть, что должна быть авторизация по логину и паролю для источника данных.

    //todo:    Расширим программу из задания 1. Предположим, что в разных источниках данных храниться информация из
    //двух систем, информацию из них необходимо сверять между собой, чтобы находить и выводить недостающую.Создайте и
    //заполните таблицы произвольными данными для решения задачи.Первый источник данных должен содержать таблицу с
    //полями:
    //ID
    //Фамилия
    //Имя
    //Отчество
    //Номер телефона (может быть пустым)
    //Email.
    //Второй источник данных содержит таблицу со следующими полями:
    //ID
    //Email
    //Код товара
    //Наименование товара
    //У нас две разные системы, которые хранят разную информацию по клиентам.Одно из полей должно быть
    //идентификатором.В нашем случае — это поле e-mail.

    //todo: Расширим программу из задания 2. Создайте запросы SQL:
    //Select — для выборки данных о покупках по клиенту.
    //Insert — вставка во вторую таблицу по совершенной покупке, а также добавление новых клиентов в первую таблицу.
    //Update — обновление данных по клиенту из первой таблицы.
    //Delete — очистка таблиц.
    //После чего, используя запросы SQL и компоненты WPF, разработайте программу для сотрудников магазина.
    #endregion

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {    
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
            
        }

       
    }

}
