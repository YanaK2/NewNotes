using NewNotes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NewNotes.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalendarPage : ContentPage
    {
        protected override async void OnAppearing()
        {
            collectionView.ItemsSource = await App.NotesDB.GetNotesAsync();

            base.OnAppearing();
        }
        public string ItemId
        {
            set
            {
                LoadNote(value);
            }
        }
        public CalendarPage()
        {
            InitializeComponent();

            BindingContext = new Note();
        }

        private /*async*/ void CalendarView_DateSelectionChanged(object sender, XCalendar.Models.DateSelectionChangedEventArgs e)
        {
            // string Selectedate = calendar.SelectedDates.Single().Date.ToString();
            // DisplayAlert("Title", Selectedate, "OK");

            NotePlace.IsVisible = true;
            InputNote.Text="";
            
            
        }

        private async void LoadNote(string value)
        {
            try
            {
                int id = Convert.ToInt32(value);

                Note note = await App.NotesDB.GetNoteAsync(id);

                BindingContext = note;
            }
            catch { }
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            Note note = (Note)BindingContext;

            note.Date = calendar.SelectedDates.Single().Date;

            if (note.Date == null) 
            { 
                note.Date = DateTime.Now;
            } else note.Date = calendar.SelectedDates.Single().Date;

            if (!string.IsNullOrWhiteSpace(note.Text))
            {
                await App.NotesDB.SaveNoteAsync(note);
            }
            await Shell.Current.GoToAsync("..");
            await DisplayAlert("Уведомление", "Заметка сохранена", "OK");
            NotePlace.IsVisible = false;

        }
        private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                Note note = (Note)e.CurrentSelection.FirstOrDefault();
                await Shell.Current.GoToAsync(
                    $"{nameof(NoteAddingPage)}?{nameof(NoteAddingPage.ItemId)}={note.ID.ToString()}");
            }
        }
    }
}