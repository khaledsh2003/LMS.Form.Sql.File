using LMS.Bl.Sql;
using LMS.Interface;
using LMS.Bl;
using LMS.BI;
using LMS.Bl.file;
using LMS.Bl.File;

namespace LMS.From
{
    public partial class Form1 : Form
    {
        private string _selected;
        private string _fullSelected;
        private string _FromDate;
        private string _ToDate;
        private IUserManager _userManager;
        private IBookManager _bookManager;
        public int rowIndex;
        public int UserIDToDelete;


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
            foreach (var i in _bookManager.GetBooksList())
            {
                cbMenu.Items.Add("ID: " + i.Id + ", Book name: " + i.Name);
            }  
        }
        private void AddUsersToGrid()
        {
            foreach (var i in _userManager.GetUsersList())
            {
                dataGridView1.Rows.Add(i.id, i.Name, i.PhoneNum, i.RentBoughtBook, i.FromDate, i.ToDate, i.bookID);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            

            dateTimePicker1.MinDate = DateTime.Now;
            dateTimePicker2.MinDate = DateTime.Now;
            AddToCbMenu();
            AddUsersToGrid();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        
        private void DeleteFromCbMenu(int bookId)
        {
            foreach(var i in _bookManager.GetBooksList())
            {
                if (i.Id == bookId)
                {
                    cbMenu.Items.Remove("ID: " + i.Id + ", Book name: " + i.Name);
                }
            }
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
                _bookManager.DecreaseCopies(bookId);
                DeleteFromCbMenu(bookId);
                foreach(var i in _userManager.GetUsersList())
                {
                    if (i.PhoneNum == phone)
                    {
                        dataGridView1.Rows.Add(i.id, i,name, i.PhoneNum, i.RentBoughtBook,i.FromDate, i.ToDate);
                    }
                }
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
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                dataGridView1.CurrentRow.Selected = true;
                rowIndex = dataGridView1.CurrentCell.RowIndex;
                UserIDToDelete= int.Parse(dataGridView1.Rows[rowIndex].Cells["Column1"].FormattedValue.ToString());
                _selected = dataGridView1.Rows[rowIndex].Cells["Column4"].FormattedValue.ToString();

            }

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

            if (dateTimePicker1.Value.Day > dateTimePicker2.Value.Day)
            {
                MessageBox.Show("Error from greater than to");
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
                MessageBox.Show("Error from greater than to");
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
            string IdAndBookName = "";
            string bookName =_selected;
            int bookId = 0;
            _userManager.RemoveUserById(UserIDToDelete);
            AddUsersToGrid();
            foreach(var i in _bookManager.GetBooksList())
            {
                if (i.Name == bookName)
                {
                    bookId = i.Id;
                }
            }
            _bookManager.IncreaseCopies(bookId);
            IdAndBookName = "ID: " + bookId + ", Book name: " + bookName;
            if (!cbMenu.Items.Contains(IdAndBookName))
            {
                cbMenu.Items.Add(IdAndBookName);
            }

        }
    }
}