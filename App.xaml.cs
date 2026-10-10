using _301434046_eskim__Lab2.Services;
using _301434046_eskim__Lab2.Stores;
using _301434046_eskim__Lab2.ViewModels;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.S3;
using Amazon.S3.Model;
using System.Configuration;
using System.Data;
using System.Windows;

namespace _301434046_eskim__Lab2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override async void OnStartup(StartupEventArgs e)
        {
            NavigationStore navigationStore = new NavigationStore();

            AuthenticationService authenticationService = new AuthenticationService();

            AWSConfigs.AWSProfileName = "wpf-app";

            AmazonS3PdfService amazonS3PdfService = new AmazonS3PdfService();
            
            IAmazonDynamoDB dynamoDBClient = new AmazonDynamoDBClient(RegionEndpoint.USEast1);

            BookService bookService = new BookService(dynamoDBClient);

            navigationStore.CurrentViewModel
                = new LoginViewModel(
                    navigationStore,
                    authenticationService,
                    amazonS3PdfService,
                    bookService);
            MainWindow = new MainWindow()
            {
                DataContext = new MainWindowViewModel(navigationStore)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}
