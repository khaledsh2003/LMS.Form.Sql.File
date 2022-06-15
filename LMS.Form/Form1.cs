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
        public string rowIndexID;
        public int rowIndexID2;
        public string bookNameToDelete;


        public Form1(IUserManager userManager, IBookManager bookManager)
        {
            InitializeComponent();
            _userManager = userManager;
            _bookManager= bookManager;
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
            bool IsInMenu=!cbMenu.Items.Contains(bookName);
            foreach (var i in _bookManager.GetList())
            {
                if (i.Copies == 0 )
                {
                    cbMenu.Items.Remove(i.Name);
             
                }
                if (IsInMenu && i.Copies!=0)
                {
                    cbMenu.Items.Add(bookName);
                }
            }
        }

        private void btnRent_Click(object sender, EventArgs e)
        {
            int id = 0;
            int id_2 = 0;
            string name = textBox1.Text;
            string phone = textBox2.Text;
            var list = _bookManager.GetList();

            _userManager.Create(name, phone, _selected,_FromDate,_ToDate);
            id=_bookManager.GetBook(_selected);
            MessageBox.Show(_selected);
            MessageBox.Show(id.ToString());

            list[id].Copies--;
            id_2=_userManager.GetUser(name, _selected);
           

            dataGridView1.Rows.Add(id_2, name, phone, _selected,_FromDate,_ToDate);

            AdjustCbMenu(_selected);




            _userManager.UpdateUserFile();
            _bookManager.UpdateBookFile();
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
                MessageBox.Show(rowIndex.ToString());
                rowIndexID = dataGridView1.Rows[rowIndex].Cells["Column1"].FormattedValue.ToString();
                rowIndexID2 = int.Parse(rowIndexID);
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
        }

        private void btnRemoveRenter_Click(object sender, EventArgs e)
        {

            string bookName = "";
            dataGridView1.Rows.RemoveAt(rowIndexID2);
            int id = rowIndexID2;

            //find book name
            foreach(var i in _userManager.GetList())
            {
                if (i.InstantId == id)
                {
                    bookNameToDelete = i.RentBoughtBook;

                }
            }

            _userManager.Delete(id);


            var b = _bookManager.GetList().FirstOrDefault(x => x.Name == bookNameToDelete);
            if (b != null) 
            {
                bookName = b.Name;
                MessageBox.Show(bookName);
                b.Copies++;

            }
            AdjustCbMenu(bookName);


            _userManager.UpdateUserFile();
            _bookManager.UpdateBookFile();
          

        }
        



    }
}