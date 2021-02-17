using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace AvaloniaApp
{
	public class MessageBox : Window
	{
#pragma warning disable 0649, 0169
		private readonly TextBox textBoxContent;
#pragma warning restore 0649, 0169

		public MessageBox(string text)
		{
			this.InitializeComponent();
#if DEBUG
			this.AttachDevTools();
#endif
			this.InitControlFields();

			textBoxContent.Text=text;
		}

		private void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);
		}

		public static void Show(string text,Window owner)
		{
			new MessageBox(text).ShowDialog(owner);
		}
	}
}
