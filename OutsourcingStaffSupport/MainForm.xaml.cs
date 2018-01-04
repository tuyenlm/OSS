using MaterialDesignThemes.Wpf;
using SharpConfig;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using static OutsourcingStaffSupport.MainWindow;

namespace OutsourcingStaffSupport
{
	/// <summary>
	/// Interaction logic for MainForm.xaml
	/// </summary>
	public partial class MainForm : UserControl
	{
		Dictionary<String, String> Code_comanyList = new Dictionary<string, string>();
		Dictionary<String, int> SaiItakuList = new Dictionary<String, int> { { "", 0 }, { "〇", 1 }, { "□", 2 } };
		Dictionary<String, int> KanrishaKubunList = new Dictionary<String, int> { { "", 0 }, { "〇", 1 }, { "◎", 2 }, { "○/◎", 3 } };
		public static String fileNameConfig = @"\CompanyDataOutput.env";
		List<Data> items = new List<Data>();
		Dictionary<string, string> fields = new Dictionary<string, string>();
		Dictionary<string, string> dataAdd;
		Data data;
		List<string> userSanka;
		string pathString;
		public MainForm()
		{
			InitializeComponent();
			fields.Add("txt_KigyouShikibetsuKoudo", "KigyouShikibetsuKoudo");
			fields.Add("cbb_KigyouShikibetsuName", "KigyouShikibetsuName");
			fields.Add("txt_BukkenChuumon", "BukkenChuumon");
			fields.Add("txt_BukkenBanu", "BukkenBanu");
			fields.Add("cbb_SaiItaku", "SaiItaku");
			fields.Add("txt_SaiItakuSaki", "SaiItakuSaki");
			fields.Add("cbb_KanrishaKubun", "KanrishaKubun");
			fields.Add("cbb_JuujiSha", "JuujiSha");
			fields.Add("dp_JuujiKaishi", "JuujiKaishi");
			fields.Add("dp_JuujiShuuryou", "JuujiShuuryou");
			fields.Add("dp_SeiyakuShoShutoku", "SeiyakuShoShutoku");
			fields.Add("dp_JouhouSekyuriti", "JouhouSekyuriti");
			fields.Add("dp_ShoukyoKakunin", "ShoukyoKakunin");
			fields.Add("dp_MeisaiKoushin", "MeisaiKoushin");
			fields.Add("txt_Bikou", "Bikou");
			fields.Add("cbb_SekininshaBu", "SekininshaBu");
			fields.Add("cbb_SekininSha", "SekininSha");
			fields.Add("cbb_SekininShaDenWa", "SekininShaDenWa");

		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{
			//cbb_KigyouShikibetsuName
			List<String> KigyouShikibetsuName = getDataForFileENV("Data", "dataComapy");
			for (int i = 0; i < KigyouShikibetsuName.Count; i++)
			{
				Code_comanyList.Add(KigyouShikibetsuName[i].Split('-')[0], KigyouShikibetsuName[i].Split('-')[1]);
				if (i == 0) txt_KigyouShikibetsuKoudo.Text = KigyouShikibetsuName[i].Split('-')[0];
				cbb_KigyouShikibetsuName.Items.Add(KigyouShikibetsuName[i].Split('-')[1]);
			}
			cbb_KigyouShikibetsuName.SelectedIndex = 0;

			//cbb_saiItaku
			if (SaiItakuList.Count > 0)
			{
				foreach (var item in SaiItakuList) cbb_SaiItaku.Items.Add(item.Key);
				cbb_SaiItaku.SelectedIndex = 0;
			}

			//cbb_SekininshaBu
			cbb_SekininshaBu.ItemsSource = getDataForFileENV("Setting", "cmbExecutivePost");

			//cbb_SekininSha
			cbb_SekininSha.ItemsSource = getDataForFileENV("Setting", "cmbExecutiveName");

			//cbb_SekininShaDenWa
			cbb_SekininShaDenWa.ItemsSource = getDataForFileENV("Setting", "cmbExecutiveTel");

			//ccb_JuujiSha
			cbb_JuujiSha.ItemsSource = getDataForFileENV("Setting", "cmbWorkerName");

			//ccb_KanrishaKubun
			if (KanrishaKubunList.Count > 0)
			{
				foreach (var item in KanrishaKubunList) cbb_KanrishaKubun.Items.Add(item.Key);
				cbb_KanrishaKubun.SelectedIndex = 0;
			}
			//ListBox lb_SankaMei
			lb_SankaMei.ItemsSource = getDataForFileENV("Setting", "cmbWorkerName");

			pathString = Environment.CurrentDirectory + @"\" + getDataForFileENV("Setting", "ExportPath")[0];
			lblTextPath.Text = pathString;
		}

		public static List<String> getDataForFileENV(string type, string v)
		{
			List<String> data = new List<string>();
			Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
			var fileENV = Configuration.LoadFromFile(Directory.GetCurrentDirectory() + fileNameConfig, Encoding.GetEncoding("shift-jis"));
			var setting = fileENV[type];
			foreach (var item in setting)
				if (item.Name.Equals(v))
					for (int i = 0; i < item.StringValue.Trim().Split(',').Length; i++)
						if (!String.IsNullOrEmpty(item.StringValue.Trim().Split(',')[i]))
							data.Add(item.StringValue.Trim().Split(',')[i]);
			return data;
		}

		public class Data
		{
			public string KigyouShikibetsuKoudo { get; set; }
			public string KigyouShikibetsuName { get; set; }
			public string BukkenChuumon { get; set; }
			public string BukkenBanu { get; set; }
			public string SaiItaku { get; set; }
			public string SaiItakuSaki { get; set; }
			public string JuujiShuuryou { get; set; }
			public string JuujiKaishi { get; set; }
			public string SeiyakuShoShutoku { get; set; }
			public string JuujiSha { get; set; }
			public string SekininShaDenWa { get; set; }
			public string KanrishaKubun { get; set; }
			public string JouhouSekyuriti { get; set; }
			public string ShoukyoKakunin { get; set; }
			public string MeisaiKoushin { get; set; }
			public string Bikou { get; set; }
			public string SekininshaBu { get; set; }
			public string SekininSha { get; set; }
		}

		private void cbb_KigyouShikibetsuName_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			changeKigyouShikibetsuName();
		}

		private void changeKigyouShikibetsuName()
		{
			foreach (var item in Code_comanyList) if (item.Value.Equals(cbb_KigyouShikibetsuName.SelectedValue)) txt_KigyouShikibetsuKoudo.Text = item.Key;
		}

		private void txt_BukkenBanu_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
		{
			Regex regex = new Regex("[^0-9]+");
			e.Handled = regex.IsMatch(e.Text);
		}

		private void cbb_saiItaku_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			//Console.WriteLine(SaiItakuList[cbb_SaiItaku.SelectedValue.ToString()]);
		}

		//Set value for SaiItaku form File .csv
		private void SetValue_SaiItaku()
		{
			cbb_SaiItaku.Text = "〇";
		}

		private void ckb_MatometeSanka_Click(object sender, RoutedEventArgs e)
		{
			if (ckb_MatometeSanka.IsChecked == true) lb_SankaMei.SelectAll();
			else lb_SankaMei.UnselectAll();
		}

		private void btnTsuika_Click(object sender, RoutedEventArgs e)
		{
			dataAdd = getData();
			if (userSanka.Count > 0)
				for (int i = 0; i < userSanka.Count; i++)
				{
					fucData();
					data.JuujiSha = userSanka[i];
				}
			else fucData();
			dataGridResult.ItemsSource = items;
			CollectionViewSource.GetDefaultView(dataGridResult.ItemsSource).Refresh();
			lb_SankaMei.UnselectAll();
			ckb_MatometeSanka.IsChecked = false;
		}

		private Dictionary<string, string> getData()
		{
			Dictionary<string, string> data = new Dictionary<string, string>();
			//Get value for All TextBox control
			foreach (TextBox item in FindVisualChildren<TextBox>(this)) if (item.Name.Contains("txt_")) data.Add(item.Name, item.Text);

			//Get value for all DatePicker control
			foreach (DatePicker item in FindVisualChildren<DatePicker>(this)) data.Add(item.Name, item.Text);

			//Get value for all ComboBox control
			foreach (ComboBox item in FindVisualChildren<ComboBox>(this)) data.Add(item.Name, item.Text);

			//Get value for all ListBox control
			userSanka = new List<string>();
			foreach (ListBox item in FindVisualChildren<ListBox>(this)) foreach (object element in item.SelectedItems) if (!String.IsNullOrEmpty(element.ToString())) userSanka.Add(element.ToString());
			return data;
		}

		public void fucData()
		{
			data = new Data();
			foreach (var itemD in dataAdd)
			{
				var txtSpy = itemD.Key.Split('_');
				String val = itemD.Value.ToString();
				data = buildData(txtSpy[1], data, val);
			}
			items.Add(data);
		}

		private Data buildData(string field, Data dataB, string value)
		{
			switch (field)
			{
				case "KigyouShikibetsuKoudo":
					dataB.KigyouShikibetsuKoudo = value;
					break;
				case "KigyouShikibetsuName":
					dataB.KigyouShikibetsuName = value;
					break;
				case "BukkenChuumon":
					dataB.BukkenChuumon = value;
					break;
				case "BukkenBanu":
					dataB.BukkenBanu = value;
					break;
				case "SaiItaku":
					dataB.SaiItaku = value;
					break;
				case "SaiItakuSaki":
					dataB.SaiItakuSaki = value;
					break;
				case "KanrishaKubun":
					dataB.KanrishaKubun = value;
					break;
				case "JuujiSha":
					dataB.JuujiSha = value;
					break;
				case "JuujiKaishi":
					dataB.JuujiKaishi = value;
					break;
				case "JuujiShuuryou":
					dataB.JuujiShuuryou = value;
					break;
				case "SeiyakuShoShutoku":
					dataB.SeiyakuShoShutoku = value;
					break;
				case "JouhouSekyuriti":
					dataB.JouhouSekyuriti = value;
					break;
				case "ShoukyoKakunin":
					dataB.ShoukyoKakunin = value;
					break;
				case "MeisaiKoushin":
					dataB.MeisaiKoushin = value;
					break;
				case "Bikou":
					dataB.Bikou = value;
					break;
				case "SekininshaBu":
					dataB.SekininshaBu = value;
					break;
				case "SekininSha":
					dataB.SekininSha = value;
					break;
				case "SekininShaDenWa":
					dataB.SekininShaDenWa = value;
					break;
				default:
					break;
			}
			return dataB;
		}

		public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
		{
			if (depObj != null)
				for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
				{
					DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
					if (child != null && child is T) yield return (T)child;
					foreach (T childOfChild in FindVisualChildren<T>(child)) yield return childOfChild;
				}
		}
		private void btnShuusei_Click(object sender, RoutedEventArgs e)
		{
			Dictionary<String, String> dataLocal = getData();
			IList items = dataGridResult.SelectedItems;
			Console.WriteLine(items.Count);
			if (items.Count >= 1)
			{
				foreach (object item2 in items)
				{
					foreach (var item in dataLocal)
					{
						if (items.Count > 1)
						{
							if (!item.Key.Equals("cbb_JuujiSha")) buildData(fields[item.Key], (item2 as Data), item.Value);
							else buildData(fields[item.Key], (item2 as Data), (item2 as Data).JuujiSha.ToString());
						}
						else buildData(fields[item.Key], (item2 as Data), item.Value);
						CollectionViewSource.GetDefaultView(dataGridResult.ItemsSource).Refresh();
					}
				}
			}
			else MessageBox.Show("オブジェクトを選択してください", "情報", MessageBoxButton.OK, MessageBoxImage.Information);
		}

		private void BtnOk_Click(object sender, RoutedEventArgs e)
		{
			Dictionary<String, String> dataLocal = getData();
			foreach (var item in items) foreach (var item2 in dataLocal) buildData(fields[item2.Key], item, item2.Value);
			CollectionViewSource.GetDefaultView(dataGridResult.ItemsSource).Refresh();
		}

		private void btnDelete_Click(object sender, RoutedEventArgs e)
		{
			items.Remove((Data)dataGridResult.SelectedItem);
			CollectionViewSource.GetDefaultView(dataGridResult.ItemsSource).Refresh();
			btnShuusei.IsEnabled = false;
		}

		private void dataGridResult_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			Data items = (Data)dataGridResult.SelectedItem;
			if (items != null)
			{
				btnShuusei.IsEnabled = true;
				txt_KigyouShikibetsuKoudo.Text = items.KigyouShikibetsuKoudo.ToString();
				cbb_KigyouShikibetsuName.Text = items.KigyouShikibetsuName.ToString();
				txt_BukkenChuumon.Text = items.BukkenChuumon.ToString();
				txt_BukkenBanu.Text = items.BukkenBanu.ToString();
				cbb_SaiItaku.Text = items.SaiItaku.ToString();
				txt_SaiItakuSaki.Text = items.SaiItakuSaki.ToString();
				cbb_KanrishaKubun.Text = items.KanrishaKubun.ToString();
				cbb_JuujiSha.Text = items.JuujiSha.ToString();
				dp_JuujiKaishi.Text = items.JuujiKaishi.ToString();
				dp_JuujiShuuryou.Text = items.JuujiShuuryou.ToString();
				dp_SeiyakuShoShutoku.Text = items.SeiyakuShoShutoku.ToString();
				dp_JouhouSekyuriti.Text = items.JouhouSekyuriti.ToString();
				dp_ShoukyoKakunin.Text = items.ShoukyoKakunin.ToString();
				dp_MeisaiKoushin.Text = items.MeisaiKoushin.ToString();
				txt_Bikou.Text = items.Bikou.ToString();
				cbb_SekininshaBu.Text = items.SekininshaBu.ToString();
				cbb_SekininSha.Text = items.SekininSha.ToString();
				cbb_SekininShaDenWa.Text = items.SekininShaDenWa.ToString();
			}
		}

		private void btnReset_Click(object sender, RoutedEventArgs e)
		{
			foreach (TextBox item in FindVisualChildren<TextBox>(this))
				if (item.Name.Equals("txt_BukkenBanu")) item.Text = "0";
				else item.Clear();

			//Get value for all DatePicker control
			foreach (DatePicker item in FindVisualChildren<DatePicker>(this))
				if (!item.Name.Equals("dp_ShoukyoKakunin"))
				{
					item.SelectedDate = null;
					item.SelectedDate = DateTime.Today;
				}
				else item.SelectedDate = null;

			foreach (ComboBox item in FindVisualChildren<ComboBox>(this))
				if (!item.Name.Equals("cbb_JuujiSha"))
				{
					item.SelectedIndex = 0;
					changeKigyouShikibetsuName();
				}
				else item.Text = "";
			lb_SankaMei.UnselectAll();
			ckb_MatometeSanka.IsChecked = false;
		}

		private void btnCSVExport_Click(object sender, RoutedEventArgs e)
		{
			Directory.CreateDirectory(pathString);

			if (Directory.Exists(pathString))
			{
				if (!String.IsNullOrEmpty(txt_BukkenChuumon.Text))
				{
					Dictionary<String, List<Data>> df = new Dictionary<String, List<Data>>();
					foreach (var item in items)
					{
						var BukkenChuumon = item.BukkenChuumon.ToString();
						List<Data> ss = new List<Data>();
						if (df.ContainsKey(BukkenChuumon))
						{
							List<Data> gg = df[BukkenChuumon];
							gg.Add(item);
							df[BukkenChuumon] = gg;
						}
						else
						{
							ss.Add(item);
							df.Add(BukkenChuumon, ss);
						}
					}

					foreach (var itemMain in df)
					{
						var csv = new StringBuilder();
						String filePath = pathString + @"\" + itemMain.Key + ".csv";
						foreach (var item in itemMain.Value)
						{
							var BukkenChuumon = item.BukkenChuumon.ToString();
							var KigyouShikibetsuKoudo = item.KigyouShikibetsuKoudo.ToString();
							var KigyouShikibetsuName = item.KigyouShikibetsuName.ToString();
							var BukkenBanu = item.BukkenBanu.ToString();
							var SaiItaku = SaiItakuList[item.SaiItaku.ToString()];
							var SaiItakuSaki = item.SaiItakuSaki.ToString();
							var JuujiShuuryou = item.JuujiShuuryou.ToString();
							var JuujiKaishi = item.JuujiKaishi.ToString();
							var SeiyakuShoShutoku = item.SeiyakuShoShutoku.ToString();
							var JuujiSha = item.JuujiSha.ToString();
							var SekininShaDenWa = item.SekininShaDenWa.ToString();
							var KanrishaKubun = KanrishaKubunList[item.KanrishaKubun.ToString()];
							var JouhouSekyuriti = item.JouhouSekyuriti.ToString();
							var ShoukyoKakunin = item.ShoukyoKakunin.ToString();
							var MeisaiKoushin = item.MeisaiKoushin.ToString();
							var Bikou = item.Bikou.ToString();
							var SekininshaBu = item.SekininshaBu.ToString();
							var SekininSha = item.SekininSha.ToString();
							var newLine = string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\",\"{8}\",\"{9}\",\"{10}\",\"{11}\",\"{12}\",\"{13}\",\"{14}\",\"{15}\",\"{16}\",\"{17}\"",
							KigyouShikibetsuKoudo, KigyouShikibetsuName, BukkenChuumon, BukkenBanu, SaiItaku, SaiItakuSaki, KanrishaKubun, JuujiSha, JuujiKaishi, JuujiShuuryou, SeiyakuShoShutoku, JouhouSekyuriti, ShoukyoKakunin, MeisaiKoushin, Bikou, SekininshaBu, SekininSha, SekininShaDenWa);
							csv.AppendLine(newLine);
						}
						File.WriteAllText(filePath, csv.ToString());
					}
				}
				else
				{
					MessageBox.Show("物件注文番号を入力してください。", "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
				}
			}

		}

		private void btnCSVImport_Click(object sender, RoutedEventArgs e)
		{
			Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog
			{
				DefaultExt = ".csv",
				Filter = "CSV Files (*.csv)|*.csv"
			};
			Nullable<bool> result = dlg.ShowDialog();
			if (result == true)
			{
				string filename = dlg.FileName;
				string[] lines = File.ReadAllLines(System.IO.Path.ChangeExtension(filename, ".csv"));
				for (int i = 0; i < lines.Length; i++)
				{
					Data ba = new Data();
					string[] data = lines[i].Split(',');
					Dictionary<int, string> ls = new Dictionary<int, string>();
					for (int j = 0; j < data.Length; j++) ls.Add(j, data[j].Replace("\"", ""));
					ba.KigyouShikibetsuKoudo = ls[0];
					ba.KigyouShikibetsuName = ls[1];
					ba.BukkenChuumon = ls[2];
					ba.BukkenBanu = ((int.TryParse(ls[3], out int myInt)) ? ls[3] : "0");
					ba.SaiItaku = SaiItakuList.FirstOrDefault(x => x.Value == Convert.ToInt32(ls[4])).Key.ToString();
					ba.SaiItakuSaki = ls[5];
					ba.KanrishaKubun = KanrishaKubunList.FirstOrDefault(x => x.Value == Convert.ToInt32(ls[6])).Key;
					ba.JuujiSha = ls[7];
					ba.JuujiKaishi = ls[8];
					ba.JuujiShuuryou = ls[9];
					ba.SeiyakuShoShutoku = ls[10];
					ba.JouhouSekyuriti = ls[11];
					ba.ShoukyoKakunin = ls[12];
					ba.MeisaiKoushin = ls[13];
					ba.Bikou = ls[14];
					ba.SekininshaBu = ls[15];
					ba.SekininSha = ls[16];
					ba.SekininShaDenWa = ls[17];
					items.Add(ba);
				}
				dataGridResult.ItemsSource = items;
				CollectionViewSource.GetDefaultView(dataGridResult.ItemsSource).Refresh();
			}
		}

	}
}
