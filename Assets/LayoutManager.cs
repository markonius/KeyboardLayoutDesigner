using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class LayoutManager : MonoBehaviour 
{
	public KeyboardController _keyboard;
	public InputField _layoutPath;

	public void Save()
	{
		using (StreamWriter writer = new StreamWriter(_layoutPath.text))
		{
			foreach (Key key in _keyboard._assignedKeys)
			{
				Vector3 position = key.transform.position;
				writer.WriteLine(key._normal.text + " " + position.x + " " + position.y + " " + position.z);
			}
		}
		Debug.Log("Saved layout");
	}

	public void Load()
	{
		while (_keyboard._assignedKeys.Count > 0)
		{
			Key key = _keyboard._assignedKeys[_keyboard._assignedKeys.Count - 1];
			_keyboard._assignedKeys.RemoveAt(_keyboard._assignedKeys.Count - 1);
			_keyboard._keys.Add(key);
		}

		var lines = KeyboardController.ReadLines(_layoutPath.text);
		foreach (string line in lines)
		{
			string[] stuff = line.Split(' ');

			foreach (Key key in _keyboard._keys)
			{
				if (key._normal.text == stuff[0])
				{
					_keyboard._assignedKeys.Add(key);
					float x = float.Parse(stuff[1]);
					float y = float.Parse(stuff[2]);
					float z = float.Parse(stuff[3]);
					key.transform.position = new Vector3(x, y, z);
					break;
				}
			}

			foreach (Key key in _keyboard._assignedKeys)
			{
				_keyboard._keys.Remove(key);
			}

		}

		_keyboard.SortKeys();
	}
}
