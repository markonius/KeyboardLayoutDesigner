using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour 
{
	public KeyboardController _keyboard;

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 100))
			{
				Key key = hit.collider.gameObject.GetComponent<Key>();
				if (key != null)
				{
					Key._dragging = key;
					Debug.Log(key._normalCount);
					return;
				}

				Slot slot = hit.collider.gameObject.GetComponent<Slot>();
				if (slot != null && Key._dragging != null)
					slot.SetKey(Key._dragging);
			}
			
			Key._dragging = null;
		}
		else if (Input.GetMouseButtonDown(1))
		{
			
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 100))
			{
				Key key = hit.collider.gameObject.GetComponent<Key>();
				if (key != null)
				{
					_keyboard._assignedKeys.Remove(key);
					_keyboard._keys.Add(key);
					_keyboard.SortKeys();
				}
			}
		}
	}
}
