using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web;
using System.Collections.Generic;


public class Utma
{
	private int _visitCount = 0;
	public int VisitCount
	{
		get{return _visitCount;}
	}

	private DateTime _firstVisit = DateTime.Now;
	public DateTime FirstVisit
	{
		get{return _firstVisit;}
	}

	private DateTime _lastVisit = DateTime.Now;
	public DateTime LastVisit
	{
		get{return _lastVisit;}
	}

	public Utma(string init_str)
	{
		string[] src = init_str.Split('.');
		double tmp_dbl = 0;

		if(src.Length == 6)
		{
			//Формат: XXXX.DDDD.FFFF.PPPP.CCCC.N
			//0 XXXX - hash домена, полезной информации не содержит.
			//1 DDDD - уникальный ID пользователя в системе Google Analytics.

			//2 FFFF - дата первого посещения пользователем сайта в Unix формате (количество секунд, прошедших с первого января 1970-ого года).
			double.TryParse(src[2], out tmp_dbl);
			_firstVisit = ConvertFromUnixTimestamp(tmp_dbl);

			//3 PPPP - дата предыдущего посещения пользователем сайта в Unix формате.
			double.TryParse(src[3], out tmp_dbl);
			_lastVisit = ConvertFromUnixTimestamp(tmp_dbl);

			//4 CCCC - время начала текущего посещения (начало сессии) в Unix формате.
			//5 N - количество посещений сайта данным пользователем.			
			int.TryParse(src[5], out _visitCount);
		}
	}

	private DateTime ConvertFromUnixTimestamp(double timestamp)
	{
		DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
		return origin.AddSeconds(timestamp);
	}

	private double ConvertToUnixTimestamp(DateTime date)
	{
		DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
		TimeSpan diff = date - origin;
		return Math.Floor(diff.TotalSeconds);
	}

}


public class Utmb
{
	private int _viewedPagesCount = 0;
	public int ViewedPagesCount
	{
		get{return _viewedPagesCount;}
	}

	public Utmb(string init_str)
	{
		string[] src = init_str.Split('.');

		if(src.Length == 4)
		{
			//Формат: XXXX.P.10.CCCC
			//XXXX - hash домена.

			//0 P - количество страниц, просмотренных пользователям в течение текущей сессии.
			int.TryParse(src[0], out _viewedPagesCount);

			//1 10 - магическое число Google одинаковый на всех сайтах параметр, не меняющийся с течением времени. думаю, полезной информации не несет.
			//2 CCCC - время начала текущего посещения (начало сессии) в Unix формате (аналогично CCCC параметру _utma).
		}
	}
}


public class Utmz
{
	private int _visitFromForeignSiteCount = 0;
	public int VisitFromForeignSiteCount
	{
		get{return _visitFromForeignSiteCount;}
	}

	private int _foreignSiteCount = 0;
	public int ForeignSiteCount
	{
		get{return _foreignSiteCount;}
	}

	private string _searchFrom = "";
	public string SearchFrom
	{
		get{return _searchFrom;}
	}

	private string _searchKeyword = "";
	public string SearchKeyword
	{
		get{return _searchKeyword;}
	}

	public Utmz(string init_str)
	{
		string[] src = init_str.Split('.');

		if(src.Length == 6)
		{
			//Формат: XXXX.TTTT.V.S.utmcsr{source}|utmccn{campaign}|utmcmd{medium}|utmctr{keyword}
			//0 XXXX - hash домена.
			//1 TTTT - дата последнего обновления cookies в unix формате.
			
			//2 V - количество посещений пользователем сайта, совершенных по ссылкам с других ресурсов.
			int.TryParse(src[2], out _visitFromForeignSiteCount);

			//3 S - количество различных ресурсов, с которых пользователь попадал на сайт.
			int.TryParse(src[3], out _foreignSiteCount);

			//4.1 utmcsr - ресурс-поисковик, с которого пользователь попал на сайт.
			_searchFrom = ExtractParameters(src[4], "utmcsr");

			//4.2 utmccn - содержит информацию о компании из AdWords (или значение utm_campaign в запросе) или же сообщает, что пользователь попал к вам посредством organic search.
			//4.3 utmcmd - содержит название компании (или значение utm_medium в запросе) или сообщает об organic search.

			//4.4 utmctr - ключевые слова, по которым велся поиск.
			_searchKeyword = ExtractParameters(src[4], "utmctr");
		}
	}

	private string ExtractParameters(string src, string type_name)
	{
		//src format
		//utmcsr{source}|utmccn{campaign}|utmcmd{medium}|utmctr{keyword}
		
		//type_name
		//utmcsr, utmccn, utmcmd, utmctr

		string[] str = src.Split('|');

		if(str.Length > 4)
		{
			foreach(string s in str)
			{
				if(s.Contains(type_name))
				{
					return s.Replace("}", "").Replace(type_name + "{", "");
				}
			}
			
		}
		return "";
	}
}