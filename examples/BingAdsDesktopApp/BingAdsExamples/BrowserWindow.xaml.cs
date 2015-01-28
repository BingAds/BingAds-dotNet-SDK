// Copyright 2014 Microsoft Corporation 

// Licensed under the Apache License, Version 2.0 (the "License"); 
// you may not use this file except in compliance with the License. 
// You may obtain a copy of the License at 

//    http://www.apache.org/licenses/LICENSE-2.0 

// Unless required by applicable law or agreed to in writing, software 
// distributed under the License is distributed on an "AS IS" BASIS, 
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
// See the License for the specific language governing permissions and 
// limitations under the License. 

using System;
using System.Threading.Tasks;
using System.Windows;

namespace BingAdsExamples
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
