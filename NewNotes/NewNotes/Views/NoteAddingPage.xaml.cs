using NewNotes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NewNotes.Views
{

    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public partial class NoteAddingPage : ContentPage
    {
        public string ItemId
        {
            set
            {
                LoadNote(value);
            }
        }

        public NoteAddingPage()
        {
            InitializeComponent();

            BindingContext = new Note();
            
        }


        private async void LoadNote(string value)
        {
            try
            {
                int id = Convert.ToInt32(value);

                Note note = await App.NotesDB.GetNoteAsync(id);

                BindingContext = note;
                 if (note.SecretNote == true)
                {
                    secret.IsChecked= true;
                }
            }
            catch { }

        }


        private async void OnSaveButton_Clicked(object sender, EventArgs e)
        {
            Note note = (Note)BindingContext;

            note.Date = DateTime.UtcNow;

            note.SecretNote = secret.IsChecked;
            
            if (!string.IsNullOrWhiteSpace(note.Text))
            {
                await App.NotesDB.SaveNoteAsync(note);
            }

            await Shell.Current.GoToAsync("..");
        }

        private async void OnDeleteButton_Clicked(Object sender, EventArgs e)
        {
            Note note = (Note)BindingContext;

            await App.NotesDB.DeleteNoteAsync(note);

            await Shell.Current.GoToAsync("..");

            await DisplayAlert("Уведомление", "Заметка удалена", "OK");
        }
    }
}