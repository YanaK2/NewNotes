using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using NewNotes.Models;
using System.Threading.Tasks;
using NewNotes.Views;

namespace NewNotes.Data
{
   public class NotesDB
    {
        readonly SQLiteAsyncConnection db;

        public NotesDB(string connectionString) { 
        
            db = new SQLiteAsyncConnection(connectionString);

            db.CreateTableAsync<Note>().Wait();
            db.CreateTableAsync<Password>().Wait(); 

        }
        public Task<List<Note>> GetNotesAsync() 
        {
            return db.Table<Note>().ToListAsync();
        }
        public Task<List<Password>> GetPasswordsAsync()
        {
            return db.Table<Password>().ToListAsync();
        }
        public Task<Note> GetNoteAsync(int id)
        {
            return db.Table<Note>().Where(i=>i.ID == id).FirstOrDefaultAsync();
        }
        public Task<Password> GetPasswordAsync(int id)
        {
            return db.Table<Password>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveNoteAsync(Note note) {
        
        if(note.ID!=0)
            {
                return db.UpdateAsync(note);

            }
        else
            {
                return db.InsertAsync(note);
            }
        }
        public Task<int> SavePasswordAsync(Password password)
        {

            if (password.ID != 0)
            {
                return db.UpdateAsync(password);

            }
            else
            {
                return db.InsertAsync(password);
            }
        }
        public Task<int> DeleteNoteAsync(Note note) {
        
            return db.DeleteAsync(note);

        }
    }
}
