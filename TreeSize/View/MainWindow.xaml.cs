using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using TreeSize.Model;
using TreeSize.ViewModel;

namespace TreeSize.View
{
	public partial class MainWindow
	{
		private IList<Node> _drives;

		public MainWindow()
		{
			InitializeComponent();
			SetIcon();

			SetDrives();
			FillComboBox();
		}

		private void SetIcon()
		{
			var iconUri = new Uri(@"..\..\..\View\Images\treesize.ico", UriKind.RelativeOrAbsolute);
			Icon = BitmapFrame.Create(iconUri);
		}

		private void FillComboBox()
		{
			var drivesNames = _drives.Select(d => d.Name).ToList();

			foreach (var name in drivesNames)
				ComboBox.Items.Add(name);

			ComboBox.SelectedValue = drivesNames.First();
		}

		private void SetDrives()
		{
			_drives = DriveInfo.GetDrives()
								.Select(d => new NodeBuilder(d).Node)
								.ToList();
		}

		private void TreeViewItem_Expanded(object sender, RoutedEventArgs e)
		{
			if ((e.OriginalSource as TreeViewItem)?.DataContext is not Node node)
				return;

			node.Nodes.Clear();

			SetNodes(node);
		}

		private void ButtonGo_OnClick(object sender, RoutedEventArgs e)
		{
			var selectedDrive = _drives.First(d => d.Name == ComboBox.SelectedValue.ToString());

			SetNodes(selectedDrive);

			Root.ItemsSource = selectedDrive.Nodes;
		}

		private static void SetNodes(Node node)
		{
			try
			{
				node.Nodes = TreeBuilder.SetDirectory(node);

				RunToCountSize(node);
			}
			catch (Exception e)
			{
				Debug.Print(e.Message);
			}
		}

		private static void RunToCountSize(Node node)
		{
			Directory.EnumerateDirectories(node.FullName)
					.Select(s => new DirectoryInfo(s))
					.Select(d =>
						Task.Run(() => TreeBuilder.CountSize(node.Nodes, d)))
					.ToArray(); //This action is required for correct work
		}
	}
}