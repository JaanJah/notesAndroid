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
        ImageButton refreshBtn;
        ListView list;
        DatabaseService databaseService;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            list = FindViewById<ListView>(Resource.Id.listView1);
            inputText = FindViewById<EditText>(Resource.Id.inputText);
            var submitBtn = FindViewById<Button>(Resource.Id.submitBtn);
            refreshBtn = FindViewById<ImageButton>(Resource.Id.refreshBtn);
            databaseService = new DatabaseService();
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
            refreshBtn.Click += RefreshBtn_Click;
            list.Adapter = new CustomAdapter(this, notes.ToList());
        }

        private void RefreshBtn_Click(object sender, System.EventArgs e)
        {
            var listAdapter = new CustomAdapter(this, databaseService.GetAllNotes().ToList());
            list.Adapter = listAdapter;
            listAdapter.NotifyDataSetChanged();
        }
    }
}
