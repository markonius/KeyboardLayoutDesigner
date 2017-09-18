using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.IO;
using System;
using System.Text.RegularExpressions;

public class Analyzer : MonoBehaviour 
{
	public InputField _regex;
	public InputField _dataPath;
	public InputField _outputPath;
	public InputField _logPath;
	public KeyboardController _keyboard;

	public void Analyze()
	{
		string path = _dataPath.text;
		string van = _outputPath.text;
		var singles = new Dictionary<char, int>();
		var doubles = new Dictionary<string, int>();
		int count = 0;

		Regex regex = new Regex(_regex.text);

		var files = GetFiles(path, regex.IsMatch);
		foreach(string file in files)
			count += PerformAnalysis(file, singles, doubles);

		using (StreamWriter writer = new StreamWriter(van))
		{
			foreach (var pair in singles)
				writer.WriteLine(pair.Key + " " + pair.Value);

			writer.WriteLine("--");

			foreach (var pair in doubles)
				writer.WriteLine(pair.Key + " " + pair.Value);
		}

		Debug.Log(string.Format("Analyzed {0} characters.", count));
	}

	private int PerformAnalysis(string file, Dictionary<char, int> singles, Dictionary<string, int> doubles)
	{
		int chars = 0;
		char previous = '\n';
		using (StreamReader reader = new StreamReader(file))
		{
			while(reader.Peek() > 0)
			{
				char c = (char)reader.Read();
				if (IsValid(c))
				{
					if (char.IsLetter(c))
						c = char.ToLower(c);

					int count = 0;
					singles.TryGetValue(c, out count);
					singles[c] = count + 1;
					
					if (IsValid(previous) && previous != c)
					{
						count = 0;
						string key = previous.ToString() + c;
						doubles.TryGetValue(key, out count);
						doubles[key] = count + 1;
					}
				}

				previous = c;
				chars++;
			}
		}
		return chars;
	}

	private bool IsValid(char c)
	{
		return c != '\n' && c != '\t' && c != ' ' && c != '\r';
	}

	public void Load()
	{
		_keyboard.LoadHeatMap(_logPath.text);
	}

	public static IEnumerable<string> GetFiles(string path, Func<string, bool> discriminator)
	{
		return Directory.GetFiles(path)
						.Where(discriminator)
						.Concat(
								Directory.GetDirectories(path)
								.SelectMany(d => GetFiles(d, discriminator)));
	}
}
