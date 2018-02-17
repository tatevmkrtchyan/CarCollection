using System;
using System.Windows.Forms;

namespace CarCollection
{
    public partial class FormAdd : Form
    {
        public Brand Brand { get; set; }
        public Model Model { get; private set; }
        public string Color { get; private set; }
        public int Year { get; private set; }
        public decimal Price { get; private set; }

        public FormAdd()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
                // search enq anum @st modeli

            try
            {
                this.Brand = new Brand(this.txtBrand.Text);
                this.Model = new Model(this.txtModel.Text);

                this.Color = this.txtColor.Text;
                this.Year = System.Convert.ToInt32(this.txtYear.Text);
                this.Price = System.Convert.ToDecimal(this.txtPrice.Text);
                this.DialogResult = DialogResult.OK;

            }
            catch
            {
                MessageBox.Show("Incorrect format");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void FormAdd_Load(object sender, EventArgs e)
        {

        }
    }
}
