using LMS.Bl.Sql;
using LMS.BL.Interface;
using System.Data;
using System.Data.SqlClient;

namespace LMS.From
{
    public partial class Form1 : Form
    {

        private string _selected;
        private string _fullSelected;

        private string _FromDate;
        private string _ToDate;
        private IUserManagerSql _userManager;
        private IBookManagerSql _bookManager;
        public int rowIndex;
        public int rowIndexBook;


        public Form1()
        {
            InitializeComponent();
            _userManager = new UserSqlManager();
            _bookManager = new BookSqlManager();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void AddToCbMenu()
        {
            int copies = 0;
            SqlDataReader reader = _bookManager.GetBooksReader();
            while (reader.Read())
            {
                copies = int.Parse(reader[2].ToString());
                if (copies > 0)
                {
                    cbMenu.Items.Add("ID: " + reader[0] + ", Book name: " + reader[1]);

                }
            }
        }
        private void DeleteFromCbMenu(int bookId)
        {
            int copies;
            int index=0;
            SqlDataReader reader = _bookManager.GetBooksReader();
            while (reader.Read())
            {
                copies =int.Parse(reader[2].ToString());
                if (copies==0)
                {
                    cbMenu.Items.Remove(_fullSelected);

                }
            }
        }
        private void ReadUsersInForm()
        {
            SqlDataAdapter userAdapter = _userManager.GetUsersReader();
            DataTable userData = new DataTable();
            userAdapter.Fill(userData);
            dataGridView1.DataSource = userData;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            dateTimePicker1.MinDate = DateTime.Now;
            dateTimePicker2.MinDate = DateTime.Now;

            AddToCbMenu();
            ReadUsersInForm();


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }


        private void btnRent_Click(object sender, EventArgs e)
        {
            string name, phone;
            int bookId;
            bool isBookAval;
            name = textBox1.Text;
            phone = textBox2.Text;
            bookId = _bookManager.GetBookIdByName(_selected);
            isBookAval = _bookManager.IsBookAval(bookId);
            if (isBookAval)
            {
                _userManager.CreateUser(name, phone, _selected, _FromDate, _ToDate, bookId);
                DeleteFromCbMenu(bookId);
                ReadUsersInForm();

            }
            else
            {
                MessageBox.Show("Book not Avaliable");
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsNumber(e.KeyChar)) && (!char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void comboBox1_DropDownStyleChanged(object sender, EventArgs e)
        {

        }


        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

            if (dateTimePicker1.Value.Day > dateTimePicker2.Value.Day)
            {
                MessageBox.Show("Error from date is greater than to date");
                dateTimePicker1.Value = dateTimePicker2.Value;


            }
            else
            {
                _FromDate = dateTimePicker1.Value.ToShortDateString();
            }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value.Day > dateTimePicker2.Value.Day)
            {
                MessageBox.Show("Error from date is greater than to date");
                dateTimePicker1.Value = DateTime.Now;
            }
            else
            {
                _ToDate = dateTimePicker2.Value.ToShortDateString();
            }
        }

        private void cbMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selected = cbMenu.Text;
            _fullSelected = _selected;
            int length = _selected.Length - 18;
            _selected = _selected.Substring(18, length);
        }




        private void btnRemoveRenter_Click(object sender, EventArgs e)
        {
            //FIX ADDING BY TO MENU only
            int id=_userManager.GetBookIdByUserId(int.Parse(textBox3.Text));
            string bookName= _bookManager.GetBookNameUserID(id);
            _userManager.RemoveUserById(int.Parse(textBox3.Text));
            ReadUsersInForm();
            cbMenu.Items.Add("ID: " + id + ", Book name: " + bookName);


        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {/*
            if (dataGridView1.CurrentRow.Cells["ID"].Value != null)
            {
                string ID;
                rowIndex = dataGridView1.CurrentCell.RowIndex;
                ID = dataGridView1.Rows[rowIndex].Cells["ID"].FormattedValue.ToString();
                rowIndexBook = int.Parse(ID);
            }
            */
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}