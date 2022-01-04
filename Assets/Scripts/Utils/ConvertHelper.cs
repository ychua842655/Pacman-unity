using System;
using System.Collections;
using System.Collections.Generic;

public static class ConvertHelper
{
	public static int AToI(object obj, int defaultVal = -1)
	{
		if (obj == null)
		{
			return defaultVal;
		}
		string s = obj.ToString();
		return int.Parse(s);
	}

	public static float AToF(object obj, float defaultVal = -1)
	{
		if (obj == null)
		{
			return defaultVal;
		}
		string s = obj.ToString();
		return float.Parse(s);
	}

	static public double AToD(object obj, double defaultVal = -1)
	{
		if (obj == null)
		{
			return defaultVal;
		}
		string s = obj.ToString();
		return double.Parse(s);
	}

	static public long AToL(object obj, long defaultVal = -1)
	{
		if (obj == null) return defaultVal;
		string s = obj.ToString();
		return long.Parse(s);
	}

	static public string OToS(object obj, string defaultVal = "")
	{
		if (obj == null)
		{
			return defaultVal;
		}
		string s = obj.ToString();
		return s;
	}

	static public bool AToB(object obj, bool defaultVal = false)
	{
		if (obj == null)
		{
			return defaultVal;
		}
		string s = obj.ToString();
		return bool.Parse(s);
	}

	public static System.DateTime ConvertToDateTime(string time)
	{
		//"1986-10-2 23:23:23 "

		string[] splitedTimeStrs = time.Split('T');
		if (splitedTimeStrs.Length != 6)
		{
			// AppLogger.Error("wrong time string format or split");
		}

		string[] data = splitedTimeStrs[0].Split('-');

		int year = Convert.ToInt32(data[0]);
		int month = Convert.ToInt32(data[1]);
		int day = Convert.ToInt32(data[2]);

		string[] timeVal = splitedTimeStrs[1].Split(':');

		int hour = Convert.ToInt32(timeVal[0]);
		int minute = Convert.ToInt32(timeVal[1]);
		int second = Convert.ToInt32(timeVal[2]);
		DateTime dt = new DateTime(year, month, day, hour, minute, second);

		return dt;
	}

	public static string ConvertBytesToUTF8(byte[] bytes)
	{
		return System.Text.Encoding.UTF8.GetString(bytes);
	}

	public static string ConvertDateToString(System.DateTime date)
	{
		string format = "{0}-{1:00}-{2:00}T{3:00}:{4:00}:{5:00}";
		return string.Format(format, date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second);
	}

	public static int[] ConvertArrayFromStringToFloatToInt(string[] fromArray)
	{
		int[] toArray = new int[fromArray.Length];
		for (int i = 0; i < toArray.Length; i++)
		{
			toArray[i] = int.Parse(float.Parse(fromArray[i]).ToString());
		}
		return toArray;
	}

	public static Dictionary<int, int> ConvertHashtableToDictionary(Hashtable table)
	{
		Dictionary<int, int> dict = new Dictionary<int, int>();
		foreach (DictionaryEntry entry in table)
		{
			int attributeId = ConvertHelper.AToI(entry.Key);
			int attributeValue = ConvertHelper.AToI(entry.Value);
			dict.Add(attributeId, attributeValue);
		}
		return dict;
	}

	public static int[] ConvertArrayListToIntArray(ArrayList arrayList)
	{
		int[] result = new int[arrayList.Count];
		int i = 0;
		foreach (Object obj in arrayList)
		{
			result[i] = AToI(obj);
			i++;
		}
		return result;
	}

	public static float[] ConvertArrayListToFloatArray(ArrayList arrayList)
	{
		float[] result = new float[arrayList.Count];
		int i = 0;
		foreach (Object obj in arrayList)
		{
			result[i] = AToF(obj);
			i++;
		}
		return result;
	}

	public static long ConvertDateTimeToLong(DateTime dt)
	{
		return new DateTimeOffset(dt).ToUnixTimeSeconds();
	}
}

