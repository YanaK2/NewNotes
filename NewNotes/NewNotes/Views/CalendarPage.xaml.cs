using NewNotes.Data;
using NewNotes.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NewNotes.Views
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]

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
             DateTime Selectedate = calendar.SelectedDates.Single().Date;
            
            // DisplayAlert("Title", Selectedate, "OK");

            NotePlace.IsVisible = true;
            InputNote.Text="";
            IdNote.Text = "0";


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

            
            try
            {
                 note.Date = calendar.SelectedDates.Single().Date;
            }
            catch (InvalidOperationException)
            {
                note.Date= DateTime.Now;
            }

            note.ID =Convert.ToInt32(IdNote.Text);
            if (!string.IsNullOrWhiteSpace(note.Text))
            {
                await App.NotesDB.SaveNoteAsync(note);
            }
            await Shell.Current.GoToAsync("..");
            await DisplayAlert("Уведомление", "Заметка сохранена", "OK");
            NotePlace.IsVisible = false;

        }
        private /*async */void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (e.CurrentSelection != null)
            {
                NotePlace.IsVisible = true;
                Note note = (Note)e.CurrentSelection.FirstOrDefault();
                
                if (note != null)
                {  
                    IdNote.Text = note.ID.ToString();
                    InputNote.Text = note.Text;

                }

                    /*await Shell.Current.GoToAsync(
                    //НЕ ВИДИТ ПУТЬ КАК ЭТО ПЕРЕДЕЛАТЬ АААААА
                    $"{nameof(CalendarPage)}?{nameof(CalendarPage.ItemId)}={note.ID.ToString()}",
                    InputNote.Text = note.Text
                    );*/
            }
        }

        private async void DeleteButton_Clicked(Object sender, EventArgs e)
        {
            Note note = (Note)BindingContext;

            note.ID = Convert.ToInt32(IdNote.Text);

            await App.NotesDB.DeleteNoteAsync(note);

            NotePlace.IsVisible = false;

            await Shell.Current.GoToAsync("..");

            await DisplayAlert("Уведомление", "Заметка удалена", "OK");
        }
    }
}