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
    public partial class frmlostnfound : Form
    {
        public static readonly HttpClient client = new HttpClient();
        public frmlostnfound()
        {
            InitializeComponent();
            getContents();
        }
        int[] ids = new int[1];
        async void getContents()
        {
            string filter = "";
            if (comboBox3.SelectedIndex > 0)
            {
                filter += "&type="+comboBox3.Text;
            }
            if (comboBox1.SelectedIndex > 0)
            {
               filter += "&status=" + comboBox1.Text;
            }
            var res = await client.GetStringAsync("http://localhost/eservweb/api.php?lostnfound" + filter) ;

            lost lost= JsonConvert.DeserializeObject<lost>(res);
            listView1.Items.Clear();
            int ctr = 0;
            foreach (Lostnfound l in lost.data )
            {
                Array.Resize(ref ids, ctr + 1);
                ids[ctr] = l.id;
                string[] str = { l.name,l.type,l.contact,l.subject,l.location, l.status, l.notes };
                listView1.Items.Add(new ListViewItem(str));
                ctr++;
            }
        }
        async void action (string type = "approved" )
        {

            if (listView1.SelectedIndices.Count == 0)
            {
                MessageBox.Show("select on the list first");
                return;
            }
            string status = listView1.SelectedItems[0].SubItems[5].Text;
            if (status!="pending")
            {
                MessageBox.Show("selected item was already "+status);
                return;
            }
            var values = new Dictionary<string, string>
            {
                { "lostnfound", ids[listView1.SelectedIndices[0]].ToString() },
                { "value", type}
            };

            //form "postable object" if that makes any sense
            var content = new FormUrlEncodedContent(values);
            var res = await client.PostAsync("http://localhost/eservweb/api.php", content);
            var responseString = await res.Content.ReadAsStringAsync();
            response reponse = JsonConvert.DeserializeObject<response>(responseString);
            MessageBox.Show(reponse.msg);
            getContents();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void frmlostnfound_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            action();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            getContents();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            getContents();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            action("rejected");
        }
    }

}
