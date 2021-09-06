using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TreeSize.Model
{
	public class Node : INotifyPropertyChanged
	{
		private long _size;
		private ObservableCollection<Node> _nodes;

		public long Size
		{
			get => _size;
			set
			{
				if (value == _size)
					return;

				_size = value;
				OnPropertyChanged(nameof(Size));
			}
		}

		public ObservableCollection<Node> Nodes
		{
			get => _nodes;
			set
			{
				if (value == _nodes)
					return;

				_nodes = value;
				OnPropertyChanged(nameof(Nodes));
			}
		}

		public string FullName { get; init; }

		public string ImagePath { get; init; }

		public string Name { get; init; }

		public Node()
		{
			_nodes = new ObservableCollection<Node>();
		}

		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}