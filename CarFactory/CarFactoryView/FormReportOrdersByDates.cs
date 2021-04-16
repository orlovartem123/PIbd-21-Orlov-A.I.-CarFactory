using CarFactoryBusinessLogic.BusinessLogics;
using CarFactoryBusinessLogic.BindingModels;
using Microsoft.Reporting.WinForms;
using System;
using System.Windows.Forms;
using Unity;

namespace CarFactoryView
{
    public partial class FormReportOrdersByDates : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly ReportLogic logic;

        public FormReportOrdersByDates(ReportLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }

        private void FormClientOrders_Load(object sender, EventArgs e)
        {
            reportViewer.RefreshReport();
        }

        [Obsolete]
        private void buttonToPdf_Click(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog { Filter = "pdf|*.pdf" })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        logic.SaveOrdersByDatesToPdfFile(new ReportBindingModel
                        {
                            FileName = dialog.FileName
                        });
                        MessageBox.Show("Done", "Success", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            try
            {
                var dataSource = logic.GetOrdersByDates();
                ReportDataSource source = new ReportDataSource("DataSetOrdersByDates", dataSource);
                reportViewer.LocalReport.DataSources.Add(source);
                reportViewer.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }
    }
}
