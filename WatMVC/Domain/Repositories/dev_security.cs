using System;
using System.Configuration;
using System.Data;
using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Data.SqlClient;

public class dev_security
{
    public dev_security()
    { 
        //
    }

    /*public static string GetWinEncoding(string encodedData)
    {
        string result = encodedData;
        string abc = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя ";
        string newEncodingData = encodedData.ToLower();
        
        foreach (char find in newEncodingData.ToCharArray())
        {
            if (!abc.Contains(find.ToString()))
            {
                Encoding win1251 = Encoding.GetEncoding("windows-1251");
                Encoding utf8 = Encoding.UTF8;

                byte[] utf8Bytes = utf8.GetBytes(encodedData);
                byte[] winBytes = Encoding.Convert(utf8, win1251, utf8Bytes, 0, utf8Bytes.Length);
                result = utf8.GetString(winBytes, 0, winBytes.Length);
                break;
            }
        }
        return result;
    }*/
    
	public static string GetMD5(string src) 
	{
	   byte[] textBytes = System.Text.Encoding.Default.GetBytes(src);
	   try 
	   {
		  System.Security.Cryptography.MD5CryptoServiceProvider cryptHandler;
		  cryptHandler = new System.Security.Cryptography.MD5CryptoServiceProvider();
		  byte[] hash = cryptHandler.ComputeHash (textBytes);
		  string ret = "";
		  foreach (byte a in hash) 
		  {
			 if (a<16)
				ret += "0" + a.ToString ("x");
			 else
				ret += a.ToString ("x");
		  }
		  return ret ;
	   }
	   catch 
	   {
		  throw;
	   }
	}

}