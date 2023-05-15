using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NewNotes.Models;
using Xamarin.CommunityToolkit.Extensions;
using System.Xml.Xsl;
using System.Security.Cryptography;
using XCalendar.Core.Enums;

namespace NewNotes.Views
{
   
    public partial class NotesPage : ContentPage
    {
       
        public NotesPage()
        {
            InitializeComponent();

        }

        protected override async void OnAppearing()
        {
            collectionView.ItemsSource = await App.NotesDB.GetNotesAsync();

            base.OnAppearing();
        }

        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(NoteAddingPage));
        }


        //вывод заметки в NoteAddingPage
        private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(e.CurrentSelection != null)
            {
                Note note = (Note)e.CurrentSelection.FirstOrDefault();
                if(note.SecretNote==false) 
                { 
                    await Shell.Current.GoToAsync(
                    $"{nameof(NoteAddingPage)}?{nameof(NoteAddingPage.ItemId)}={note.ID.ToString()}");
                } else
                {
                    int id = 1;
                    Password pass = await App.NotesDB.GetPasswordAsync(id);
                    string CheckPass = await DisplayPromptAsync("Это приватная заметка", "Введите код доступа", "Ок", "Отмена");
                    if (CheckPass == null || CheckPass != pass.Text)
                    {   
                        await DisplayAlert("Неверный код", "Повторите попытку", "Ок");
                        OnAppearing();
                        
                        
                    } else
                    {
                    await Shell.Current.GoToAsync(
                    $"{nameof(NoteAddingPage)}?{nameof(NoteAddingPage.ItemId)}={note.ID.ToString()}");
                    }
                   

                }
                
            } 
        }
    }
}