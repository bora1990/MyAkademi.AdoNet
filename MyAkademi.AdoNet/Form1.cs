using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyAkademi.AdoNet
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection connection = new SqlConnection("Server=DESKTOP-L5FHJU6;initial Catalog=MyPortfolioDb;Integrated Security=true");

        private void Form1_Load(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand("Select * from Category", connection);
            cmbCategory.DisplayMember = "CategoryName";
            cmbCategory.ValueMember = "CategoryID";
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            cmbCategory.DataSource = dt;
            connection.Close();
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("Select * from Project", connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dt=new DataTable();
            adapter.Fill(dt);   
            dataGridView1.DataSource = dt;

            connection.Close();

        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            connection.Open();

            SqlCommand command = new SqlCommand("Insert into Project (Title,Description,ProjectCategory,CompleteDay,Price) values (@p1,@p2,@p3,@p4,@p5)",connection);
            command.Parameters.AddWithValue("@p1",txtProjectTitle.Text); 
            command.Parameters.AddWithValue("@p2",rchDetail.Text); 
            command.Parameters.AddWithValue("@p3",cmbCategory.Text); 
            command.Parameters.AddWithValue("@p4",txtProcessValue.Text); 
            command.Parameters.AddWithValue("@p5",txtPrice.Text); 
            command.ExecuteNonQuery();         
            connection.Close();
            MessageBox.Show("Proje Bilgisi Başarıyla Eklendi");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("Delete from Project where ProjectId=@p1", connection);
            command.Parameters.AddWithValue("@p1", txtID.Text);
            command.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Proje Bilgisi Silindi");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("Update Project Set Title=@p1,Description=@p2,ProjectCategory=@p3,CompleteDay=@p4,Price=@p5 where ProjectID=@p6 ",connection);
            command.Parameters.AddWithValue("@p1", txtProjectTitle.Text);
            command.Parameters.AddWithValue("@p2", rchDetail.Text);
            command.Parameters.AddWithValue("@p3", cmbCategory.Text);
            command.Parameters.AddWithValue("@p4", txtProcessValue.Text);
            command.Parameters.AddWithValue("@p5", txtPrice.Text);
            command.Parameters.AddWithValue("p6",txtID.Text);
            command.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Proje Bilgisi Başarıyla Güncellendi","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Warning);
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("Select * from Project where Title=@p1",connection);
            command.Parameters.AddWithValue("@p1",txtProjectTitle.Text);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable(); 
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            connection.Close();

        }
    }
}
