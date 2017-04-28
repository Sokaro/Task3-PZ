using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using UWPLibrary;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Шаблон элемента пустой страницы задокументирован по адресу http://go.microsoft.com/fwlink/?LinkId=234238

namespace GitHubClient
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class BlankPage2 : Page
    {
        public BlankPage2()
        {
            this.InitializeComponent();
            textBox.IsReadOnly = true;
        }

        ReposHelper a = new ReposHelper();
        //StreamSocket socket;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            a = (ReposHelper)e.Parameter;
            textBlock.Text = a.reposname;
            textBlock1.Text = a.username;

            execute();

        }

        public async void execute()
        {
            await connect();
        }

        public async Task connect()
        {
            //
            SocketClient mysupersocket = new SocketClient();
            RepositoryNameUserNameModel model = new RepositoryNameUserNameModel { RepositoryName = a.reposname, UserName = a.username };
            RepositoryDescriptionModel description = await mysupersocket.GetDescription(model);
            //

          

            textBox.Text = description.Description;

        }


        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(BlankPage1));
        }


        private void button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(BlankPage3), a);
        }
    }
}
