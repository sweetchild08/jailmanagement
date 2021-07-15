using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Windows.Forms;


namespace eserve2
{
    class helper
    {
        public static db db = new db();
        public static Form1 frmlogin = new Form1();
        public static frmmain frmmain = new frmmain();

        public static dialog dialog = new dialog();

        public static void SavePictureToFileSystem(Image picture,string path="")
        {
            string pictureFolderPath = Properties.Settings.Default.file_directory;
            Path.Combine(pictureFolderPath,path);
            if (!Directory.Exists(pictureFolderPath))
            {
                Directory.CreateDirectory(pictureFolderPath);
            }

            picture.Save(Path.Combine(pictureFolderPath, "1.jpg"));
        }

        public static byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }
        public static Image bytearrayToImage(byte[] bytes)
        {
            MemoryStream ms = new MemoryStream(bytes);
            Image im=new Bitmap(ms);
            return im;

        }
        public static int calcAge(DateTime bdate)
        {
            DateTime bday = bdate;
            DateTime today = DateTime.Today;
            int age = today.Year - bday.Year;
            return bday.Date > today.AddYears(-age) ? age - 1 : age;
        }

        public static void focus(TextBox t, Label placeholder)
        {
            t.Visible = true;
            t.Focus();
            placeholder.Visible = false;
        }

        public static void leave(TextBox t, Label placeholder)
        {
            if (string.IsNullOrEmpty(t.Text))
            {
                t.Visible = false;
                placeholder.Visible = true;
            }
            else
            {
                t.Visible = true;
                placeholder.Visible = false;
            }

        }
    }
    public class Clearances
    {
        public List<Clearance> data { get; set; }
    }
    public class Clearance
    {
        public int id { get; set; }
        public string last_name { get; set; }
        public string first_name { get; set; }
        public string middle_name { get; set; }
        public string sitio { get; set; }
        public string barangay { get; set; }
        public string date_of_birth { get; set; }
        public string sex { get; set; }
        public string civil_status { get; set; }
        public string place_of_birth { get; set; }
        public string citizenship { get; set; }
        public string religion { get; set; }
        public string occupation { get; set; }
        public string contact_number { get; set; }
        public string height { get; set; }
        public string weight { get; set; }
        public string purpose { get; set; }
    }
}
