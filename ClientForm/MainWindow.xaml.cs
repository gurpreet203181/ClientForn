using System;
using System.Collections.Generic;
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
using System.Net;
using System.Net.Sockets;

namespace ClientForm
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Socket client;
        public MainWindow()
        {
            InitializeComponent();
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        }

        private void btn_connetti_Click(object sender, RoutedEventArgs e)
        {

            IPAddress ipaddr = null;
            int nPort = 0;

            if (!IPAddress.TryParse(txt_ipServer.Text, out ipaddr))
            {
                //ip address sbagliato
                MessageBox.Show("Ip vuoto o non valido.", "Errore");
                //return;
            }
            else if (!int.TryParse(txt_porta.Text, out nPort))
            {
                MessageBox.Show("Porta vuota o non valida", "Errore");
            }
          
            else
            {

                client.Connect(ipaddr, nPort);
                btn_connetti.IsEnabled = false;
                lst_msg.Items.Add("Hello World");

            }


        }

        private void btn_invia_Click(object sender, RoutedEventArgs e)
        {
            string message = txt_msg.Text.Trim();
            byte[] buff = new byte[128];
            string message2;
            byte[] buff2 = new byte[128];

            buff = Encoding.ASCII.GetBytes(message);
            client.Send(buff);

            client.Receive(buff2);
            message2 = Encoding.ASCII.GetString(buff2);

           
            lst_msg.Items.Add(txt_msg.Text);

            txt_msg.Clear();
            lst_msg.Items.Add(message2);

        }

        private void txt_msg_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
