using CarFactoryBusinessLogic.BindingModels;
using CarFactoryBusinessLogic.BusinessLogics;
using CarFactoryBusinessLogic.ViewModels;
using System;
using System.Windows.Forms;
using Unity;

namespace CarFactoryView
{
    public partial class FormCreateOrder : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly CarLogic _logicС;

        private readonly OrderLogic _logicO;

        private readonly ClientLogic _logicClient;

        public FormCreateOrder(CarLogic logicС, OrderLogic logicO, ClientLogic logicClient)
        {
            InitializeComponent();
            _logicС = logicС;
            _logicO = logicO;
            _logicClient = logicClient;
        }

        private void FormCreateOrder_Load(object sender, EventArgs e)
        {
            try
            {
                var cars = _logicС.Read(null);
                var clients = _logicClient.Read(null);
                comboBoxCar.DataSource = cars;
                comboBoxCar.DisplayMember = "CarName";
                comboBoxCar.ValueMember = "Id";
                comboBoxClients.DataSource = clients;
                comboBoxClients.DisplayMember = "ClientFIO";
                comboBoxClients.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }

        private void CalcSum()
        {
            if (comboBoxCar.SelectedValue != null &&
           !string.IsNullOrEmpty(textBoxCount.Text))
            {
                try
                {
                    int id = Convert.ToInt32(comboBoxCar.SelectedValue);
                    CarViewModel car = _logicС.Read(new CarBindingModel
                    {
                        Id = id
                    })?[0];
                    int count = Convert.ToInt32(textBoxCount.Text);
                    textBoxSum.Text = (count * car?.Price ?? 0).ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
        }

        private void TextBoxCount_TextChanged(object sender, EventArgs e)
        {
            CalcSum();
        }

        private void ComboBoxCar_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalcSum();
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Fill Count", "Error",
               MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxCar.SelectedValue == null)
            {
                MessageBox.Show("Select car", "Error", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (comboBoxClients.SelectedValue == null)
            {
                MessageBox.Show("Select client", "Error", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            try
            {
                _logicO.CreateOrder(new CreateOrderBindingModel
                {
                    CarId = Convert.ToInt32(comboBoxCar.SelectedValue),
                    ClientId = Convert.ToInt32(comboBoxClients.SelectedValue),
                    Count = Convert.ToInt32(textBoxCount.Text),
                    Sum = Convert.ToDecimal(textBoxSum.Text)
                });
                MessageBox.Show("Saved", "Message",
               MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
