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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Text = "Movie Manager";

            label5.Text = DataManager.Movies.Count.ToString();
            label6.Text = DataManager.Users.Count.ToString();
            label7.Text = DataManager.Movies.Where((x) => x.IsBorrowed).Count().ToString();
            label8.Text = DataManager.Movies.Where((x) =>
            { 
                return x.IsBorrowed && x.BorrowedAt.AddDays(7) < DateTime.Now; 
            }).Count().ToString();

            dataGridView1.DataSource = DataManager.Movies;
            dataGridView2.DataSource = DataManager.Users;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (textBox1.Text == "")
            {
                MessageBox.Show("Code를 입력하세요.");

            }
            else if (textBox3.Text == "")
            {
                MessageBox.Show("사용자 Id를 입력하세요.");

            }
            else
            {
                try
                {
                    Movie movie = DataManager.Movies.Single((x) => x.Code == textBox1.Text);
                    if (movie.IsBorrowed)
                    {
                        MessageBox.Show("이미 대여 중인 영화입니다");
                    }
                    else
                    {
                        User user = DataManager.Users.Single((x) => x.Id.ToString() == textBox3.Text);
                        movie.UserId = user.Id;
                        movie.UserName = user.Name;
                        movie.IsBorrowed = true;
                        movie.BorrowedAt = DateTime.Now;

                        dataGridView1.DataSource = null;
                        dataGridView1.DataSource = DataManager.Movies;
                        DataManager.Save();

                        MessageBox.Show("\"" + movie.Name + "\"이 대여되었습니다");

                        // 대여자 숫자 표시 변경
                        label7.Text = DataManager.Movies.Where((x) => x.IsBorrowed).Count().ToString();
                    }
                }catch(Exception ex)
                {
                    MessageBox.Show("존재하지 않는 영화/사용자 입니다.");
                }
                
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
                MessageBox.Show("Code를 입력해주세요");
            else
            {
                Movie movie = DataManager.Movies.Single((x) => x.Code == textBox1.Text);
                if (movie.IsBorrowed)
                {
                    User user = DataManager.Users.Single((x) => x.Id.ToString() == movie.UserId.ToString());
                    movie.UserId = 0;
                    movie.UserName = "";
                    movie.IsBorrowed = false;
                    movie.BorrowedAt = new DateTime();

                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = DataManager.Movies;
                    DataManager.Save();

                    if (movie.BorrowedAt.AddDays(7) > DateTime.Now)
                        MessageBox.Show("\" " + movie.Name + " \" 이 연체되었습니다.");
                    else
                        MessageBox.Show("\" " + movie.Name + " \"이 반납되었습니다.");

                    label7.Text = DataManager.Movies.Where((x) => x.IsBorrowed).Count().ToString();
                }
                else
                    MessageBox.Show("대여하지 않았습니다.");
            }
        }
        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                Movie movie = dataGridView1.CurrentRow.DataBoundItem as Movie;
                textBox1.Text = movie.Code;
                textBox2.Text = movie.Name;
            }
            catch (Exception ex) { }
        }

        private void dataGridView2_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                User user = dataGridView2.CurrentRow.DataBoundItem as User;
                textBox3.Text = user.Id.ToString();
            }
            catch (Exception ex)
            {
               
            }
        }

        private void 영화관리ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Form2().ShowDialog();
        }

        private void 사용자관리ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Form3().ShowDialog();
        }
    }
}
