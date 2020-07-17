using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace Department
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection connection;
        SqlCommand command;
        String str = "Data Source=FPTU_L;Initial Catalog=FUH_COMPANY;Integrated Security=True";
       // SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Constr"].ConnectionString);
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();

        public void loadData()
        {
            command = connection.CreateCommand();
            command.CommandText = "select * from tblDepartment";
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            dgv.DataSource = table;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(str);
            connection.Open();
            loadData();
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            command = connection.CreateCommand();
            command.CommandText = "insert into tblDepartment values ('" +txtDepNum.Text+"', '"+txtDepName.Text+"'," +
                "'"+txtmrgSNN.Text+"', '"+dateMrgAss.Text+"') ";
            command.ExecuteNonQuery();
            loadData();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            command = connection.CreateCommand();
            command.CommandText = "update tblDepartment set depName = '"+txtDepName.Text+"', mgrSSN = '"+txtmrgSNN.Text+"'," +
                "mgrAssDate = '"+dateMrgAss.Text+"' where depNum ='"+txtDepNum.Text+"' ";
            command.ExecuteNonQuery();
            loadData();
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = dgv.CurrentRow.Index;
            txtDepNum.Text = dgv.Rows[i].Cells[0].Value.ToString();
            txtDepName.Text = dgv.Rows[i].Cells[1].Value.ToString();
            txtmrgSNN.Text = dgv.Rows[i].Cells[2].Value.ToString();
            dateMrgAss.Text = dgv.Rows[i].Cells[3].Value.ToString();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            command = connection.CreateCommand();
            command.CommandText = "delete from tblEmployee where depNum ='" + txtDepNum.Text + "'";
            command.CommandText = "delete from tblDepLocation where depNum ='" + txtDepNum.Text + "'";
            command.CommandText = "delete from tblDepartment where depNum= '" + txtDepNum.Text + "'";

            command.ExecuteNonQuery();
            loadData();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            txtDepNum.Text = "";
            txtDepName.Text = "";
            dateMrgAss.Text = "";
            txtmrgSNN.Text = "";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
