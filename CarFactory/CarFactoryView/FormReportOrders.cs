using Microsoft.Reporting.WinForms;
using CarFactoryBusinessLogic.BindingModels;
using CarFactoryBusinessLogic.BusinessLogics;
using System;
using System.Windows.Forms;
using Unity;

namespace CarFactoryView
{
    public partial class FormReportOrders : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly ReportLogic logic;

        public FormReportOrders(ReportLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }

        private void FormClientOrders_Load(object sender, EventArgs e)
        {
            reportViewer.RefreshReport();
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            if (dateTimePickerFrom.Value.Date >= dateTimePickerTo.Value.Date)
            {
                MessageBox.Show("Start date must be less than the end date",
               "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                ReportParameter parameter = new ReportParameter("ReportParameterPeriod",
                "from " +
               dateTimePickerFrom.Value.ToShortDateString() +
                " to " +
               dateTimePickerTo.Value.ToShortDateString());
                reportViewer.LocalReport.SetParameters(parameter);

                var dataSource = logic.GetOrders(new ReportBindingModel
                {
                    DateFrom = dateTimePickerFrom.Value,
                    DateTo = dateTimePickerTo.Value
                });
                ReportDataSource source = new ReportDataSource("DataSetOrders",dataSource);
                reportViewer.LocalReport.DataSources.Add(source);
                reportViewer.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }

        [Obsolete]
        private void buttonToPdf_Click(object sender, EventArgs e)
        {
            if (dateTimePickerFrom.Value.Date >= dateTimePickerTo.Value.Date)
            {
                MessageBox.Show("Start date must be less than the end date",
               "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            using (var dialog = new SaveFileDialog { Filter = "pdf|*.pdf" })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        logic.SaveOrdersToPdfFile(new ReportBindingModel
                        {
                            FileName = dialog.FileName,
                            DateFrom = dateTimePickerFrom.Value,
                            DateTo = dateTimePickerTo.Value
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
    }
}