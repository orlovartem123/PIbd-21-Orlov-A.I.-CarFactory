using CarFactoryBusinessLogic.BindingModels;
using CarFactoryBusinessLogic.BusinessLogics;
using CarFactoryBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;

namespace CarFactoryView
{
    public partial class FormWarehouse : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }

        private readonly WarehouseLogic warehouseLogic;

        private int? id;

        private Dictionary<int, (string, int)> warehouseComponents;

        public FormWarehouse(WarehouseLogic warehouseLogic)
        {
            InitializeComponent();
            this.warehouseLogic = warehouseLogic;
        }

        private void FormWarehouse_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    WarehouseViewModel view = warehouseLogic.Read(new WarehouseBindingModel
                    {
                        Id = id.Value
                    })?[0];

                    if (view != null)
                    {
                        textBoxName.Text = view.WarehouseName;
                        textBoxManagerName.Text = view.ManagerFullName;
                        warehouseComponents = view.WarehouseComponents;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
            else
            {
                warehouseComponents = new Dictionary<int, (string, int)>();
            }
        }

        private void LoadData()
        {
            try
            {
                if (warehouseComponents != null)
                {
                    dataGridView.Rows.Clear();
                    foreach (var warehouseComponent in warehouseComponents)
                    {
                        dataGridView.Rows.Add(new object[] { warehouseComponent.Key, warehouseComponent.Value.Item1, warehouseComponent.Value.Item2 });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Fill name", "Error", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxManagerName.Text))
            {
                MessageBox.Show("Fill full managers name", "Error", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            try
            {
                warehouseLogic.CreateOrUpdate(new WarehouseBindingModel
                {
                    Id = id,
                    WarehouseName = textBoxName.Text,
                    ManagerFullName = textBoxManagerName.Text,
                    DateCreate = DateTime.Now,
                    WarehouseComponents = warehouseComponents
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

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
