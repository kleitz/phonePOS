using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using PhonePOS.Resources;
using Microsoft.Phone.Tasks;
using System.Windows.Media.Imaging;
using ZXing;
using SQLite;
using PhonePOS.Entities;
using System.Text;

namespace PhonePOS
{
    public partial class MainPage : PhoneApplicationPage
    {
        PhoneNumberChooserTask selNumber;
        SQLiteConnection dbConn;
        
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            selNumber = new PhoneNumberChooserTask();
            selNumber.Completed += selNumber_Completed;

            dbConn = new SQLiteConnection("posconn");
            dbConn.CreateTable<Product>();
            // Código de ejemplo para traducir ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        public void obtenerNumeros()
        {
            
        }

        void selNumber_Completed(object sender, PhoneNumberResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                imgQRCode.Source = GenerateQRCode(e.PhoneNumber);
            }
            
        }

        private static WriteableBitmap GenerateQRCode(string phoneNumber)
        {
            BarcodeWriter _writer = new BarcodeWriter();

            _writer.Renderer = new ZXing.Rendering.WriteableBitmapRenderer()
            {
                Foreground = System.Windows.Media.Color.FromArgb(255, 0, 0, 255) // blue
            };

            _writer.Format = BarcodeFormat.QR_CODE;


            _writer.Options.Height = 400;
            _writer.Options.Width = 400;
            _writer.Options.Margin = 1;

            var barcodeImage = _writer.Write("tel:" + phoneNumber); //tel: prefix for phone numbers

            return barcodeImage;
        }

        private void btnScanNumber_Click(object sender, RoutedEventArgs e)
        {                  
            var newprod = dbConn.Insert(new Product() 
                                        { 
                                        ProductDescription =  "Camisa Dockers"
                                        });
            var newprod1 = dbConn.Insert(new Product()
            {
                ProductDescription = "Pantalón Levis"
            });

            var newprod2 = dbConn.Insert(new Product()
            {
                ProductDescription = "Cinturón"
            });

            string q2 = "UPDATE Product SET ProductDescription = 'Mantecadas' WHERE ProductDescription = ?";


            txtProducts.Text = GetAllProducts();

            dbConn.Execute(q2, "Cinturón");

            txtProducts.Text = txtProducts.Text + GetAllProducts();
            
            selNumber.Show();
        }

        private string GetAllProducts()
        {
            string query = "SELECT * FROM Product WHERE Id > ?";
            int i = 0;
            var products = dbConn.Query<Product>(query, i);

            StringBuilder sb = new StringBuilder("PRODUCTOS: ");
            sb.AppendLine(products.Count.ToString());
            foreach (var p in products)
            {
                sb.Append(p.Id);
                sb.Append("\t");
                sb.AppendLine(p.ProductDescription);
            }

            return sb.ToString();
        }


        private void btnScanBarcode_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/PageScancodes.xaml", UriKind.Relative));
        }

       

        // Código de ejemplo para compilar una ApplicationBar traducida
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Establecer ApplicationBar de la página en una nueva instancia de ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Crear un nuevo botón y establecer el valor de texto en la cadena traducida de AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Crear un nuevo elemento de menú con la cadena traducida de AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}