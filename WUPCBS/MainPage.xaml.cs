//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Net.Http;
//using System.Runtime.InteropServices.WindowsRuntime;
//using Windows.Foundation;
//using Windows.Foundation.Collections;
//using Windows.UI.Xaml;
//using Windows.UI.Xaml.Controls;
//using Windows.UI.Xaml.Controls.Primitives;
//using Windows.UI.Xaml.Data;
//using Windows.UI.Xaml.Input;
//using Windows.UI.Xaml.Media;
//using Windows.UI.Xaml.Navigation;
//using Newtonsoft.Json;
//using WUPCBS.Models;
//using WUPCBS.Controller;
//using System.Collections;
//using System.Text;
//using System.Net;
//using System.Text.Json;
//using System.Text.Json.Serialization;
//using WUPCBS.Class;
//using Microsoft.Toolkit.Uwp;


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WUPCBS.Models;
using WUPCBS.Controller;
using WUPCBS.Class;
using Windows.UI.Popups;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

namespace WUPCBS
{
    /// <summary>
    /// Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public List<Msgjs> Msgjs;
    
        public MainPage()
        {
            this.InitializeComponent();
            GetMsgList();
     
        }
        /// <summary>
        /// Function to get the message sended from a web service
        /// </summary>
        private async void GetMsgList()
        {
            var Obj = new ObjMsg(); // Declare the class that contain functions
            Msgjs = await Obj.GetData(); //Asign the result to a object to binding the gridview
            sortMode(); // set order mode to the list of sended message
        }
        /// <summary>
        /// local void to prepare the class and send it to the post method to the web service 
        /// </summary>
        /// <param name="msg"></param>
        private async void SavingMsg(MsgDTO msg)
        {
            MessageDialog msgdialog = new MessageDialog("", ""); // Creating the message dialog to send the response of the webservice
            var Obj = new ObjMsg();
            var rst = new Resultado();// Declare object to recive the response of  the webservice
            rst = await Obj.SaveSendedmesage(msg);// Reciving the response of the result of the webserice
            if (rst.Codigo == 1)
            {
              ClearField();
              GetMsgList();
            }
            else
            {
                msgdialog.Content = "The message can't be sended";
                msgdialog.Title = "Something was wrong";
                await msgdialog.ShowAsync();
            }
        }
        private void ClearField()// Function to clear the last message infor
        {
            txtMessage.Text = "";
            txtTO.Text = "";
        }
        private async  void Button_Click(object sender, RoutedEventArgs e) // Button to validate de entry fields, if there are ok prepare a class with the info the message and the to's numbers
        {
            string messagetitle = "Missing Data";
            MessageDialog msgdialog = new MessageDialog("", messagetitle);
            if (txtTO.Text == "") // validate no empty numbers
            {
                msgdialog.Content = "You have to add a telephone Number"; 
                await msgdialog.ShowAsync();
            }
            else if (txtTO.Text == "")// validate no empty message
            {
                msgdialog.Content = "You have to type a message text";
                await msgdialog.ShowAsync();
            }
            else
            {
                MsgDTO msg = new MsgDTO(); // Prepare the class msgDTO
                msg.tomsg = txtTO.Text;
                msg.messagetxt = txtMessage.Text;
                SavingMsg(msg); // Sending the class to the function to send and save the message
            }
         
        }

        private void txtTO_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtMessage_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            GetMsgList(); // Refresh the msg list
        }

       
        private void sortMode()// funtion to sort the gridview data
        {
            if (cbSortMode.SelectedItem == null)
            {
                Gv1.ItemsSource = Msgjs ;
            }
            else
            {
                Object selectedItem = cbSortMode.SelectedItem;
                if (selectedItem.ToString() == "By send date")
                {
                    var SortResult = Msgjs.OrderBy(a => a.crdate);
                    Gv1.ItemsSource = SortResult;
                }
                else if (selectedItem.ToString() == "By To")
                {
                    var SortResult = Msgjs.OrderBy(a => a.tomsg);
                    Gv1.ItemsSource = SortResult;
                }
                else
                {
                    var SortResult = Msgjs.OrderBy(a => a.messagetxt);
                    Gv1.ItemsSource = SortResult;
                }
            }
      
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)// when the sort mode change in the combobox sort the list of messages
        {
            sortMode();
        }
    }
}
