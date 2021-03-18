using CarFactoryBusinessLogic.BindingModels;
using CarFactoryBusinessLogic.BusinessLogics;
using CarFactoryBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;

namespace CarFactoryView
{
    public partial class FormAddComponentsToWarehouse : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public int ComponentId
        {
            get { return Convert.ToInt32(comboBoxComponent.SelectedValue); }
            set { comboBoxComponent.SelectedValue = value; }
        }

        public int Warehouse
        {
            get { return Convert.ToInt32(comboBoxWarehouse.SelectedValue); }
            set { comboBoxWarehouse.SelectedValue = value; }
        }

        public int Count
        {
            get { return Convert.ToInt32(textBoxCount.Text); }
            set { textBoxCount.Text = value.ToString(); }
        }

        private readonly WarehouseLogic storeHouseLogic;

        public FormAddComponentsToWarehouse(ComponentLogic logicComponent, WarehouseLogic logicWarehouse)
        {
            InitializeComponent();
            storeHouseLogic = logicWarehouse;

            List<ComponentViewModel> listComponents = logicComponent.Read(null);
            if (listComponents != null)
            {
                comboBoxComponent.DisplayMember = "ComponentName";
                comboBoxComponent.ValueMember = "Id";
                comboBoxComponent.DataSource = listComponents;
                comboBoxComponent.SelectedItem = null;
            }

            List<WarehouseViewModel> listWarehouses = logicWarehouse.Read(null);
            if (listWarehouses != null)
            {
                comboBoxWarehouse.DisplayMember = "WarehouseName";
                comboBoxWarehouse.ValueMember = "Id";
                comboBoxWarehouse.DataSource = listWarehouses;
                comboBoxWarehouse.SelectedItem = null;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Fill count", "Error",
               MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (comboBoxComponent.SelectedValue == null)
            {
                MessageBox.Show("Select component", "Error", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }

            if (comboBoxWarehouse.SelectedValue == null)
            {
                MessageBox.Show("Select warehouse", "Error", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }

            storeHouseLogic.AddComponent(new WarehouseAddComponentBindingModel
            {
                ComponentId = Convert.ToInt32(comboBoxComponent.SelectedValue),
                WarehouseId = Convert.ToInt32(comboBoxWarehouse.SelectedValue),
                Count = Convert.ToInt32(textBoxCount.Text)
            });

            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
