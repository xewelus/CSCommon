using System.ComponentModel;
using System.Windows.Forms;

namespace CommonWinForms.Controls
{
	/// <summary>
	/// Button.
	/// In this class the additional properties, concerning to the interface,
	/// but not related with data are entered.
	/// </summary>
	public class SmartButton : System.Windows.Forms.Button
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Default constructor.
		/// </summary>
		public SmartButton()
		{
			// This call is required by the Windows.Forms Form Designer.
			this.InitializeComponent();

			// Set style Selectable
			this.SetStyle(ControlStyles.Selectable,this._Selectable);
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(this.components != null)
				{
					this.components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
		}
		#endregion

		#region fields

		private bool _Selectable;

		#endregion

		#region properties

		/// <summary>
		/// Defined whether the button can have focus.
		/// </summary>
		[DefaultValue(false)]
		public bool Selectable
		{
			get
			{
				return this._Selectable;
			}
			set
			{
				this._Selectable = value;
			}
		}

		#endregion
	}
}
