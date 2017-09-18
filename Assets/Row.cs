using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Row
{
	public int _count;
	public Vector2 _offset;
	public Slot[] _slots;

	public Row(int count, Vector2 offset, Transform keyboard, KeyboardController kb)
	{
		_count = count;
		_offset = offset;
		_slots = new Slot[count];

		for (int i = 0; i < count; i++)
		{
			GameObject go = Resources.Load<GameObject>("Slot");
			go = GameObject.Instantiate(go);

			_slots[i] = go.GetComponent<Slot>();
			_slots[i]._keyboard = kb;

			go.transform.parent = keyboard;
			go.transform.localPosition = new Vector3(offset.x + i, offset.y, 0.2f);
		}
	}
}
