using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 개인프로젝트제출
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();

            Text = "사용자 관리";

            dataGridView1.DataSource = DataManager.Users;
            dataGridView1.CurrentCellChanged += dataGridView1_CurrentCellChanged;

            button1.Click += (sender, e) =>
            {
                try
                {

                    if (DataManager.Users.Exists(x => x.Id == int.Parse(textBox1.Text)))
                    {
                        MessageBox.Show("이미 존재하는 ID입니다.")
                    }
                    else
                    {
                        User user = new User()
                        {
                            Id = int.Parse(textBox1.Text),
                            Name = textBox2.Text,

                        };
                        DataManager.Users.Add(book);
                        dataGridView1.DataSource = null;
                        dataGridView1.DataSource = DataManager.Users;
                        DataManager.Save();
                    }
                }
                catch (Exception ex) { }
            };

            button2.Click += (sender, e) =>
            {
                try
                {
                    User user = DataManager.Users.Single((x) => x.Id == int.Parse(textBox1.Text));
                    user.Name = textBox2.Text;
                    
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = DataManager.Users;
                    DataManager.Save();
                }

                catch (Exception ex)
                {
                    MessageBox.Show("없는 사용자 입니다.")
                }
            };

            button3.Click += (sender, e) =>
            {
                try
                {
                    User user = DataManager.Users.Single((x) => x.Id == int.Parse(textBox1.Text));

                    DataManager.Users.Remove(book);

                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = DataManager.Users;
                    DataManager.Save();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("없는 사용자 입니다.")
                }
            };
        }
    }

    private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
    {
        try
        {
            User user = dataGridView1.CurrentRow.DataBoundItem as User;
            textBox1.Text = user.Id.ToString();
            textBox2.Text = book.Name;
        }
        catch (Exception ex) { }

    }
}
