using System.Windows.Forms;

namespace CarCollection
{
    public partial class FormLogIn : Form
    {
        public FormLogIn()
        {
            InitializeComponent();
        }

        private void btnAdmin_Click(object sender, System.EventArgs e)
        {
            FormAdmin form = new FormAdmin();
            form.ShowDialog();
        }

        private void btnClient_Click(object sender, System.EventArgs e)
        {
            FormClient form = new FormClient();
            form.ShowDialog();
        }
    }
}
