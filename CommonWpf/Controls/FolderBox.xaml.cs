using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CommonWpf.Controls
{
	/// <summary>
	/// Control for choose folder with TextBox and selecting button.
	/// </summary>
	public partial class FolderBox : UserControl
	{
		public FolderBox()
		{
			this.InitializeComponent();
			this.DataContext = this;
		}

		public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
			"Text",
			typeof(string),
			typeof(FolderBox),
			new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

		public string Text
		{
			get
			{
				return (string)this.GetValue(TextProperty);
			}
			set
			{
				this.SetValue(TextProperty, value);
			}
		}

		public static readonly DependencyProperty ButtonBackgroundProperty = DependencyProperty.Register(
			"ButtonBackground",
			typeof(Brush),
			typeof(FolderBox),
			new FrameworkPropertyMetadata(SystemColors.ControlBrush, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

		public Brush ButtonBackground
		{
			get
			{
				return (Brush)this.GetValue(ButtonBackgroundProperty);
			}
			set
			{
				this.SetValue(ButtonBackgroundProperty, value);
			}
		}
	}
}
