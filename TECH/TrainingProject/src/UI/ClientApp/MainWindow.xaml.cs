using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ClientApp.Model.DataSuppliers;
using ClientApp.Other;
using ClientApp.ViewModel;

namespace ClientApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        void Window1_Closing(object sender, CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        void SmallImage_OnMouseDown(object sender, MouseEventArgs e)
        {
            ProductIcon.Source = (sender as Image)?.Source;
        }

        void MainListItem_OnMouseDown(object sender, MouseEventArgs e)
        {
            var panel = sender as StackPanel;
            if (panel != null && panel.Children.Count > 0)
            {
                ProductIcon.Source = (panel.Children[0] as Image)?.Source;
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            DataContext = new ApplicationViewModel(new ProductSupplier(new ProductServiceProxy()));

            // data context for test purposes
            //DataContext = new ApplicationViewModel(new ProductSupplierMock());

            this.Closing += new CancelEventHandler(Window1_Closing);

            Logger.Info("Client application started");
        }
    }
}
