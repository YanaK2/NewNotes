﻿using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace NewNotes.Models
{
    public class Note
    {
        [PrimaryKey,AutoIncrement]
        public int ID { get; set; }

        public string Title { get; set; }
        public string Text { get; set; }

        public DateTime Date { get; set; }

        public bool SecretNote { get; set; }

    }
}
