using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using CommonWpf.Classes.UI;
using UserControl = System.Windows.Controls.UserControl;

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

		public static readonly RoutedEvent TextChangedEvent = EventManager.RegisterRoutedEvent(
			"TextChanged",
			RoutingStrategy.Bubble,
			typeof(RoutedEventHandler),
			typeof(FolderBox)
		);

		public event RoutedEventHandler TextChanged
		{
			add
			{
				this.AddHandler(TextChangedEvent, value);
			}
			remove
			{
				this.RemoveHandler(TextChangedEvent, value);
			}
		}

		protected virtual void OnTextChanged()
		{
			RoutedEventArgs args = new RoutedEventArgs(TextChangedEvent);
			this.RaiseEvent(args);
		}

		private void Button_OnClick(object sender, RoutedEventArgs e)
		{
			try
			{
				using (FolderBrowserDialog dlg = new FolderBrowserDialog())
				{
					dlg.SelectedPath = this.Text;
					if (dlg.ShowDialog() == DialogResult.OK)
					{
						this.Text = dlg.SelectedPath;
					}
				}
			}
			catch (Exception ex)
			{
				ExceptionHandler.Catch(ex);
			}
		}

		private void FolderTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
		{
			try
			{
				this.OnTextChanged();
			}
			catch (Exception ex)
			{
				ExceptionHandler.Catch(ex);
			}
		}
	}
}
