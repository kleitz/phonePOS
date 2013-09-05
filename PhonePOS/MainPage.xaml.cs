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

namespace PhonePOS
{
    public partial class MainPage : PhoneApplicationPage
    {
        PhoneNumberChooserTask selNumber;
        
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            selNumber = new PhoneNumberChooserTask();
            selNumber.Completed += selNumber_Completed;
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
            selNumber.Show();
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