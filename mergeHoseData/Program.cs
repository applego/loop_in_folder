using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace mergeHoseData
{
	class Program
	{
		static void Main(string[] args)
		{
			//指定したキーの値を取得
			//見つからないときはnullを返す
			Console.WriteLine(System.Configuration.ConfigurationManager.AppSettings["Comment"]);

			//すべてのキーとその値を取得
			foreach (string key in System.Configuration.ConfigurationManager.AppSettings.AllKeys)
			{
				Console.WriteLine("{0} : {1}",
					key, System.Configuration.ConfigurationManager.AppSettings[key]);
			}
		}
	}
}
