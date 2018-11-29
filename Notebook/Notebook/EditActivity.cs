using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace Notebook
{
    [Activity(Label = "editActivity")]
    public class EditActivity : Activity
    {
        EditText inputText;
        Button submitBtn;
        Notes note;
        DatabaseService databaseService;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.editActivity);
            inputText = FindViewById<EditText>(Resource.Id.inputText);
            submitBtn = FindViewById<Button>(Resource.Id.submitBtn);
            databaseService = new DatabaseService();
            databaseService.CreateDatabase();
            note = JsonConvert.DeserializeObject<Notes>(Intent.GetStringExtra("Note"));

            submitBtn.Click += SubmitBtn_Click;
        }

        private void SubmitBtn_Click(object sender, EventArgs e)
        {
            note.Note = inputText.Text;
            databaseService.UpdateNote(note);
            Finish();
        }
    }
}