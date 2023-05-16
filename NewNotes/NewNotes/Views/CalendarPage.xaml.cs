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
            NotePlace.IsVisible = false;
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
            TitleNote.Text = "Заметка из календаря";
            SecretNote.Text = "false";


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
            note.Title = TitleNote.Text;
            note.SecretNote= Convert.ToBoolean(SecretNote.Text);
            if (!string.IsNullOrWhiteSpace(note.Text))
            {
                await App.NotesDB.SaveNoteAsync(note);
            }
            await Shell.Current.GoToAsync("..");
            await DisplayAlert("Уведомление", "Заметка сохранена", "OK");
            NotePlace.IsVisible = false;
            IdNote.Text = "";
            InputNote.Text = "";
            TitleNote.Text = "";
            SecretNote.Text = "";
            OnAppearing();

        }
        private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (e.CurrentSelection != null)
            {
               
                Note note = (Note)e.CurrentSelection.FirstOrDefault();
                if (note.SecretNote == false) 
                {
                    if (note != null)
                    {
                        NotePlace.IsVisible = true;
                        IdNote.Text = note.ID.ToString();
                        InputNote.Text = note.Text;
                        TitleNote.Text = note.Title;
                        SecretNote.Text = note.SecretNote.ToString();
                        
                    }
                } else
                {
                    NotePlace.IsVisible = false;
                    try
                    {
                        int id = 1;
                        Password pass = await App.NotesDB.GetPasswordAsync(id);
                        string CheckPass = await DisplayPromptAsync("Это приватная заметка", "Введите код доступа", "Ок", "Отмена");
                        if (CheckPass == null || CheckPass != pass.Text)
                        {
                            await DisplayAlert("Неверный код", "Повторите попытку", "Ок");
                            NotePlace.IsVisible = false;
                            OnAppearing();

                        }
                        else if (CheckPass == pass.Text)
                        {
                            NotePlace.IsVisible = true;
                            IdNote.Text = note.ID.ToString();
                            InputNote.Text = note.Text;
                            TitleNote.Text = note.Title;
                            SecretNote.Text = note.SecretNote.ToString();
                        }
                    }
                    catch(NullReferenceException) 
                    {
                        await DisplayAlert("Вы не задали код","Нет доступа к закрытой заметке. Чтобы установить код перейдите на страницу Инфо","Ок");
                        OnAppearing();
                    }
                    
                    
                }
              
            }

        }

        private async void DeleteButton_Clicked(Object sender, EventArgs e)
        {
            Note note = (Note)BindingContext;

            note.ID = Convert.ToInt32(IdNote.Text);

            await App.NotesDB.DeleteNoteAsync(note);

            NotePlace.IsVisible = false;
            IdNote.Text ="";
            InputNote.Text = "";
            TitleNote.Text = "";
            SecretNote.Text = "";

            await Shell.Current.GoToAsync("..");

            await DisplayAlert("Уведомление", "Заметка удалена", "OK");
            OnAppearing();
        }
    }
}