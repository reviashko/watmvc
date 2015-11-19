using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using System.Text.RegularExpressions;


public class DevPage:System.Web.UI.Page
{
	public string TopArticle{get;set;}

	public string BotArticle{get;set;}

	public string LeftMenuSrc{get;set;}

	public void Page_PreInit(object sender, EventArgs e)
	{
		if (!Request.Url.ToString().Contains("http://localhost") && !Request.Url.ToString().Contains("www") || !HttpContext.Current.Request.RawUrl.Equals(HttpContext.Current.Request.RawUrl.ToLower().ToLower()))
		{
			HttpContext context = HttpContext.Current;
			context.Response.ClearHeaders();
			context.Response.StatusCode = 301;
			context.Response.Status = "301 Moved permanently";
			context.Response.AddHeader("Location", dev_const.SiteUrl + Request.RawUrl.ToString().Substring(1).ToLower());
			context.Response.Flush();
			context.Response.End();
		}

		if(Request.QueryString["nomobile"] != null)
		{
			Session.Add("nomobile", "1");
		}

		/*
		Request.RawUrl.ToString().Substring(1).ToLower()

		dev_cookie_mng cm = new dev_cookie_mng();
		if ( Request["nomobile"] != null )
			cm.WriteCookie("nomobile", "x", 20);


		if(Request.Browser.IsMobileDevice && ( Request["nomobile"] == null || cm.ReadCookie("nomobile") != "x") )
		{
			Response.Redirect("http://m.watshop.ru/");
		}
		*/

		if( Request.Browser.IsMobileDevice && Session["nomobile"] == null)
		{
			Response.Redirect("http://m.watshop.ru" + Request.RawUrl.ToString().ToLower());
		}

		/* if(Request.ServerVariables["HTTP_USER_AGENT"] != null && Session["nomobile"] == null)
		{
			string user_agent = Request.ServerVariables["HTTP_USER_AGENT"].ToLower();
			if(
				user_agent.Contains("windows ce") ||
				user_agent.Contains("opera mini") ||
				user_agent.Contains("avantGo") ||
				user_agent.Contains("mazingo") ||
				user_agent.Contains("mobile") ||
				user_agent.Contains("t68") ||
				user_agent.Contains("syncalot") ||
				user_agent.Contains("blazer") 
			){
				Response.Redirect("http://m.watshop.ru/");
			}else{
				Session["nomobile"] = "1";
			}
		}*/

	}


	public void Page_Load(object sender, EventArgs e)
	{
		Literal baseTag = new Literal();
		baseTag.Text = string.Format("<base href=\"{0}\" />", dev_const.SiteUrl);
		Page.Header.Controls.Add(baseTag);
	}

	virtual public bool SetPageData(int group_id, bool fillMetaData)
	{
		return SetPageData(group_id, fillMetaData, false, false);
	}

	virtual public bool SetPageData(int group_id, bool fillMetaData, bool fillLeftMenu, bool isTag)
	{
		InProcCache cmng = new InProcCache();
		string cacheItemName = string.Format("seodata_{0}", group_id);

		dev_page_metadata seoData = cmng.GetKey<dev_page_metadata>(cacheItemName);
		if(seoData == null)
		{
			IDataBase db = new MSSql();
			db.AddParameter(new SqlParameter("@Group_id", group_id));
			db.AddParameter(new SqlParameter("@Is_tag", isTag));
			db.SetStoredProcedure("WebSite.Groups_GetSeoData");
			DataTable src = db.GetDataTable();

			if(src != null && src.Rows.Count > 0)
			{
				seoData = new dev_page_metadata(src.Rows[0]);
				seoData.TopArticle = RenderControls(seoData.TopArticle);
				seoData.BotArticle = RenderControls(seoData.BotArticle);
			}else{
				seoData = new dev_page_metadata();
			}


			cmng.KeyAdd(cacheItemName, seoData, 9000, 18000);
		}

		if(!seoData.Exists)
		{
			return seoData.Exists;
		}

		TopArticle = seoData.TopArticle;
		BotArticle = seoData.BotArticle;


		if(fillMetaData)
		{
			SetMetaData(seoData.Title, seoData.Descr, seoData.Keywords);
		}

		return seoData.Exists;
	}

	public void SetMetaData(string title, string description, string keywords)
	{
		Literal descr_l = new Literal();
		descr_l.Text = string.Format("<meta name=\"description\" lang=\"ru\" content=\"{0}\" />", description);
		Page.Header.Controls.Add(descr_l);

		Literal keywords_l = new Literal();
		keywords_l.Text = string.Format("<meta name=\"keywords\" lang=\"ru\" content=\"{0}\" />", keywords);
		Page.Header.Controls.Add(keywords_l);

		Page.Title = title;
	}

	public void FillLeftMenuSrc(string left_menu_cntrl)
	{
		if(left_menu_cntrl != null)
		{
			LeftMenuSrc = RenderControls(left_menu_cntrl.Length > 5 ? left_menu_cntrl : "");
		}
	}

	protected string RenderControls(string html)
	{
		string pattern = @"(\[.*\])";
		Regex re = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
		MatchCollection mc = re.Matches(html);

		if (mc.Count > 0)
		{
			foreach(Match m in mc)
			{
				string search = m.Groups[0].Value;

				string[] data = search.Replace("[","").Replace("]","").Split(';');

				if(data.Length > 0 && data[0].Contains(".ascx") && File.Exists(AppDomain.CurrentDomain.BaseDirectory + data[0].Substring(1)))
				{
				
					try
					{
						DevControl cntrl = LoadControl(data[0]) as DevControl;
						Dictionary<string, object> prs = GetControlParams(data);

						cntrl.InitControl(prs);
						cntrl.DataBind();

						html = html.Replace(search, GetControlHtml(cntrl) );
					}catch{}
				}
			}
		}

		return html;
	}


	private Dictionary<string, object> GetControlParams(string[] data)
	{
		Dictionary<string, object> prs = new Dictionary<string, object>();
		if(data.Length > 0)
		{
			int tmp = 1;
			foreach(string str in data)
			{
				if(tmp > 1 && str.Length > 0)
				{
					string[] tmp_par = str.Split('=');
					if(tmp_par.Length == 2)
					{
						prs.Add(tmp_par[0], tmp_par[1].Replace("::", "="));
					}else{
						prs.Add("ControlName", str);
					}
				}
				tmp++;
			}
		}
		return prs;
	}

	private string GetControlHtml(Control ctrl)
	{
		System.Text.StringBuilder sb = new System.Text.StringBuilder();
		System.IO.StringWriter tw = new System.IO.StringWriter(sb);
		System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
		ctrl.RenderControl(hw);
		return sb.ToString();
	}
}