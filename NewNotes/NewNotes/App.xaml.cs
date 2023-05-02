using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NewNotes.Data;
using System.IO;

namespace NewNotes
{
    public partial class App : Application
    {
        static NotesDB notesDB;

        public static NotesDB NotesDB
        {
            get
            {
                if (notesDB == null)
                {
                    notesDB = new NotesDB(
                        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "NotesDatabase.db3"));
                }
                return notesDB;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private void Application_RequestedThemeChanged(object sender, AppThemeChangedEventArgs e)
        {

        }
    }
}
