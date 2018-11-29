using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;

namespace Notebook
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        EditText inputText;
        string note;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            var list = FindViewById<ListView>(Resource.Id.listView1);
            inputText = FindViewById<EditText>(Resource.Id.inputText);
            var submitBtn = FindViewById<Button>(Resource.Id.submitBtn);

            var databaseService = new DatabaseService();
            databaseService.CreateDatabase();
            databaseService.CreateTableWithData(note);
            var notes = databaseService.GetAllNotes();
            databaseService.GetAllNotes();
            submitBtn.Click += delegate
            {
                note = inputText.Text;
                databaseService.AddNote(note);

                notes = databaseService.GetAllNotes();
                list.Adapter = new CustomAdapter(this, notes.ToList());
            };
            list.Adapter = new CustomAdapter(this, notes.ToList());
        }
    }
}
