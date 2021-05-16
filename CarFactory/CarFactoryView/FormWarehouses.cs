using CarFactoryBusinessLogic.BindingModels;
using CarFactoryBusinessLogic.BusinessLogics;
using CarFactoryBusinessLogic.ViewModels;
using System;
using System.Reflection;
using System.Windows.Forms;
using Unity;

namespace CarFactoryView
{
    public partial class FormWarehouses : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly WarehouseLogic warehouseLogic;

        public FormWarehouses(WarehouseLogic warehouseLogic)
        {
            InitializeComponent();
            this.warehouseLogic = warehouseLogic;
        }

        private void FormWarehouses_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var method = typeof(Program).GetMethod("ConfigGrid");
                MethodInfo generic = method.MakeGenericMethod(typeof(WarehouseViewModel));
                generic.Invoke(this, new object[] { warehouseLogic.Read(null), dataGridView });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormWarehouse>();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void ButtonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var form = Container.Resolve<FormWarehouse>();
                form.Id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private void ButtonDel_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Delete Entry", "Question", MessageBoxButtons.YesNo,
               MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int id =Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                    try
                    {
                        warehouseLogic.Delete(new WarehouseBindingModel { Id = id });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK,
                       MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }

        private void ButtonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
