using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace Version01
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class AddMessagesWindow : Window
    {

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private ObservableCollection<string> messages;

        // For MainWindow (creating new person)
        public AddMessagesWindow(ObservableCollection<string> messages)
        {
            InitializeComponent();
            this.messages = messages;
        }

        // For EditPersonWindow (existing person)
        public AddMessagesWindow(Person person)
        {
            InitializeComponent();
            this.messages = person.Messages;
        }

        private void btnAddMessage_Click(object sender, RoutedEventArgs e)
        {
            NotificationService notificationService = new NotificationService(this);

            messages.Add(txtbxNewMessage.Text);
            notificationService.ShowSuccess(txtbxNewMessage.Text + " - added");
            txtbxNewMessage.Clear();

        }
        
    }
}
