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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

            Text = "영화 관리";

            dataGridView1.DataSource = DataManager.Movies;
            dataGridView1.CurrentCellChanged += dataGridView1_CurrentCellChanged;

            button1.Click += (sender, e) =>
            {
                try
                {

                    if (DataManager.Movies.Exists(x => x.Code == textBox1.Text))
                    {
                        MessageBox.Show("이미 존재하는 작품입니다.")
                    }
                    else
                    {
                        Book book = new Movie()
                        {
                            Code = textBox1.Text,
                            Name = textBox2.Text,
                            Director = textBox3.Text

                        };
                        DataManager.Movies.Add(book);

                    }
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = DataManager.Movies;
                    DataManager.Save();
                }
                catch (Exception ex) { }

            };

            button2.Click += (sender, e) =>
            {
                try
                {
                    Movie movie = DataManager.Movies.Single((x) => x.Code == textBox1.Text);
                    movie.Name = textBox2.Text;
                    movie.Director = textBox3.Text;

                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = DataManager.Movies;
                    DataManager.Save();
                }

                catch (Exception ex)
                {
                    MessageBox.Show("목록에 없는 영화입니다.")
                }
            };

            button3.Click += (sender, e) =>
            {
                try
                {
                    Movie movie = DataManager.Movies.Single((x) => x.Code == textBox1.Text);

                    DataManager.Movies.Remove(book);

                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = DataManager.Movies;
                    DataManager.Save();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("목록에 없는 영화입니다.")
                }
            };
        }
        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
                {
                    try
                    {
                        Movie movie = dataGridView1.CurrentRow.DataBoundItem as Movie;
                        textBox1.Text = book.Code;
                        textBox2.Text = book.Name;
                        textBox3.Text = book.Director;
                    }
                    catch (Exception ex) { }

                }
    }

        

       