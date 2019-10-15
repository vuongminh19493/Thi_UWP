using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Thi_UWP.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ShowNotes : Page
    {
        ObservableCollection<string> noteItems = new ObservableCollection<string>();
        IReadOnlyList<StorageFile> files = new List<StorageFile>();
        public ShowNotes()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            GetNote();
            noteList.ItemsSource = noteItems;
        }

        private void GetNote()
        {
            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            files = storageFolder.GetFilesAsync().GetAwaiter().GetResult();
            foreach (var file in files)
            {
                noteItems.Add(file.Name);
            }
        }

        private void CreateNote_OnClick(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CreateNote));
        }

        private void UpdateButton_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedIndex = noteList.SelectedIndex;
            var fileName = noteItems[selectedIndex];
            foreach (var file in files)
            {
                if (file.Name == fileName)
                {
                    Windows.Storage.FileIO.WriteTextAsync(file,showNote.Text).GetAwaiter().GetResult();
                }
            }
        }

        private void NoteList_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            var selectedIndex = noteList.SelectedIndex;
            var fileName = noteItems[selectedIndex];
            foreach (var file in files)
            {
                if (file.Name == fileName)
                {
                    var content = Windows.Storage.FileIO.ReadTextAsync(file).GetAwaiter().GetResult();
                    showNote.Text = content;
                }
            }
        }
    }
}
