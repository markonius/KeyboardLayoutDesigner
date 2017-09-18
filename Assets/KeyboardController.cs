using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class KeyboardController : MonoBehaviour 
{
	public Row[] _rows = new Row[12];

	private GameObject _keyPrefab;
	private GameObject _linePrefab;
	public List<Key> _keys = new List<Key>();
	public List<Key> _assignedKeys = new List<Key>();
	public int _maxKeysPerRow = 15;
	public float _keysXOffset = 10;
	public float _keysYOffset = 4;

	public string _layout;

	private void Start () 
	{
		_rows[0] = new Row(13, new Vector2(0, 0), transform, this);
		_rows[1] = new Row(13, new Vector2(0, -0.3f), transform, this);
		_rows[2] = new Row(13, new Vector2(0, -0.6f), transform, this);
		_rows[3] = new Row(12, new Vector2(1.5f, -1), transform, this);
		_rows[4] = new Row(12, new Vector2(1.5f, -1.3f), transform, this);
		_rows[5] = new Row(12, new Vector2(1.5f, -1.6f), transform, this);
		_rows[6] = new Row(12, new Vector2(1.75f, -2f), transform, this);
		_rows[7] = new Row(12, new Vector2(1.75f, -2.3f), transform, this);
		_rows[8] = new Row(12, new Vector2(1.75f, -2.6f), transform, this);
		_rows[9] = new Row(11, new Vector2(1.25f, -3), transform, this);
		_rows[10] = new Row(11, new Vector2(1.25f, -3.3f), transform, this);
		_rows[11] = new Row(11, new Vector2(1.25f, -3.6f), transform, this);

		_keyPrefab = Resources.Load<GameObject>("Key");
		_linePrefab = Resources.Load<GameObject>("Connection");
	}

	public void LoadHeatMap(string path)
	{
		using (StreamReader reader = new StreamReader(path))
		{
			// Singles
			while (true)
			{
				string line = reader.ReadLine();
				if (line == "--")
				{
					Debug.Log("Done with singles");
					break;
				}

				string c = line[0].ToString();
				string countS = line.Substring(2);
				int count = int.Parse(countS);

				bool found = false;
				foreach(Key key in _keys)
				{
					if (key._normal.text == c)
					{
						key._normalCount = count;
						if (count > Key._maxCount)
							Key._maxCount = count;
						found = true;
						break;
					}
				}

				if (!found)
				{
					GameObject go = GameObject.Instantiate(_keyPrefab);
					Key key = go.GetComponent<Key>();
					key._normalCount = count;
					key._normal.text = c;
					if (count > Key._maxCount)
						Key._maxCount = count;

					_keys.Add(key);
				}
			}

			// Doubles
			while (true)
			{
				string line = reader.ReadLine();
				if (line == null)
				{
					Debug.Log("Done with doubles");
					break;
				}

				string pair = line.Remove(2);
				string countS = line.Substring(3);
				int count = int.Parse(countS);

				foreach (Key key in _keys)
				{
					if (key._normal.text == pair[0].ToString())
					{
						bool found = false;
						foreach (Key other in _keys)
						{
							if (other._normal.text == pair[1].ToString())
							{
								key._nexts.Add(other);
								key._nextCounts.Add(count);
								GameObject go = GameObject.Instantiate(_linePrefab);
								LineRenderer lineRenderer = go.GetComponent<LineRenderer>();
								key._lineRenderers.Add(lineRenderer);

								if (count > Key._maxNextCount)
									Key._maxNextCount = count;
								found = true;
								break;
							}
						}
						if (!found)
							Debug.LogWarning("Missing key: " + pair[1].ToString());
					}
				}
			}

			SortKeys();
		}
	}

	public void SortKeys()
	{
		_keys.Sort((l, r) => r._normalCount - l._normalCount);
		for (int i = 0; i < _keys.Count; i++)
		{
			Key key = _keys[i];
			int x = i % _maxKeysPerRow;
			int y = i / _maxKeysPerRow;
			key.gameObject.transform.localPosition = new Vector2(x - _keysXOffset, _keysYOffset - y * 0.3f);
		}
	}

	private string[][] LoadLayout()
	{
		string[] contents = ReadLines("Assets/" + _layout + ".layout").ToArray();
		
		string[][] layout = new string[4][];

		for (int i = 0, j = 0; i < 4; i++)
		{
			layout[i] = new string[3];
			
			for (int k = 0; k < 3; k++, j++)
				layout[i][k] = contents[j];

			j++;
		}

		return layout;
	}

	public static IEnumerable<string> ReadLines(string path)
	{
		using (StreamReader reader = new StreamReader(path))
		{
			while (!reader.EndOfStream)
				yield return reader.ReadLine();
		}
	}
}
