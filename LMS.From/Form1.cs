using LMS.Bl.file;
using LMS.BL.Interface;

namespace LMS.From
{
    public partial class Form1 : Form
    {
       
        private string _selected;
        private string _FromDate;
        private string _ToDate;
        private IUserManager _userManager;
        private IBookManager _bookManager;
        public int rowIndex;
        public string rowIndexBook;


        public Form1()
        {
            InitializeComponent();
            _userManager = new UserFileManager();
            _bookManager = new BookFileManager();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dateTimePicker1.MinDate = DateTime.Now;
            dateTimePicker2.MinDate = DateTime.Now;
            foreach(var i in _bookManager.GetList())
            {
                if (i.Copies != 0)
                {
                    cbMenu.Items.Add(i.Name);
                }
            }
            foreach(var i in _userManager.GetList())
            {
                dataGridView1.Rows.Add(i.InstantId, i.Name, i.PhoneNum, i.RentBoughtBook, i.FromDate, i.ToDate);


            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void AdjustCbMenu(string bookName)
        {

            bool IsInMenu=cbMenu.Items.Contains(bookName);
            foreach (var i in _bookManager.GetList())
            {
                if (i.Copies == 0 )
                {
                    cbMenu.Items.Remove(i.Name);
             
                }
                if (!IsInMenu && i.Copies!=0)
                {
                    cbMenu.Items.Add(bookName);
                    IsInMenu = true;
                }
            }
        }

        private void btnRent_Click(object sender, EventArgs e)
        {
            int id = 0;
            string name = textBox1.Text;
            string phone = textBox2.Text;

            _userManager.Create(name, phone, _selected,_FromDate,_ToDate);
            
            _bookManager.DecreaseCopies(_selected);

            id=_userManager.GetUserById(name, _selected);
           

            dataGridView1.Rows.Add(id, name, phone, _selected,_FromDate,_ToDate);

            AdjustCbMenu(_selected);




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
        }

        


        private void btnRemoveRenter_Click(object sender, EventArgs e)
        {
            int id=0;
           
            dataGridView1.Rows.RemoveAt(rowIndex);
            //find id
           

            foreach(var i in _userManager.GetList())
            {
                if(i.RentBoughtBook == rowIndexBook)
                {
                    id=_userManager.GetList().IndexOf(i);
                }
            }

            _userManager.Delete(id);


            var b = _bookManager.GetList().FirstOrDefault(x => x.Name == rowIndexBook);
            if (b != null) 
            {   
                b.Copies++;
                _bookManager.DecreaseCopies("");
            }
          
            AdjustCbMenu(rowIndexBook);


            dataGridView1.ClearSelection();


        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow.Cells["ID"].Value != null)
            {
                rowIndex = dataGridView1.CurrentCell.RowIndex;
                rowIndexBook = dataGridView1.Rows[rowIndex].Cells["RentedBook"].FormattedValue.ToString();
            }
           
        }
    }
}