using System;
using System.IO;
using TreeSize.Model;

namespace TreeSize.ViewModel
{
	public class NodeBuilder
	{
		private const long DefaultSize = 0L;
		private Node _node;

		public Node Node => _node;

		public NodeBuilder(DriveInfo driveInfo)
		{
			if (driveInfo is null)
				throw new ArgumentNullException(nameof(driveInfo));

			InitDrive(driveInfo);
		}

		public NodeBuilder(DirectoryInfo directoryInfo)
		{
			if (directoryInfo is null)
				throw new ArgumentNullException(nameof(directoryInfo));

			InitDirectory(directoryInfo);
			_node.Nodes.Add(new Node {Name = "Loading..."});
		}

		public NodeBuilder(FileInfo fileInfo)
		{
			if (fileInfo is null)
				throw new ArgumentNullException(nameof(fileInfo));

			InitFile(fileInfo);
		}

		private void InitDirectory(DirectoryInfo directoryInfo)
		{
			_node = new Node
			{
				Name = directoryInfo.Name,
				FullName = directoryInfo.FullName,
				Size = DefaultSize,
				ImagePath = Path.GetFullPath(@"..\..\..\View\Images\folder.png")
			};
		}

		private void InitDrive(DriveInfo driveInfo)
		{
			_node = new Node
			{
				Name = driveInfo.Name,
				FullName = driveInfo.Name
			};
		}

		private void InitFile(FileInfo fileInfo)
		{
			_node = new Node
			{
				Name = fileInfo.Name,
				FullName = fileInfo.FullName,
				Size = fileInfo.Length,
				ImagePath = Path.GetFullPath(@"..\..\..\View\Images\file.png")
			};
		}
	}
}