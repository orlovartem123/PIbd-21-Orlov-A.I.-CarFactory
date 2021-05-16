using CarFactoryBusinessLogic.BindingModels;
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

        private bool hasNext = false;

        private readonly int mailsOnPage = 2;

        private int currentPage = 0;

        public FormMails(MailLogic mailLogic)
        {
            logic = mailLogic;
            if (mailsOnPage < 1) { mailsOnPage = 5; }
            InitializeComponent();
        }

        private void FormMails_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var list = logic.Read(new MessageInfoBindingModel { ToSkip = currentPage * mailsOnPage, ToTake = mailsOnPage + 1 });
                var method = typeof(Program).GetMethod("ConfigGrid");
                MethodInfo generic = method.MakeGenericMethod(typeof(MessageInfoViewModel));
                hasNext = !(list.Count() <= mailsOnPage);
                if (hasNext)
                {
                    buttonNext.Text = "Next " + (currentPage + 2);
                    buttonNext.Enabled = true;
                }
                else
                {
                    buttonNext.Text = "Next";
                    buttonNext.Enabled = false;
                }
                generic.Invoke(this, new object[] { list.Take(mailsOnPage).ToList(), dataGridView });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            if (hasNext)
            {
                currentPage++;
                textBoxPage.Text = (currentPage + 1).ToString();
                buttonPrev.Enabled = true;
                buttonPrev.Text = "Prev " + (currentPage);
                LoadData();
            }
        }

        private void buttonPrev_Click(object sender, EventArgs e)
        {
            if ((currentPage - 1) >= 0)
            {
                currentPage--;
                textBoxPage.Text = (currentPage + 1).ToString();
                buttonNext.Enabled = true;
                buttonNext.Text = "Next " + (currentPage + 2);
                if (currentPage == 0)
                {
                    buttonPrev.Enabled = false;
                    buttonPrev.Text = "Prev";
                }
                else
                {
                    buttonPrev.Text = "Prev " + (currentPage);
                }
                LoadData();
            }
        }
    }
}
