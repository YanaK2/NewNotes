using NewNotes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static NewNotes.Views.AboutPage;

namespace NewNotes.Views
{

    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            
            InitializeComponent();

        }

        private async void PasswordButton_Clicked(object sender, EventArgs e)
        {
            /* int id = 1;

             Password pass = await App.NotesDB.GetPasswordAsync(id);
             try
             {
                 Console.WriteLine(pass.Text);
             }
             catch (NullReferenceException)
             {
                 Password password = new Password();
                 password.Text = newpass.Text;
                 password.ID = 1;
                 await App.NotesDB.SavePasswordAsync(password);
                 Console.WriteLine(password.ID);
             }*/
            Password password = new Password();
            password.Text = newpass.Text;
            password.ID = 1;
            await App.NotesDB.SavePasswordAsync(password);
            Console.WriteLine(password.ID);
            /* if (pass.Text != null)
             {
                 Password password = new Password();
                 string password1 = await DisplayPromptAsync("Новый код доступа", "Задайте код, чтобы получить доступ к приватной заметке", "Сохранить", "Отмена");
                 password.Text = password1;
                 password.ID = 1;
                 await App.NotesDB.SavePasswordAsync(password);
                 Console.WriteLine(password.ID);
             } else
             {
                 Console.WriteLine(pass.Text);
                 passwordbutton.Text = "Изменить код";
                 string passwordRepeat = await DisplayPromptAsync("Изменение кода", "Для изменения кода, необходимо подтверждение", "Сохранить", "Отмена");
                 if (passwordRepeat == null || passwordRepeat != pass.Text)
                 {
                     await DisplayAlert("НЕВЕРНЫЙ КОД", "", "Ок");
                 }
                 else
                 {
                    string password2 = await DisplayPromptAsync("Новый код доступа", "Задайте код, чтобы полуить доступ к приватной заметке", "Сохранить", "Отмена");
                     pass.Text = password2;
                     pass.ID = 1;
                     await App.NotesDB.SavePasswordAsync(pass);
                     Console.WriteLine(pass.Text);
                 }
             }*/


            // passwordbutton.IsVisible = false;
            //  passview.Text = App.NotesDB.GetPasswordsAsync().ToString();

        }
        //Я БЛИЗКА К УТРАНОВЛЕНИЮ ПАРОЛЯ, МНЕ НАДО КАК-ТО СОХРАНИТЬ ТЕКСТ В БД
        private async void ChangePasswordButton_Clicked(object sender, EventArgs e)
        {
            int id = 1;
           
              Password pass = await App.NotesDB.GetPasswordAsync(id);
              try
              {
                  passview.Text = pass.Text;
              } 
              catch (NullReferenceException)
              {
                  passview.Text = "Код не задан";
              }
        }
    }
}