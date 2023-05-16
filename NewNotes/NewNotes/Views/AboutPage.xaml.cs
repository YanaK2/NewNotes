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
        protected override async void OnAppearing()
        {
            
            
            try
            {
                int id = 1;

                Password pass = await App.NotesDB.GetPasswordAsync(id);
                if (pass.Text != null)
                {
                    passwordbutton.IsVisible = false;
                    changepasswordbutton.IsVisible = true;
                    newpass.Placeholder = "Изменить пароль";
                } else 
                {
                    passwordbutton.IsVisible = true;
                    changepasswordbutton.IsVisible = false;
                }
            } catch(NullReferenceException) 
            {
                passwordbutton.IsVisible = true;
                changepasswordbutton.IsVisible = true;
            }
          
            base.OnAppearing();

        }
        public AboutPage()
        {
            
            InitializeComponent();

        }
        //сохранение первого пароля
        private async void PasswordButton_Clicked(object sender, EventArgs e)
        {
            Password password = new Password();
            password.Text = newpass.Text;
            await App.NotesDB.SavePasswordAsync(password);
            Console.WriteLine(password.ID);
            newpass.Text = "";
            int id = 1;

            Password pass = await App.NotesDB.GetPasswordAsync(id);
            Console.WriteLine(pass.Text);
            OnAppearing();
            
        }
        //смена пароля 
        private async void ChangePasswordButton_Clicked(object sender, EventArgs e)
        {
              int id = 1;
              Password pass = await App.NotesDB.GetPasswordAsync(id);
              string CheckPass = await DisplayPromptAsync("Повторите код", "Для установки нового кода необходимо подтвердить старый:", "Ок", "Отмена");
            if (CheckPass == null || CheckPass!=pass.Text) 
            {
                await DisplayAlert("Неверный код", "Повторите попытку", "Ок");
            } else
            {
            pass.Text = newpass.Text;
            pass.ID = 1;
            await App.NotesDB.SavePasswordAsync(pass);
            //Console.WriteLine(pass.ID); это для тестирования
           // Console.WriteLine(pass.Text);
            newpass.Text = "";
            await DisplayAlert("Код сменен", "Новый код установлен", "Ок");
            }
              
        }
    }
}