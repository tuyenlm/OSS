using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.IO;
using SharpConfig;
using System.Windows.Controls.Primitives;
using System.Data;
using System.Collections;

namespace OutsourcingStaffSupport
{
	/// <summary>
	/// Interaction logic for MyContentTest.xaml
	/// </summary>
	public partial class Settings : UserControl
	{
		public static String fileNameConfig = @"\CompanyDataOutput.env";
		DataGrid dataGrid;
		public Settings()
		{
			InitializeComponent();
			txt_Path.Text = Environment.CurrentDirectory + @"\" + MainForm.getDataForFileENV("Setting", "ExportPath")[0];
			String type = "Setting";
			String v = "cmbWorkerName";
			List<String> data = new List<string>();
			Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
			var fileENV = Configuration.LoadFromFile(Directory.GetCurrentDirectory() + fileNameConfig, Encoding.GetEncoding("shift-jis"));
			var setting = fileENV[type];

			foreach (var item in setting)
			{
				Build(item.PreComment, item.RawValue, item.Name);
				if (item.Name.Equals(v))
					for (int i = 0; i < item.StringValue.Trim().Split(',').Length; i++)
						if (!String.IsNullOrEmpty(item.StringValue.Trim().Split(',')[i]))
						{
							data.Add(item.StringValue.Trim().Split(',')[i]);
							Console.WriteLine("---->" + item.StringValue.Trim().Split(',')[i]);
						}
			}


		}

		private void btnBrowseFolderName_Click(object sender, RoutedEventArgs e)
		{
			var dialog = new CommonOpenFileDialog();
			dialog.IsFolderPicker = true;
			CommonFileDialogResult result = dialog.ShowDialog();
			if (result == CommonFileDialogResult.Ok) txt_Path.Text = dialog.FileName;
		}
		private void Build(String text, String arrayText, String dataGridName)
		{
			Console.WriteLine("arrayText " + arrayText);
			StackPanel spMain = new StackPanel
			{
				Orientation = Orientation.Horizontal,
				Margin = new Thickness(20, 10, 0, 0)
			};
			TextBlock lblText = new TextBlock
			{
				VerticalAlignment = VerticalAlignment.Center,
				Margin = new Thickness(0, 30, 10, 0),
				Text = text
			};
			TextBox tbText = new TextBox
			{
				Name = "txt_" + dataGridName,
				Width = 300,
				HorizontalAlignment = HorizontalAlignment.Left,
				BorderBrush = Brushes.White,
				BorderThickness = new Thickness(1),
				Background = Brushes.LightGray,
				Foreground = Brushes.Black,
				Margin = new Thickness(0, 0, 10, 0),
			};
			Button btnAdd = new Button
			{
				Name = "btn_" + dataGridName,
				Content = "追加",
				HorizontalAlignment = HorizontalAlignment.Left,
				VerticalAlignment = VerticalAlignment.Center,
				Tag = dataGridName,

			};
			btnAdd.AddHandler(Button.ClickEvent, new RoutedEventHandler(btnAdd_Click));
			StackPanel spChild = new StackPanel
			{
				Orientation = Orientation.Horizontal,
				Margin = new Thickness(0, 0, 0, 10),
				HorizontalAlignment = HorizontalAlignment.Left,
				VerticalAlignment = VerticalAlignment.Center
			};
			spChild.Children.Add(tbText);
			spChild.Children.Add(btnAdd);
			dataGrid = new DataGrid
			{
				Name = dataGridName,
				BorderBrush = Brushes.Green,
				BorderThickness = new Thickness(1),
				HorizontalAlignment = HorizontalAlignment.Left,
				MaxHeight = 300,
				CanUserAddRows = false,
				AutoGenerateColumns = false

			};
			DataGridTextColumn col1 = new DataGridTextColumn();
			var ab = arrayText.Trim().Split(',');
			for (int i = 0; i < ab.Length; i++)
			{
				dataGrid.Items.Add(new Data() { Value = ab[i] });
			}
			col1.Header = "Value";
			col1.Binding = new Binding("Value");
			dataGrid.Columns.Add(col1);

			DataGridTemplateColumn col = new DataGridTemplateColumn();
			col.Header = "削除";
			DataTemplate dt = new DataTemplate();
			var sp = new FrameworkElementFactory(typeof(StackPanel));
			sp.SetValue(StackPanel.OrientationProperty, Orientation.Horizontal);

			var btnDelete = new FrameworkElementFactory(typeof(Button));
			btnDelete.SetValue(Button.TagProperty, dataGridName);
			btnDelete.SetValue(Button.ContentProperty, "削除");
			btnDelete.AddHandler(Button.ClickEvent, new RoutedEventHandler(btnDelete_Click));
			btnDelete.SetValue(Button.MarginProperty, new Thickness(0, 0, 10, 0));
			sp.AppendChild(btnDelete);

			dt.VisualTree = sp;
			col.CellTemplate = dt;
			dataGrid.Columns.Add(col);
			spSettings.Children.Add(lblText);
			spSettings.Children.Add(spChild);
			spSettings.Children.Add(dataGrid);

		}
		private void btnAdd_Click(object sender, RoutedEventArgs e)
		{
			Button btn = sender as Button;
			String val = "";
			foreach (DataGrid item in MainForm.FindVisualChildren<DataGrid>(this))
			{
				foreach (TextBox itemTextBox in MainForm.FindVisualChildren<TextBox>(this))
				{
					if (itemTextBox.Name.Equals("txt_" + btn.Tag))
					{
						if (item.Name.Equals(btn.Tag))
						{
							val = itemTextBox.Text;
							item.Items.Add(new Data() { Value = itemTextBox.Text });
							item.Items.Refresh();
							item.ScrollIntoView(item.Items.GetItemAt(item.Items.Count - 1));
							var rows = GetDataGridRows(item);

							foreach (DataGridRow r in rows)
							{
								//   DataRowView rv = (DataRowView)r.Item;
								foreach (DataGridColumn column in item.Columns)
								{
									if (column.GetCellContent(r) is TextBlock)
									{
										TextBlock cellContent = column.GetCellContent(r) as TextBlock;
										Console.WriteLine("cellContent.Text " + cellContent.Text);
										
									}
								}
							}
						}
					}
				}
			}
			//String pat = Environment.CurrentDirectory + @"\CompanyDataOutput.env";
			//System.IO.StreamReader file = new System.IO.StreamReader(pat, Encoding.GetEncoding("shift-jis"));
			//string line;
			//string text = "cmbWorkerName";
			//int counter = 0;
			//String txtkq ="";
			//while ((line = file.ReadLine()) != null)
			//{
			//	if (line.Contains(text))
			//	{
			//		Console.WriteLine(line);
			//		txtkq = line;
			//		break;
			//	}

			//	counter++;
			//}

			//Console.WriteLine("Line number: {0}", counter);

			//file.Close();
			//string text2 = File.ReadAllText(pat);
			//text2 = text2.Replace(txtkq, val);
			//File.WriteAllText(pat, text2);
		}
		public IEnumerable<DataGridRow> GetDataGridRows(DataGrid grid)
		{
			var itemsSource = grid.ItemsSource as IEnumerable;
			if (null == itemsSource) yield return null;
			foreach (var item in itemsSource)
			{
				var row = grid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
				if (null != row) yield return row;
			}
		}
		private void btnDelete_Click(object sender, RoutedEventArgs e)
		{
			Button delete = sender as Button;
			DataRowView drv = dataGrid.SelectedItem as DataRowView;
			foreach (DataGrid item in MainForm.FindVisualChildren<DataGrid>(this))
			{
				if (item.Name.Equals(delete.Tag))
				{
					item.Items.RemoveAt(item.SelectedIndex);
					item.Items.Refresh();
				}
			}
		}

		public class Data
		{
			public String Value { get; set; }
		}
	}
}
