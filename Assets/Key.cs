using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour 
{
	public TextMesh _normal;

	public int _normalCount;

	public List<Key> _nexts = new List<Key>();
	public List<int> _nextCounts = new List<int>();
	public List<LineRenderer> _lineRenderers = new List<LineRenderer>();

	public MeshRenderer _renderer;
	public Material _material;

	public float _colorCurve;

	public static int _maxCount;
	public static int _maxNextCount;

	public static Key _dragging;

	private void Start()
	{
		_material = GameObject.Instantiate(_material);
		_renderer.material = _material;
	}

	private void Update()
	{
		// Draw connections
		for(int i = 0; i < _nexts.Count; i++)
		{
			Key key = _nexts[i];
			int count = _nextCounts[i];
			LineRenderer line = _lineRenderers[i];

			float connectionHue = (1 - Mathf.Pow((float)count / _maxNextCount, _colorCurve)) * (5.0f / 6);
			line.endColor = line.startColor = Color.HSVToRGB(connectionHue, 1, 1);

			if (count < ConnectionCountController.slider.value || (_dragging != null && _dragging != this))
				line.enabled = false;
			else
				line.enabled = true;

			line.SetPosition(0, transform.position + Vector3.back * 0.15f);
			line.SetPosition(1, key.transform.position + Vector3.back * 0.15f);
		}
		
		// Draw key
		if (_dragging == this)
		{
			_material.color = Color.white;
			return;
		}

		float hue = (1 - Mathf.Pow((float)_normalCount / _maxCount, _colorCurve)) * (5.0f / 6);
		_material.color = Color.HSVToRGB(hue, 1, 1);
	}
}
