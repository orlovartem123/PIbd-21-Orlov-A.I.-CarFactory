namespace CarFactoryView
{
    partial class FormReportOrdersByDates
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.ReportOrderByDatesViewModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.buttonToPdf = new System.Windows.Forms.Button();
            this.reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.ReportOrderByDatesViewModelBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // ReportOrderByDatesViewModelBindingSource
            // 
            this.ReportOrderByDatesViewModelBindingSource.DataSource = typeof(CarFactoryBusinessLogic.ViewModels.ReportOrderByDatesViewModel);
            // 
            // buttonToPdf
            // 
            this.buttonToPdf.Location = new System.Drawing.Point(994, 8);
            this.buttonToPdf.Name = "buttonToPdf";
            this.buttonToPdf.Size = new System.Drawing.Size(180, 43);
            this.buttonToPdf.TabIndex = 5;
            this.buttonToPdf.Text = "To Pdf";
            this.buttonToPdf.UseVisualStyleBackColor = true;
            this.buttonToPdf.Click += new System.EventHandler(this.buttonToPdf_Click);
            // 
            // reportViewer
            // 
            this.reportViewer.Dock = System.Windows.Forms.DockStyle.Left;
            reportDataSource1.Name = "DataSetOrdersByDates";
            reportDataSource1.Value = this.ReportOrderByDatesViewModelBindingSource;
            this.reportViewer.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer.LocalReport.ReportEmbeddedResource = "CarFactoryView.ReportOrdersByDates.rdlc";
            this.reportViewer.Location = new System.Drawing.Point(0, 0);
            this.reportViewer.Name = "reportViewer";
            this.reportViewer.ServerReport.BearerToken = null;
            this.reportViewer.Size = new System.Drawing.Size(988, 567);
            this.reportViewer.TabIndex = 6;
            // 
            // FormReportOrdersByDates
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1179, 567);
            this.Controls.Add(this.reportViewer);
            this.Controls.Add(this.buttonToPdf);
            this.Name = "FormReportOrdersByDates";
            this.Text = "Client Order";
            this.Load += new System.EventHandler(this.FormClientOrders_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ReportOrderByDatesViewModelBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonToPdf;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer;
        private System.Windows.Forms.BindingSource ReportOrderByDatesViewModelBindingSource;
    }
}