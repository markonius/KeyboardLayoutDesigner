using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour 
{
	public KeyboardController _keyboard;

	public void SetKey(Key key)
	{
		_keyboard._keys.Remove(key);
		_keyboard._assignedKeys.Add(key);
		_keyboard.SortKeys();
		key.transform.localPosition = transform.position + new Vector3(0, 0, -0.2f);
	}
}
