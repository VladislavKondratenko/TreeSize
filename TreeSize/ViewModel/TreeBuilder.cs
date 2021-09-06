using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using TreeSize.Model;

namespace TreeSize.ViewModel
{
	public static class TreeBuilder
	{
		public static ObservableCollection<Node> SetDirectory(Node node)
		{
			var directory = new DirectoryInfo(node.FullName);
			var nodes = new ObservableCollection<Node>();

			foreach (var subDir in directory.EnumerateDirectories())
				nodes.Add(new NodeBuilder(subDir).Node);

			foreach (var file in directory.EnumerateFiles())
				nodes.Add(new NodeBuilder(file).Node);

			return nodes;
		}

		public static ObservableCollection<Node> CountSize(ObservableCollection<Node> nodes,
			DirectoryInfo directory)
		{
			foreach (var node in nodes.Where(n => n.FullName == directory.FullName))
				CountSize(directory, node);

			return nodes;
		}

		private static void CountSize(DirectoryInfo directory, Node node)
		{
			try
			{
				foreach (var file in directory.EnumerateFiles())
					node.Size += file.Length;

				foreach (var subDir in directory.EnumerateDirectories())
					CountSize(subDir, node);
			}
			catch (UnauthorizedAccessException)
			{
				Debug.Print(nameof(CountSize) + ": " + directory.FullName);
			}
		}
	}
}