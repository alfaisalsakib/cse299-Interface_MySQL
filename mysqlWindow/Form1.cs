using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;

namespace mysqlWindow
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Fetch_Click(object sender, EventArgs e)
        {
            GetRequest();
            GetJsonRequest();
        }

        async static void GetRequest()
        {
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.GetAsync("http://softgulf-tech.com/cse299/get_Student_Info.php"))
                {
                    using (HttpContent content = response.Content)
                    {
                        string myContent = await content.ReadAsStringAsync();
                        var arr = await content.ReadAsByteArrayAsync();
                        Console.WriteLine(arr);
                        MessageBox.Show(myContent);
                        //MessageBox.Show(arr[0]);
                    }
                }
            }
        }
        async static void GetJsonRequest()
        {
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.GetAsync("http://softgulf-tech.com/cse299/get_Student_Info.php"))
                {
                    using (HttpContent content = response.Content)
                    {
                        string myContent = await content.ReadAsStringAsync();
                        var std = JsonConvert.DeserializeObject<dynamic>(myContent);
                        MessageBox.Show(std[6].ToString());
                    }
                }
            }
        }

        private void submit_Click(object sender, EventArgs e)
        {
            try
            {
                string user = textBox1.Text;
                string pass = textBox2.Text;

                ASCIIEncoding encoding = new ASCIIEncoding();
                string postData = "user=" + user + "&pass=" + pass;
                byte[] data = encoding.GetBytes(postData);

                WebRequest request = WebRequest.Create("http://softgulf-tech.com/cse299/student_login.php");
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;

                Stream stream = request.GetRequestStream();
                stream.Write(data, 0, data.Length);
                stream.Close();

                WebResponse response = request.GetResponse();
                stream = response.GetResponseStream();

                StreamReader sr = new StreamReader(stream);
                MessageBox.Show(sr.ReadToEnd());

                sr.Close();
                stream.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }

        
    }
}
