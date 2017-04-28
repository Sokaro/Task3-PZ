using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UWPLibrary;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Input;
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
    public sealed partial class BlankPage1 : Page
    {
        int page = 0;
        int counthelper = 37;
        bool first = true;
        //StreamSocket socket;
        string reposhelperusername;

        public void collapse()
        {
            textBlock1.Visibility = Visibility.Collapsed;
            textBlock2.Visibility = Visibility.Collapsed;
            textBlock3.Visibility = Visibility.Collapsed;
            textBlock4.Visibility = Visibility.Collapsed;
            textBlock5.Visibility = Visibility.Collapsed;
            textBlock6.Visibility = Visibility.Collapsed;
            textBlock7.Visibility = Visibility.Collapsed;
            textBlock8.Visibility = Visibility.Collapsed;
            textBlock9.Visibility = Visibility.Collapsed;
            textBlock10.Visibility = Visibility.Collapsed;
            textBlock11.Visibility = Visibility.Collapsed;
            textBlock12.Visibility = Visibility.Collapsed;
            textBlock13.Visibility = Visibility.Collapsed;
            textBlock14.Visibility = Visibility.Collapsed;
            textBlock15.Visibility = Visibility.Collapsed;
            textBlock16.Visibility = Visibility.Collapsed;
            textBlock17.Visibility = Visibility.Collapsed;
            textBlock18.Visibility = Visibility.Collapsed;
            textBlock19.Visibility = Visibility.Collapsed;
            textBlock20.Visibility = Visibility.Collapsed;
        }

        public void discollapse()
        {
            textBlock1.Visibility = Visibility.Visible; textBlock1.Text = string.Empty;
            textBlock2.Visibility = Visibility.Visible; textBlock2.Text = string.Empty;
            textBlock3.Visibility = Visibility.Visible; textBlock3.Text = string.Empty;
            textBlock4.Visibility = Visibility.Visible; textBlock4.Text = string.Empty;
            textBlock5.Visibility = Visibility.Visible; textBlock5.Text = string.Empty;
            textBlock6.Visibility = Visibility.Visible; textBlock6.Text = string.Empty;
            textBlock7.Visibility = Visibility.Visible; textBlock7.Text = string.Empty;
            textBlock8.Visibility = Visibility.Visible; textBlock8.Text = string.Empty;
            textBlock9.Visibility = Visibility.Visible; textBlock9.Text = string.Empty;
            textBlock10.Visibility = Visibility.Visible; textBlock10.Text = string.Empty;
            textBlock11.Visibility = Visibility.Visible; textBlock11.Text = string.Empty;
            textBlock12.Visibility = Visibility.Visible; textBlock12.Text = string.Empty;
            textBlock13.Visibility = Visibility.Visible; textBlock13.Text = string.Empty;
            textBlock14.Visibility = Visibility.Visible; textBlock14.Text = string.Empty;
            textBlock15.Visibility = Visibility.Visible; textBlock15.Text = string.Empty;
            textBlock16.Visibility = Visibility.Visible; textBlock16.Text = string.Empty;
            textBlock17.Visibility = Visibility.Visible; textBlock17.Text = string.Empty;
            textBlock18.Visibility = Visibility.Visible; textBlock18.Text = string.Empty;
            textBlock19.Visibility = Visibility.Visible; textBlock19.Text = string.Empty;
            textBlock20.Visibility = Visibility.Visible; textBlock20.Text = string.Empty;
        }

        public BlankPage1()
        {
            this.InitializeComponent();
            textBlock.Text = (page + 1).ToString();
            collapse();
            discollapse();
        }

        Story shelper = new Story();
        List<int> reposids;
        List<string> reposnames;

        //
        List<RepositoryIdNameModel> zom;
            //

        int reposidcounthelper = 0;

        private async void button_Click(object sender, RoutedEventArgs e)
        {

            t1 = false;
            t2 = true;

            reposids = new List<int>();
            reposnames = new List<string>();
            reposidcounthelper = 0;

            string username = textBox.Text;
            reposhelperusername = username;
            string answer = string.Empty;
            discollapse();

            //
            SocketClient mysupersocket = new SocketClient();
            List<RepositoryIdNameModel> zom=
            await mysupersocket.GetUserReposytories(username);
            //

            for(int i=0;i<zom.Count;i++)
            {
                reposids.Add(zom[i].Id);
                reposnames.Add(zom[i].Name);
            }
           

            int k = 0;
            reposidcounthelper = 0;

            while (k < 10 && k < zom.Count)
            {
                loadd(k, zom[reposidcounthelper].Name, username);
                k++;
                reposidcounthelper++;
            }


        }

        bool t2 = false;
        bool t1 = false;

        public bool checker(string answer)
        {
            string zsk = answer.Substring(9, 3);
            if (zsk == "404")
            {
                return false;
            }
            if (zsk == "200")
            {
                return true;
            }
            return false;
        }
      

        
        private async void button2_Click(object sender, RoutedEventArgs e)
        {
            t2 = false;
            t1 = true;
            string answer = string.Empty;
            int k = 0;

            while (k < 10)
            {
                //
                SocketClient mysupersocket = new SocketClient();
                RepositoryNameUserNameModel zom = await mysupersocket.GetRepository(counthelper);
                //


                if (zom!=null)
                {
                    loadd(k, zom.RepositoryName, zom.UserName);

                    k++;
                }

                counthelper++;

            }

            button2.Visibility = Visibility.Collapsed;
        }

        public void loadd(int k, string str1, string ept)
        {
            switch (k)
            {
                case 0:
                    {
                        textBlock1.Text = str1;
                        textBlock11.Text = ept;
                        break;
                    }
                case 1:
                    {
                        textBlock2.Text = str1;
                        textBlock12.Text = ept;
                        break;
                    }
                case 2:
                    {
                        textBlock3.Text = str1;
                        textBlock13.Text = ept;
                        break;
                    }
                case 3:
                    {
                        textBlock4.Text = str1;
                        textBlock14.Text = ept;
                        break;
                    }
                case 4:
                    {
                        textBlock5.Text = str1;
                        textBlock15.Text = ept;
                        break;
                    }
                case 5:
                    {
                        textBlock6.Text = str1;
                        textBlock16.Text = ept;
                        break;
                    }
                case 6:
                    {
                        textBlock7.Text = str1;
                        textBlock17.Text = ept;
                        break;
                    }
                case 7:
                    {
                        textBlock8.Text = str1;
                        textBlock18.Text = ept;
                        break;
                    }
                case 8:
                    {
                        textBlock9.Text = str1;
                        textBlock19.Text = ept;
                        break;
                    }
                case 9:
                    {
                        textBlock10.Text = str1;
                        textBlock20.Text = ept;
                        break;
                    }
                default:
                    {
                        throw new ArgumentException("how did u make this not working...");
                        break;
                    }
            }
        }

        bool swiper = false;
        double x1, x2, y1, y2;

        private void textBlock1_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ReposHelper a = new ReposHelper();
            a.reposname = textBlock1.Text;
            a.username = textBlock11.Text;
            Frame.Navigate(typeof(BlankPage2), a);
        }

        private void textBlock2_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ReposHelper a = new ReposHelper();
            a.reposname = textBlock2.Text;
            a.username = textBlock12.Text;
            Frame.Navigate(typeof(BlankPage2), a);

        }

        private void textBlock3_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ReposHelper a = new ReposHelper();
            a.reposname = textBlock3.Text;
            a.username = textBlock13.Text;
            Frame.Navigate(typeof(BlankPage2), a);
        }

        private void textBlock4_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ReposHelper a = new ReposHelper();
            a.reposname = textBlock4.Text;
            a.username = textBlock14.Text;
            Frame.Navigate(typeof(BlankPage2), a);
        }

        private void textBlock5_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ReposHelper a = new ReposHelper();
            a.reposname = textBlock5.Text;
            a.username = textBlock15.Text;
            Frame.Navigate(typeof(BlankPage2), a);
        }

        private void textBlock6_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ReposHelper a = new ReposHelper();
            a.reposname = textBlock6.Text;
            a.username = textBlock16.Text;
            Frame.Navigate(typeof(BlankPage2), a);
        }

        private void textBlock7_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ReposHelper a = new ReposHelper();
            a.reposname = textBlock7.Text;
            a.username = textBlock17.Text;
            Frame.Navigate(typeof(BlankPage2), a);
        }

        private void textBlock8_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ReposHelper a = new ReposHelper();
            a.reposname = textBlock8.Text;
            a.username = textBlock18.Text;
            Frame.Navigate(typeof(BlankPage2), a);
        }

        private void textBlock9_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ReposHelper a = new ReposHelper();
            a.reposname = textBlock9.Text;
            a.username = textBlock19.Text;
            Frame.Navigate(typeof(BlankPage2), a);
        }

        private void textBlock10_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ReposHelper a = new ReposHelper();
            a.reposname = textBlock10.Text;
            a.username = textBlock20.Text;
            Frame.Navigate(typeof(BlankPage2), a);
        }

        private void textBlock12_Tapped(object sender, TappedRoutedEventArgs e)
        {
            textBox.Text = textBlock12.Text;
            button_Click(sender, e);
        }

        private void textBlock13_Tapped(object sender, TappedRoutedEventArgs e)
        {
            textBox.Text = textBlock13.Text;
            button_Click(sender, e);
        }

        private void textBlock14_Tapped(object sender, TappedRoutedEventArgs e)
        {
            textBox.Text = textBlock14.Text;
            button_Click(sender, e);
        }

        private void textBlock15_Tapped(object sender, TappedRoutedEventArgs e)
        {
            textBox.Text = textBlock15.Text;
            button_Click(sender, e);
        }

        private void textBlock16_Tapped(object sender, TappedRoutedEventArgs e)
        {
            textBox.Text = textBlock16.Text;
            button_Click(sender, e);
        }

        private void textBlock17_Tapped(object sender, TappedRoutedEventArgs e)
        {
            textBox.Text = textBlock17.Text;
            button_Click(sender, e);
        }

        private void textBlock18_Tapped(object sender, TappedRoutedEventArgs e)
        {
            textBox.Text = textBlock18.Text;
            button_Click(sender, e);
        }

        private void textBlock19_Tapped(object sender, TappedRoutedEventArgs e)
        {
            textBox.Text = textBlock19.Text;
            button_Click(sender, e);
        }

        private void textBlock20_Tapped(object sender, TappedRoutedEventArgs e)
        {
            textBox.Text = textBlock20.Text;
            button_Click(sender, e);
        }

        private void textBlock11_Tapped(object sender, TappedRoutedEventArgs e)
        {
            textBox.Text = textBlock11.Text;
            button_Click(sender, e);
        }



        //swiper
        private void Grid_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            swiper = true;
            PointerPoint point = e.GetCurrentPoint(WindowT2);
            x1 = point.Position.X;
            y1 = point.Position.Y;
        }

        private async void Grid_PointerReleased(object sender, PointerRoutedEventArgs e)
        {

            PointerPoint point = e.GetCurrentPoint(WindowT2);
            x2 = point.Position.X;
            y2 = point.Position.Y;


            if (swiper)
            {
                if (x2 - x1 > 50)
                {
                    if (t1)
                    {
                        discollapse();

                        string answer = string.Empty;
                        int k = 0;
                        bool flag = false;

                        while (k < 10)
                        {
                            //
                            SocketClient mysupersocket = new SocketClient();
                            RepositoryNameUserNameModel zom = await mysupersocket.GetRepository(counthelper);
                            //

                            

                            if (zom!=null)
                            {                              
                                loadd(k, zom.RepositoryName, zom.UserName);

                                flag = true;
                                k++;
                            }




                            counthelper++;
                        }



                        if (flag)
                        {//textBlock21.Text = "swiped right";
                            page++;
                            textBlock.Text = (page + 1).ToString();
                        }
                    }
                    if (t2)
                    {
                        bool flag = false;
                        //reposidcounthelper
                        discollapse();
                        int k = 0;

                        while (k < 10 && reposidcounthelper < reposids.Count)
                        {
                            loadd(k, reposnames[reposidcounthelper], reposhelperusername);


                            k++;
                            flag = true;
                            reposidcounthelper++;
                        }



                        if (flag)
                        {//textBlock21.Text = "swiped right";
                            page++;
                            textBlock.Text = (page + 1).ToString();
                        }
                    }
                }
                if (x2 - x1 < -50)
                {
                    if (t1)
                    {
                        discollapse();

                        string answer = string.Empty;
                        int k = 0;
                        bool flag = false;

                        while (k < 10 && counthelper > 0)
                        {
                            //
                            SocketClient mysupersocket = new SocketClient();
                            RepositoryNameUserNameModel zom = await mysupersocket.GetRepository(counthelper);
                            //

                            

                            if (zom!=null)
                            {
                                loadd(k, zom.RepositoryName, zom.UserName);

                                k++;
                                flag = true;
                            }




                            counthelper--;
                        }

                        //  textBlock21.Text = "swiped left";
                        if (page != 0 && flag)
                        {
                            page--;
                            textBlock.Text = (page + 1).ToString();
                        }
                    }
                    if (t2)
                    {
                        bool flag = false;
                        //reposidcounthelper
                        discollapse();
                        int k = 0;

                        while (k < 10 && reposidcounthelper > 0)
                        {

                            reposidcounthelper--;
                            loadd(k, reposnames[reposidcounthelper], reposhelperusername);
                            k++;
                            flag = true;

                        }


                        // textBlock21.Text = "swiped left";
                        if (page != 0 && flag)
                        {
                            page--;
                            textBlock.Text = (page + 1).ToString();
                        }
                    }
                }

                swiper = false;
            }

        }
    }
}
