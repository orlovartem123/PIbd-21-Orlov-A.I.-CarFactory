using CarFactoryBusinessLogic.BindingModels;
using CarFactoryBusinessLogic.BusinessLogics;
using System;
using System.Windows.Forms;
using Unity;

namespace CarFactoryView
{
	public partial class FormComponent : Form
	{

		[Dependency]
		public new IUnityContainer Container { get; set; }

		public int Id { set { id = value; } }
		private readonly ComponentLogic logic;
		private int? id;


		public FormComponent(ComponentLogic logic)
		{
			InitializeComponent();
			this.logic = logic;
		}

		private void FormComponent_Load(object sender, EventArgs e)
		{
			if (id.HasValue)
			{
				try
				{
					var view = logic.Read(new ComponentBindingModel { Id = id })?[0];
					if (view != null)
					{
						textBoxName.Text = view.ComponentName;
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK,
				   MessageBoxIcon.Error);
				}
			}

		}

		private void ButtonSave_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(textBoxName.Text))
			{
				MessageBox.Show("Fill name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			try
			{
				logic.CreateOrUpdate(new ComponentBindingModel
				{
					Id = id,
					ComponentName = textBoxName.Text
				});
				MessageBox.Show("Saved", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
				DialogResult = DialogResult.OK;
				Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void ButtonCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

	}
}

