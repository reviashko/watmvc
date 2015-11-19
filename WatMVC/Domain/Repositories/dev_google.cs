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
			//������: XXXX.DDDD.FFFF.PPPP.CCCC.N
			//0 XXXX - hash ������, �������� ���������� �� ��������.
			//1 DDDD - ���������� ID ������������ � ������� Google Analytics.

			//2 FFFF - ���� ������� ��������� ������������� ����� � Unix ������� (���������� ������, ��������� � ������� ������ 1970-��� ����).
			double.TryParse(src[2], out tmp_dbl);
			_firstVisit = ConvertFromUnixTimestamp(tmp_dbl);

			//3 PPPP - ���� ����������� ��������� ������������� ����� � Unix �������.
			double.TryParse(src[3], out tmp_dbl);
			_lastVisit = ConvertFromUnixTimestamp(tmp_dbl);

			//4 CCCC - ����� ������ �������� ��������� (������ ������) � Unix �������.
			//5 N - ���������� ��������� ����� ������ �������������.			
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
			//������: XXXX.P.10.CCCC
			//XXXX - hash ������.

			//0 P - ���������� �������, ������������� ������������� � ������� ������� ������.
			int.TryParse(src[0], out _viewedPagesCount);

			//1 10 - ���������� ����� Google ���������� �� ���� ������ ��������, �� ���������� � �������� �������. �����, �������� ���������� �� �����.
			//2 CCCC - ����� ������ �������� ��������� (������ ������) � Unix ������� (���������� CCCC ��������� _utma).
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
			//������: XXXX.TTTT.V.S.utmcsr{source}|utmccn{campaign}|utmcmd{medium}|utmctr{keyword}
			//0 XXXX - hash ������.
			//1 TTTT - ���� ���������� ���������� cookies � unix �������.
			
			//2 V - ���������� ��������� ������������� �����, ����������� �� ������� � ������ ��������.
			int.TryParse(src[2], out _visitFromForeignSiteCount);

			//3 S - ���������� ��������� ��������, � ������� ������������ ������� �� ����.
			int.TryParse(src[3], out _foreignSiteCount);

			//4.1 utmcsr - ������-���������, � �������� ������������ ����� �� ����.
			_searchFrom = ExtractParameters(src[4], "utmcsr");

			//4.2 utmccn - �������� ���������� � �������� �� AdWords (��� �������� utm_campaign � �������) ��� �� ��������, ��� ������������ ����� � ��� ����������� organic search.
			//4.3 utmcmd - �������� �������� �������� (��� �������� utm_medium � �������) ��� �������� �� organic search.

			//4.4 utmctr - �������� �����, �� ������� ����� �����.
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