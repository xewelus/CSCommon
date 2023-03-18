using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using CommonWpf.Classes.UI;

namespace CommonWpf.Controls
{
	/// <summary>
	/// Button with image and text.
	/// </summary>
	public partial class ButtonWithText
	{
		public ButtonWithText()
		{
			this.InitializeComponent();
			this.DataContext = this;
		}

		public static readonly DependencyProperty LabelMarginProperty = DependencyProperty.Register(
			"LabelMargin",
			typeof(Thickness),
			typeof(ButtonWithText),
			new FrameworkPropertyMetadata(new Thickness(0, 0.0, 0.0, 0.0), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

		public Thickness LabelMargin
		{
			get
			{
				return (Thickness)this.GetValue(LabelMarginProperty);
			}
			set
			{
				this.SetValue(LabelMarginProperty, value);
			}
		}

		public static readonly DependencyProperty LabelAlignmentProperty = DependencyProperty.Register(
			"LabelAlignment",
			typeof(HorizontalAlignment),
			typeof(ButtonWithText),
			new FrameworkPropertyMetadata(HorizontalAlignment.Center, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

		public HorizontalAlignment LabelAlignment
		{
			get
			{
				return (HorizontalAlignment)this.GetValue(LabelAlignmentProperty);
			}
			set
			{
				this.SetValue(LabelAlignmentProperty, value);
			}
		}

		public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
			"Text",
			typeof(string),
			typeof(ButtonWithText),
			new FrameworkPropertyMetadata("Text", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

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

		public static readonly DependencyProperty ImageProperty = DependencyProperty.Register(
			"Image",
			typeof(ImageSource),
			typeof(ButtonWithText),
			new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

		[TypeConverter(typeof(ImageSourceConverter))]
		public ImageSource Image
		{
			get
			{
				return (ImageSource)this.GetValue(ImageProperty);
			}
			set
			{
				this.SetValue(ImageProperty, value);
			}
		}

		public static readonly DependencyProperty ButtonBackgroundProperty = DependencyProperty.Register(
			"ButtonBackground",
			typeof(Brush),
			typeof(ButtonWithText),
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

		public static readonly RoutedEvent ClickEvent = EventManager.RegisterRoutedEvent(
			name: "Click", 
			routingStrategy: RoutingStrategy.Bubble, 
			handlerType: typeof(RoutedEventHandler),
			ownerType: typeof(ButtonWithText));

		[Category("Behavior")]
		public event RoutedEventHandler Click
		{
			add
			{
				this.AddHandler(ClickEvent, value);
			}
			remove
			{
				this.RemoveHandler(ClickEvent, value);
			}
		}

		private void ButtonClick(object sender, RoutedEventArgs e)
		{
			try
			{
				this.RaiseEvent(new RoutedEventArgs(ClickEvent));
			}
			catch (Exception ex)
			{
				ExceptionHandler.Catch(ex);
			}
		}
	}
}
