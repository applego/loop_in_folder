using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace mergeHoseData
{
	public static class CommonMethod
	{
		public static void WriteSWResult(string shori, ref Stopwatch sw, bool isReset)
		{
			// 結果表示
			TimeSpan ts = sw.Elapsed;
			Console.WriteLine($"■{shori}にかかった時間:{ts}  {sw.ElapsedMilliseconds}");
			if(isReset)
				sw.Reset();
		}

		public static List<T> GetDistinct<T>(this IList<T> self)
		{
			Stopwatch sw = new Stopwatch();
			sw.Start();

			var uniqueList = new List<T>();
			var result = new List<T>();

			foreach (var n in self)
			{
				if (uniqueList.Contains(n))
				{
					result.Add(n);
				}
				else
				{
					uniqueList.Add(n);
				}
			}
			WriteSWResult("GetDistinct-List ver.", ref sw, true);
			return result;
		}

		public static HashSet<string> MergeHashSet(HashSet<string> hs1, HashSet<string> hs2)
		{
			HashSet<string> result = hs1;
			Console.WriteLine($"hs1.Count:{hs1.Count}");
			hs1.UnionWith(hs2);
			Console.WriteLine($"after hs1.UnionWith(hs2);:{hs1.Count}");

			return result;
		}
		/// <summary>
		/// Directory クラスに関する汎用関数を管理するクラス
		/// </summary>
		public static class DirectoryUtils
		{
			/// <summary>
			/// 指定したパスにディレクトリが存在しない場合
			/// すべてのディレクトリとサブディレクトリを作成します
			/// </summary>
			public static DirectoryInfo SafeCreateDirectory(string path)
			{
				if (Directory.Exists(path))
				{
					return null;
				}
				return Directory.CreateDirectory(path);
			}
		}
	}
}
