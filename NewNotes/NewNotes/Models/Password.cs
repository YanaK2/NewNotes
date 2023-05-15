﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewNotes.Models
{
    public class Password
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Text { get; set; }
    }
}
