using System;
namespace OutsourcingStaffSupport
{

	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow
	{
		public MainWindow()
		{
			InitializeComponent();
			this.contentControl.Content = new MainForm();
		}
		private void btnMain_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			this.contentControl.Content = new MainForm();
		}

		private void btnSettings_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			this.contentControl.Content = new Settings();
			string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
			//Console.WriteLine(path);
		}
	}
}