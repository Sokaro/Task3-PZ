using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using WFLibrary;

namespace GitHubClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        RepositoryNameUserNameModel globalmodel=new RepositoryNameUserNameModel();

        private void button1_Click(object sender, EventArgs e)
        {
            string username = richTextBox1.Text;
            SocketClient mysupersocket2 = new SocketClient();
            List<RepositoryIdNameModel> model= mysupersocket2.GetUserReposytories(username);

            
            if(model.Capacity>0)
            {
                richTextBox2.Text = string.Empty;
                for(int i=0; i<model.Count;i++)
                {
                    richTextBox2.Text += String.Format("Repository Name: {0}\nRepository Id: {1}\n\n", model[i].Name, model[i].Id);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(richTextBox3.Text);
                SocketClient mysupersocket2 = new SocketClient();

                RepositoryNameUserNameModel model = mysupersocket2.GetRepository(id);
                richTextBox4.Text = String.Format("Repository Name: {0}\nUser Name: {1}", model.RepositoryName, model.UserName);
                globalmodel.RepositoryName = model.RepositoryName;
                globalmodel.UserName = model.UserName;
            }
            catch(Exception ex)
            {
                richTextBox4.Text = ex.ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SocketClient mysupersocket2 = new SocketClient();
            RepositoryDescriptionModel description = mysupersocket2.GetDescription(globalmodel);
            richTextBox5.Text = description.Description;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SocketClient SAITAMA = new SocketClient();
            List<RepositoryCommitsDateMessageModel> commits = SAITAMA.GetCommits(globalmodel);

            if(commits.Count>0)
            {
                richTextBox6.Text = string.Empty;
                for (int i=0; i<commits.Count;i++)
                {
                    richTextBox6.Text += String.Format("Message: {0}\nDate:{1}\n\n",commits[i].CommitMessage,commits[i].CommitDate);
                }
            }

        }
    }
}
