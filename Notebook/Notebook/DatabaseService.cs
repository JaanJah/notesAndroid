﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

namespace Notebook
{
    public class DatabaseService
    {
        SQLiteConnection db;

        public void CreateDatabase()
        {
            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "notebookdb.db3");
            db = new SQLiteConnection(dbPath);
        }

        public void CreateTableWithData(string noteMsg)
        {

            db.CreateTable<Notes>();
            if (db.Table<Notes>().Count() == 0)
            {
                var newNote = new Notes();
                newNote.Note = noteMsg;
                db.Insert(newNote);
            }
        }

        public void AddNote(string noteMsg)
        {
            var newNote = new Notes();
            newNote.Note = noteMsg;
            db.Insert(newNote);
        }

        public TableQuery<Notes> GetAllNotes()
        {
            var table = db.Table<Notes>();
            return table;
        }
    }
}