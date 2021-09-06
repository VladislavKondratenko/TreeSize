using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Data;

namespace TreeSize.ViewModel
{
	public class SizeConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is not long size)
				return "-";

			var sb = new StringBuilder(20);
			StrFormatByteSize(size, sb, 20);

			return sb.ToString();
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		[DllImport("Shlwapi.dll", CharSet = CharSet.Auto)]
		private static extern int StrFormatByteSize(
			long fileSize,
			[MarshalAs(UnmanagedType.LPWStr)] StringBuilder buffer,
			int bufferSize);
	}
}