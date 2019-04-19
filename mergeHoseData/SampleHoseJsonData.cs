using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace mergeHoseData
{
	public class SampleHoseJsonData
	{
		public Delete delete { get; set; }
		public Created_at created_at { get; set; }

		public static SampleHoseJsonData ConvertToObj(string bjson)
		{
			try
			{
				//Dynamic
				var obj = (SampleHoseJsonData)Codeplex.Data.DynamicJson.Parse(bjson);
				
				return obj;
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}
	}

	public class Delete
	{
		public Status status { get; set; }
		public string timestamp_ms { get; set; }
	}

	public class Status
	{
		public long id { get; set; }
		public string id_str { get; set; }
		public long user_id { get; set; }
		public string user_id_str { get; set; }
	}


	public class Created_at
	{
		public string created_at { get; set; }
		public long id { get; set; }
		public string id_str { get; set; }
		public string text { get; set; }
		public string source { get; set; }
		public bool truncated { get; set; }
		public object in_reply_to_status_id { get; set; }
		public object in_reply_to_status_id_str { get; set; }
		public object in_reply_to_user_id { get; set; }
		public object in_reply_to_user_id_str { get; set; }
		public object in_reply_to_screen_name { get; set; }
		public User user { get; set; }
		public object geo { get; set; }
		public object coordinates { get; set; }
		public object place { get; set; }
		public object contributors { get; set; }
		public bool is_quote_status { get; set; }
		public int quote_count { get; set; }
		public int reply_count { get; set; }
		public int retweet_count { get; set; }
		public int favorite_count { get; set; }
		public Entities entities { get; set; }
		public bool favorited { get; set; }
		public bool retweeted { get; set; }
		public string filter_level { get; set; }
		public string lang { get; set; }
		public string timestamp_ms { get; set; }
	}

	public class User
	{
		public long id { get; set; }
		public string id_str { get; set; }
		public string name { get; set; }
		public string screen_name { get; set; }
		public string location { get; set; }
		public object url { get; set; }
		public string description { get; set; }
		public string translator_type { get; set; }
		public Derived derived { get; set; }
		public bool _protected { get; set; }
		public bool verified { get; set; }
		public int followers_count { get; set; }
		public int friends_count { get; set; }
		public int listed_count { get; set; }
		public int favourites_count { get; set; }
		public int statuses_count { get; set; }
		public string created_at { get; set; }
		public object utc_offset { get; set; }
		public object time_zone { get; set; }
		public bool geo_enabled { get; set; }
		public string lang { get; set; }
		public bool contributors_enabled { get; set; }
		public bool is_translator { get; set; }
		public string profile_background_color { get; set; }
		public string profile_background_image_url { get; set; }
		public string profile_background_image_url_https { get; set; }
		public bool profile_background_tile { get; set; }
		public string profile_link_color { get; set; }
		public string profile_sidebar_border_color { get; set; }
		public string profile_sidebar_fill_color { get; set; }
		public string profile_text_color { get; set; }
		public bool profile_use_background_image { get; set; }
		public string profile_image_url { get; set; }
		public string profile_image_url_https { get; set; }
		public string profile_banner_url { get; set; }
		public bool default_profile { get; set; }
		public bool default_profile_image { get; set; }
		public object following { get; set; }
		public object follow_request_sent { get; set; }
		public object notifications { get; set; }
	}

	public class Derived
	{
		public Location[] locations { get; set; }
	}

	public class Location
	{
		public string country { get; set; }
		public string country_code { get; set; }
		public string locality { get; set; }
		public string region { get; set; }
		public string full_name { get; set; }
		public Geo geo { get; set; }
	}

	public class Geo
	{
		public float[] coordinates { get; set; }
		public string type { get; set; }
	}

	public class Entities
	{
		public Hashtag[] hashtags { get; set; }
		public object[] urls { get; set; }
		public object[] user_mentions { get; set; }
		public object[] symbols { get; set; }
	}

	public class Hashtag
	{
		public string text { get; set; }
		public int[] indices { get; set; }
	}


}
