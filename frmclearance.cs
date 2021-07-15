using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net.Http;
using Newtonsoft.Json;

namespace eserve2
{
    public partial class frmclearance : Form
    {
        public static readonly HttpClient client = new HttpClient();
        public frmclearance()
        {
            InitializeComponent();
            getContents();
        }
        int[] ids = new int[1];
        async void getContents()
        {
            string filter = "";
            var res = await client.GetStringAsync("http://localhost/eservweb/api.php?clearance" + filter);

            cls = JsonConvert.DeserializeObject<Clearances>(res);
            listView1.Items.Clear();
            int ctr = 0;
            foreach (Clearance c in cls.data)
            {
                Array.Resize(ref ids, ctr + 1);
                ids[ctr] = c.id;
                string name = c.first_name + " " + c.middle_name + " " + c.last_name;
                string[] str = {
                    name,
                    "sitio "+c.sitio+", barangay "+c.barangay,
                    c.date_of_birth,
                    c.sex,
                    c.contact_number,
                    c.purpose
                };
                listView1.Items.Add(new ListViewItem(str));
                ctr++;
            }
        }
        Clearances cls = new Clearances();
        async void search()
        {

            string search = "";
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                search += "&search=" + textBox1.Text;
            }
            var res = await client.GetStringAsync("http://localhost/eservweb/api.php?clearance"+search);

            cls = JsonConvert.DeserializeObject<Clearances>(res);
            listView1.Items.Clear();
            int ctr = 0;
            foreach (Clearance c in cls.data)
            {
                Array.Resize(ref ids, ctr + 1);
                ids[ctr] = c.id;
                string name = c.first_name + " " + c.middle_name + " " + c.last_name;
                string[] str = {
                    name,
                    "sitio "+c.sitio+", barangay "+c.barangay,
                    c.date_of_birth,
                    c.sex,
                    c.contact_number,
                    c.purpose
                };
                listView1.Items.Add(new ListViewItem(str));
                ctr++;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            search();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count == 0)
            {
                MessageBox.Show("Select from the list first");
                return;
            }
            clearancedialog cd = new clearancedialog(cls.data[listView1.SelectedIndices[0]]);
            cd.ShowDialog();
        }

        private void frmclearance_Load(object sender, EventArgs e)
        {

        }
    }
}
