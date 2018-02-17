using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarCollection
{
    public partial class FormClient : Form
    {
        public DataTable table = new DataTable();

        public FormClient()
        {
            InitializeComponent();
        }

        private void FormClient_Load(object sender, EventArgs e)
        {
            table.Columns.Add("Brand", typeof(string));
            table.Columns.Add("Model", typeof(string));
            table.Columns.Add("Color", typeof(string));
            table.Columns.Add("Date", typeof(DateTime));
            table.Columns.Add("Price", typeof(decimal));

            dataGridView.DataSource = ReadAndWriteFile.Load();

            comboBoxBrand.Text = "Select Brand";
            comboBoxModel.Text = "Select Model";

            foreach (Brand item in ReadAndWriteFile.GetBrands())
            {
                comboBoxBrand.Items.Add(item.Name);
            }

        }

        private void comboBoxBrand_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxModel.Items.Clear();
            comboBoxModel.Visible = true;
            Brand brand = new Brand(comboBoxBrand.SelectedItem.ToString());

            foreach (var item in ReadAndWriteFile.GetModels(brand))
            {
                comboBoxModel.Items.Add(item.Name);
            }

            dataGridView.DataSource = ReadAndWriteFile.Load(brand);
        }

        private void comboBoxModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            Brand brand = new Brand(comboBoxBrand.SelectedItem.ToString());
            Model model = new Model(comboBoxModel.SelectedItem.ToString());

            dataGridView.DataSource = ReadAndWriteFile.Load(brand, model);
        }

        private void btnBuy_Click(object sender, EventArgs e)
        {
            int rowIndex = dataGridView.CurrentCell.RowIndex;


            string str = null;

            //foreach (DataGridViewCell cell in row.Cells)
            //{
            //    string value = cell.Value.ToString();

            //}

            DataGridViewRow row = dataGridView.CurrentRow;


            for (int col = 1; col < dataGridView.Rows[rowIndex].Cells.Count; col++)
            {
                string value = dataGridView.Rows[rowIndex].Cells[col].Value.ToString();

                if (col == 4)
                    str += value;

                else
                    str += $"{value}~";
            }

            List<string> strings = new List<string>();

            StreamReader srCars = File.OpenText(Resources.PathCars);
            while (!srCars.EndOfStream)
            {
                strings.Add(srCars.ReadLine());
            }
            srCars.Close();
            File.Delete(Resources.PathCars);

            for (int i = 0; i < strings.Count; i++)
            {
                if (str.Equals(strings[i]) == false)
                {
                    StreamWriter swCars = File.AppendText(Resources.PathCars);

                    swCars.WriteLine(strings[i]);
                    swCars.Close();
                }
                else
                {
                    StreamWriter swCars = File.AppendText(Resources.PathSolds);

                    swCars.WriteLine(strings[i]);
                    swCars.Close();
                }
            }
            dataGridView.Rows.RemoveAt(rowIndex);
        }
    }
}

                                                                      