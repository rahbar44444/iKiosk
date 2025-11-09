using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iKiosk.UI.Extensions
{
	public static class EnumerableExtensions
	{
		/// <summary>
		/// Converts an IEnumerable<T> to an ObservableCollection<T>.
		/// </summary>
		/// <typeparam name="T">The type of elements in the collection.</typeparam>
		/// <param name="source">The IEnumerable to convert.</param>
		/// <returns>An ObservableCollection containing the same elements as the source.</returns>
		public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> source)
		{
			return source != null
				? new ObservableCollection<T>(source)
				: new ObservableCollection<T>();
		}
	}
}
