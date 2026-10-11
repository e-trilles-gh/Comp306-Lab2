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

/*
 * Eskim Trilles - 301434046
 * COMP306 - API Engineering & Cloud Computing - Sec402
 * Lab2
 * October 11, 2026
 */

namespace _301434046_eskim__Lab2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override async void OnStartup(StartupEventArgs e)
        {
            //WPF setup
            base.OnStartup(e);

            //instantiates the class that stores the correct view to display
            NavigationStore navigationStore = new NavigationStore();

            //instantiates the class that manages the authentication
            AuthenticationService authenticationService = new AuthenticationService();

            //identifies the correct location of AWS credentials
            AWSConfigs.AWSProfileName = "wpf-app";

            //instantiates the amazon pdf storage
            AmazonS3PdfService amazonS3PdfService = new AmazonS3PdfService();
            
            //instatiates the amazon NoSQL database with specified region
            IAmazonDynamoDB dynamoDBClient = new AmazonDynamoDBClient(RegionEndpoint.USEast1);

            //instantiates the class that manages the connection between the app and AWS
            BookService bookService = new BookService(dynamoDBClient);

            //instantiates the LoginViewModel to set the LoginView as the current view
            navigationStore.CurrentViewModel
                = new LoginViewModel(
                    navigationStore,
                    authenticationService,
                    amazonS3PdfService,
                    bookService);

            //prepares the main window with the needed DataContext
            MainWindow = new MainWindow()
            {
                DataContext = new MainWindowViewModel(navigationStore)
            };
            MainWindow.Show();
        }
    }
}
