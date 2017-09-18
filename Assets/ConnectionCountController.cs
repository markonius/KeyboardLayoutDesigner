using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConnectionCountController : MonoBehaviour
{
	public Slider _slider;
	public static Slider slider;

	public Text _min;
	public Text _max;
	public Text _current;

	private void Start()
	{
		slider = _slider;
	}

	private void Update()
	{
		_slider.maxValue = Key._maxNextCount;
		_min.text = 0.ToString();
		_max.text = _slider.maxValue.ToString();
		_current.text = _slider.value.ToString();
	}
}
