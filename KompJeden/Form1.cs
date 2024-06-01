using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KompJeden
{
    public partial class Form1 : Form
    {
        NotesDataContext ndc;
        public Form1()
        {
            ndc = new NotesDataContext();
            InitializeComponent();
            foreach(Note n in ndc.Notes)
            {
                listBoxMain.Items.Add(n);
            }
        }

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            string name = textBoxPreviewName.Text;
            if (name.Length == 0 || name.Length > 40) return;

            string description = richTextBoxPreviewContent.Text;
            if (description.Length == 0 || description.Length > 400) return;

            Note newNote = new Note();
            newNote.Name = name;
            newNote.Description = description;

            ndc.Notes.InsertOnSubmit(newNote);
            listBoxMain.Items.Add(newNote);
            ndc.SubmitChanges();

            if(!(listBoxMain.SelectedItem is Note)) setPreviewText("", "");
        }

        private void listBoxMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(!(listBoxMain.SelectedItem is Note))
            {
                setPreviewText("", "");
                return;
            }

            Note selectedNote = (listBoxMain.SelectedItem as Note);
            setPreviewText(selectedNote.Name, selectedNote.Description);
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (!(listBoxMain.SelectedItem is Note)) return;

            Note note = (listBoxMain.SelectedItem as Note);
            listBoxMain.Items.Remove(note);
            ndc.Notes.DeleteOnSubmit(note);
            ndc.SubmitChanges();
            setPreviewText("", "");
        }

        private void setPreviewText(string name, string desc)
        {
            textBoxPreviewName.Text = name;
            richTextBoxPreviewContent.Text = desc;
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (!(listBoxMain.SelectedItem is Note)) return;

            string name = textBoxPreviewName.Text;
            if (name.Length == 0 || name.Length > 40) return;

            string description = richTextBoxPreviewContent.Text;
            if (description.Length == 0 || description.Length > 400) return;

            Note note = (listBoxMain.SelectedItem as Note);
            note.Name = name;
            note.Description = description;
            ndc.SubmitChanges();
            listBoxMain.Items[listBoxMain.SelectedIndex] = note;
        }
    }
}
