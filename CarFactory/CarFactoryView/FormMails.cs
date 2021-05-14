using CarFactoryBusinessLogic.BusinessLogics;
using CarFactoryBusinessLogic.ViewModels;
using System;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace CarFactoryView
{
    public partial class FormMails : Form
    {
        private readonly MailLogic logic;

        private readonly int maxPage = 0;

        private readonly int mailsOnPage = 3;

        private int currentPage = 0;

        public FormMails(MailLogic mailLogic)
        {
            logic = mailLogic;
            if (mailsOnPage < 1) { mailsOnPage = 5; }
            maxPage = (logic.Read(null).Count() - 1) / mailsOnPage;
            InitializeComponent();
            if (maxPage != 0) { buttonNext.Enabled = true; }
        }

        private void FormMails_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            var list = logic.Read(null).Skip(currentPage * mailsOnPage).Take(mailsOnPage).ToList();
            try
            {
                if (list != null)
                {
                    var method = typeof(Program).GetMethod("ConfigGrid");
                    MethodInfo generic = method.MakeGenericMethod(typeof(MessageInfoViewModel));
                    generic.Invoke(this, new object[] { logic.Read(null), dataGridView });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            if ((currentPage + 1) <= maxPage)
            {
                currentPage++;
                buttonPrev.Enabled = true;
                if (maxPage <= currentPage) { buttonNext.Enabled = false; }
                LoadData();
            }
        }

        private void buttonPrev_Click(object sender, EventArgs e)
        {
            if ((currentPage - 1) >= 0)
            {
                currentPage--;
                buttonNext.Enabled = true;
                if (currentPage == 0) { buttonPrev.Enabled = false; }
                LoadData();
            }
        }
    }
}
