using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace CarCollection
{
    public partial class FormAdmin : Form
    {
        Salon salon = new Salon();
        public DataTable table = new DataTable();       

        public FormAdmin()
        {
            InitializeComponent();
            salon.SalonChanged += Salon_SalonChanged;
        }

        private void Salon_SalonChanged(object sender, SalonChangedEventArgs e)
        {
            switch (e.ChangeType)
            {
                case SalonChangedType.Add:
                    {
                        this.table.Rows.Add(e.Brand, e.Car.Model, e.Car.Color, e.Car.Year, e.Car.Price);

                        ReadAndWriteFile.SaveCars(e.Car);
                        ReadAndWriteFile.SaveBrands(e.Brand);
                        ReadAndWriteFile.SaveModels(e.Car.Model,e.Brand);
                        dataGridView.DataSource = ReadAndWriteFile.Load();

                        break;
                        
                    }
                case SalonChangedType.Edit:
                    break;
                case SalonChangedType.Delete:
                    break;
                default:
                    break;
            }

        }

        private void Admin_Load(object sender, EventArgs e)
        {
            table.Columns.Add("Brand", typeof(string));
            table.Columns.Add("Model", typeof(string));
            table.Columns.Add("Color", typeof(string));
            table.Columns.Add("Date", typeof(int));
            table.Columns.Add("Price", typeof(decimal));

            dataGridView.DataSource = ReadAndWriteFile.Load();

            comboBoxBrand.Text = "Select Brand";
            comboBoxModel.Text = "Select Model";
                                                  
            foreach (Brand item in ReadAndWriteFile.GetBrands())
            {
                comboBoxBrand.Items.Add(item.Name);
            }
        }

        private void carToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAdd form = new FormAdd();

            if (form.ShowDialog() == DialogResult.OK)
            {
                Brand b = form.Brand;
               
                Car c = new Car()
                {
                    Model = form.Model,
                    Color = form.Color,
                    Year = form.Year,
                    Price = form.Price
                };
                salon.Add(c,b); 
            }
        }

        private void addedToolStripMenuItem_Click(object sender, EventArgs e)
        { 
            dataGridView.DataSource=ReadAndWriteFile.Load();
        }

        private void brandsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.listBox.Items.Clear();

            foreach (Brand item in ReadAndWriteFile.GetBrands())
            {
                this.listBox.Items.Add(item);
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

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            int rowIndex = dataGridView.CurrentCell.RowIndex;
            dataGridView.Rows.RemoveAt(rowIndex);
        }

        private void btnRemove_Click(object sender, EventArgs e)
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
                str+=$"{value}~";
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
                    StreamWriter swCars = File.AppendText(Resources.PathDeleteds);

                    swCars.WriteLine(strings[i]);
                    swCars.Close();
                }
            }         
            dataGridView.Rows.RemoveAt(rowIndex);
        }                       
    }
}
