using System;
using System.Threading.Tasks;
using System.Windows;

namespace BingAdsWpfApp
{
    /// <summary>
    /// Interaction logic for BrowserWindow.xaml
    /// </summary>
    public partial class BrowserWindow : Window
    {
        public BrowserWindow(Uri navigateUri, string expectedRedirectUriPath)
        {
            InitializeComponent();

            _navigateUri = navigateUri;
            _expectedRedirectUriPath = expectedRedirectUriPath;
            _redirectTaskCompletionSource = new TaskCompletionSource<Uri>();

            Browser.Navigated += Browser_Navigated;
        }

        void Browser_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            if (e.Uri.AbsolutePath == _expectedRedirectUriPath)
            {
                _redirectTaskCompletionSource.SetResult(e.Uri);

                Close();
            }
        }

        private readonly Uri _navigateUri;

        private readonly string _expectedRedirectUriPath;

        private readonly TaskCompletionSource<Uri> _redirectTaskCompletionSource;

        public Task<Uri> GetRedirectUri()
        {
            Browser.Navigate(_navigateUri);

            return _redirectTaskCompletionSource.Task;
        }
    }
}

