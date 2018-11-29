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
using Java.Lang;

namespace Notebook
{
    public class CustomAdapter : BaseAdapter<Notes>
    {
        List<Notes> items;
        Activity context;
        ImageButton editBtn;
        ImageButton deleteBtn;

        public CustomAdapter(Activity context, List<Notes> items) : base()
        {
            this.context = context;
            this.items = items;
        }

        public override Notes this[int position]
        {
            get { return items[position]; }
        }

        public override int Count
        {
            get { return items.Count; }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null; // could wrap a Contact in a Java.Lang.Object to return it here if needed
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;
            if (view == null)
                view = context.LayoutInflater.Inflate(Resource.Layout.CustomRow, null);
            view.FindViewById<TextView>(Resource.Id.noteMessage).Text = items[position].Note;

            editBtn = view.FindViewById<ImageButton>(Resource.Id.editBtn);
            editBtn.Tag = position;
            editBtn.Click += EditBtn_Click;

            deleteBtn = view.FindViewById<ImageButton>(Resource.Id.deleteBtn);
            deleteBtn.Tag = position;
            deleteBtn.Click += DeleteBtn_Click;
            NotifyDataSetChanged();
            return view;
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            var deleteBtnClicked = (ImageButton)sender;
            int position = (int)deleteBtnClicked.Tag;
            var dbService = new DatabaseService();
            dbService.CreateDatabase();
            dbService.RemoveNote(items[position]);
            NotifyDataSetChanged();
            dbService.GetAllNotes();
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}