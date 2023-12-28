using System.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text;

namespace esdnevnik {
    public partial class Form1 : Form {

        int idx;
        DataTable table;

        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            idx = 0;
            getData();
            osvezi();
        }

        private void getData() {
            SqlConnection connection = Connection.Connect();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM osoba", connection);
            table = new DataTable();
            adapter.Fill(table);
        }

        private void osvezi() {
            if ( table.Rows.Count == -1 ) return;
            textBox1.Text = table.Rows[idx]["id"].ToString();
            textBox2.Text = table.Rows[idx]["ime"].ToString();
            textBox3.Text = table.Rows[idx]["prezime"].ToString();
            textBox4.Text = table.Rows[idx]["adresa"].ToString();
            textBox5.Text = table.Rows[idx]["jmbg"].ToString();
            textBox6.Text = table.Rows[idx]["email"].ToString();
            textBox7.Text = table.Rows[idx]["pass"].ToString();
            textBox8.Text = table.Rows[idx]["uloga"].ToString();

            btnFirst.Enabled = true;
            btnPrev.Enabled = true;
            btnLast.Enabled = true;
            btnNext.Enabled = true;
            if ( idx == 0 ) {
                btnFirst.Enabled = false;
                btnPrev.Enabled = false;
            } else if ( idx == table.Rows.Count - 1 ) {
                btnLast.Enabled = false;
                btnNext.Enabled = false;
            }
        }

        private void btnNext_Click(object sender, EventArgs e) {
            if ( idx < table.Rows.Count - 1 ) idx++;
            osvezi();
        }

        private void btnPrev_Click(object sender, EventArgs e) {
            if ( idx > 0 ) idx--;
            osvezi();
        }

        private void btnLast_Click(object sender, EventArgs e) {
            idx = table.Rows.Count - 1;
            osvezi();
        }

        private void btnFirst_Click(object sender, EventArgs e) {
            idx = 0;
            osvezi();
        }

        private void btnAdd_Click(object sender, EventArgs e) {
            string commandStr = "insert into osoba (ime, prezime, adresa, jmbg, email, pass, uloga) values('"
                + textBox2.Text + "', '"
                + textBox3.Text + "', '"
                + textBox4.Text + "', '"
                + textBox5.Text + "', '"
                + textBox6.Text + "', '"
                + textBox7.Text + "', '"
                + textBox8.Text + "')";
            SqlConnection connection = Connection.Connect();
            SqlCommand command = new SqlCommand(commandStr, connection);
            try {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                getData();
                idx = table.Rows.Count - 1;
                osvezi();
            } catch ( Exception ex ){
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e) {
            string commandStr =
                $"update osoba set ime = '{textBox2.Text}', " +
                $"prezime = '{textBox3.Text}', " +
                $"adresa = '{textBox4.Text}', " +
                $"jmbg = '{textBox5.Text}', " +
                $"email = '{textBox6.Text}', " +
                $"pass = '{textBox7.Text}', " +
                $"uloga = '{textBox8.Text}' " +
                $"where id = {textBox1.Text}";
            SqlConnection connection = Connection.Connect();
            SqlCommand command = new SqlCommand(commandStr, connection);
            try {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                getData();
                osvezi();
            } catch ( Exception ex ){
                MessageBox.Show(ex.Message);
            }

        }

        private void btnDel_Click(object sender, EventArgs e) {
            string commandStr = "delete from osoba where id = " + textBox1.Text;
            SqlConnection connection = Connection.Connect();
            SqlCommand command = new SqlCommand(commandStr, connection);
            try {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                getData();
                idx--;
                if ( idx < 0 ) idx = 1;
                osvezi();
            } catch ( Exception ex ){
                MessageBox.Show(ex.Message);
            }
        }
    }
}