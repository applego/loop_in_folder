using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;

namespace mergeHoseData
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				#region 設定ファイル読込
				//指定したキーの値を取得
				//見つからないときはnullを返す
				Console.WriteLine(System.Configuration.ConfigurationManager.AppSettings["Comment"]);

				//すべてのキーとその値を取得
				foreach (string key in System.Configuration.ConfigurationManager.AppSettings.AllKeys)
				{
					Console.WriteLine("{0} : {1}",
						key, System.Configuration.ConfigurationManager.AppSettings[key]);
				}
				#endregion

				string resultDir = $"{System.Configuration.ConfigurationManager.AppSettings["Directory_result"]}{DateTime.Now.ToString("yyyyMMdd")}";

				#region Merge
				if (System.Configuration.ConfigurationManager.AppSettings["isToMerge"] == "true")
				{
					//ディレクトリ1のファイルを取得
					string mydi1 = System.Configuration.ConfigurationManager.AppSettings["Directory1"];
					DirectoryInfo di1 = new DirectoryInfo(mydi1);
					List<FileInfo> files1 = di1.EnumerateFiles("*", SearchOption.AllDirectories).ToList();

					//ディレクトリ2のファイルを取得
					string mydi2 = System.Configuration.ConfigurationManager.AppSettings["Directory2"];
					DirectoryInfo di2 = new DirectoryInfo(mydi2);
					List<FileInfo> files2 = di2.EnumerateFiles("*", SearchOption.AllDirectories).ToList();

					//重複行を消して、同じ名前のファイルをマージ
					//マージしたデータをresultDirに出力
					files1.ForEach(file1 =>
					{
						FileInfo file2_same_name = files2.FirstOrDefault(f2 => f2.Name == file1.Name);
						if (file2_same_name != null)
						{
							var lst_str1 = GetLineList(file1);
							var lst_str2 = GetLineList(file2_same_name);

							//var lst_uniq1 = CommonMethod.GetDistinct(lst_str1);
							//var lst_uniq2 = CommonMethod.GetDistinct(lst_str2);

							HashSet<string> hashset1 = new HashSet<string>(lst_str1);
							HashSet<string> hashset2 = new HashSet<string>(lst_str2);

							HashSet<string> mergeHashset = CommonMethod.MergeHashSet(hashset1, hashset2);

							CommonMethod.DirectoryUtils.SafeCreateDirectory(resultDir);
							using (StreamWriter sw = new StreamWriter($"{resultDir}\\{file2_same_name.Name}", false, Encoding.UTF8))
							{
								foreach (var mhs in mergeHashset)
								{
									sw.WriteLine(mhs);
								}
							}
						}
					});
				}
				#endregion

				#region Zip
				if (System.Configuration.ConfigurationManager.AppSettings["isToZip"] == "true")
				{
					//resultDirのファイルを取得
					DirectoryInfo diRes = new DirectoryInfo(resultDir);
					List<FileInfo> filesRes = diRes.EnumerateFiles("*", SearchOption.AllDirectories).ToList();
					filesRes.ForEach(rf =>
					{
						//System.IO.Compression.ZipFile.CreateFromDirectory(rf.FullName, $"{rf.FullName.Replace("json","zip")}");
						using (var z = ZipFile.Open(rf.FullName.Replace("json", "zip"),
										ZipArchiveMode.Update))
						{
							z.CreateEntryFromFile(
							  rf.FullName, rf.Name, CompressionLevel.Optimal);
							//z.CreateEntryFromFile(
							//  @"C:\Test\b.txt", "b.txt", CompressionLevel.Optimal);
						}
					});
				}
				#endregion

				#region
				//。。。
				////非同期
				//var task = GetJsonListAsync(files);
				////task.Wait();
				//var results_async = task.Result;

				//同期
				//var results = GetJsonList(files1);
				#endregion

			}
			catch (Exception ex)
			{

			}
		}

		public static List<string> GetLineList(FileInfo f)
		{
			try
			{
				#region 同期
				List<string> lst_json = new List<string>();

				int counter = 0;
				using (StreamReader r = new StreamReader(f.FullName,Encoding.UTF8))
				{
					string line;
					while ((line = r.ReadLine()) != null)
					{
						lst_json.Add(line);
						counter++;
					}
				}

				return lst_json;

				#endregion 同期
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		public static List<SampleHoseJsonData> GetJsonList(List<string> lst_json)
		{
			try
			{
				#region 同期
				List<SampleHoseJsonData> lst_sampleHoseJsonDatas = new List<SampleHoseJsonData>();

				int counter = 0;
				lst_json.ForEach(line =>
				{
					lst_sampleHoseJsonDatas.Add(SampleHoseJsonData.ConvertToObj(line));
					counter++;
				});

				return lst_sampleHoseJsonDatas;

				#endregion 同期
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static async Task<List<string>> GetJsonListAsync(List<FileInfo> files)
		{
			try
			{
				#region 非同期
				Stopwatch sw = new Stopwatch();
				sw.Start();
				List<string> lst_json_async = new List<string>();

				int numTotalfile = files.Count();
				const int numThreads = 1;
				int numRepeat = numThreads;
				int numLeavefile = numTotalfile;
				for (int fn = 0; fn <= numTotalfile; fn += numThreads)
				{
					Task<string>[] json_read_Tasks;
					if (numLeavefile <= numThreads)
					{
						json_read_Tasks = new Task<string>[numLeavefile];
						numRepeat = numLeavefile;
					}
					else
					{
						json_read_Tasks = new Task<string>[numThreads];
						numLeavefile -= numThreads;
					}

					for (int i = 0; i < numRepeat; i++)
					{
						if ((fn + i) > numTotalfile)
							break;
						using (StreamReader r = new StreamReader(files[fn + i].FullName))
						{
							//Task<string> json_async = ReadToEndAsync(r);
							json_read_Tasks[i] = ReadToEndAsync(r);
						}
					}
					var results = await Task.WhenAll(json_read_Tasks);
					foreach (var r in results)
					{
						lst_json_async.Add(r);
					}
				}
				sw.Stop();
				CommonMethod.WriteSWResult("非同期",ref sw,true);


				return lst_json_async;

				#endregion 非同期
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		static async Task<string> ReadToEndAsync(StreamReader r)
		{
			return await r.ReadToEndAsync();
		}
	}
}
