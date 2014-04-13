using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class TimeManager : MonoBehaviour
{
	private static TimeManager _instance = null;
	public static TimeManager Instance
	{
		get{ return _instance; }
	}

	private double _currentTime = 0.0f;
	public double CurrentTime
	{
		get { return _currentTime; }
	}
	
	private float _currentSpeed = 1.0f;
	private int _currentSpeedIndex = 0;
	
	public List<float> Speeds = new List<float>();
	public int StartingSpeed = 0;
		
	private GUIText _speedText = null;
	private GUIText _timeText = null;
	
	void Awake()
	{
		_instance = this;
		
		// Check to make sure Speeds are Correct
		DebugUtils.Assert(Speeds.Count > 0, "TimeManager: No Speeds in Speed list.");
		
		float lastSpeed = Speeds[0];
		
		for (int i = 1; i < Speeds.Count; ++i)
		{
			DebugUtils.Assert(Speeds[i] > lastSpeed, "TimeManager: Speed list is not ascending.");
						
			lastSpeed = Speeds[i];
		}
		
		DebugUtils.Assert(StartingSpeed >= 0 && StartingSpeed < Speeds.Count, "TimeManager: StartingSpeed is out of range.");
		
		_currentSpeedIndex = StartingSpeed;
		_currentSpeed = Speeds[StartingSpeed];
	}
	
	void OnEnable()
	{
		GameObject HUD = GameObject.FindGameObjectWithTag("HUD");
		
		if (HUD != null)
		{
			Transform trans = HUD.transform.FindChild("TimeSpeed");
			_speedText = trans.GetComponent<GUIText>();
			
			trans = HUD.transform.FindChild("Time");
			_timeText = trans.GetComponent<GUIText>();
		}
	}
	
	void OnDestroy()
	{
		_instance = null;
	}
	
	void Update()
	{
		PollInput();
		
		_currentTime += _currentSpeed * Time.deltaTime;
				
		UpdateHUD();
	}
	
	void PollInput()
	{
		if (Input.GetButtonDown("StopTime"))
		{
			Stop();
		}
		
		if (Input.GetButtonDown("IncreaseTimeSpeed"))
		{
			IncreaseSpeed();
		}
		
		if (Input.GetButtonDown("DecreaseTimeSpeed"))
		{
			DecreaseSpeed();
		}
		
		if (Input.GetButtonDown("ReverseTime"))
		{
			Reverse();
		}
	}
	
	void UpdateHUD()
	{
		if (_speedText != null)
		{
			_speedText.text = string.Format("Time Speed: {0} days/sec", _currentSpeed);
		}
		if (_timeText != null)
		{
			_timeText.text = string.Format("Time: {0:F}", _currentTime);
		}
	}
	
	public void IncreaseSpeed()
	{
		if (_currentSpeedIndex < Speeds.Count - 1)
		{
			_currentSpeedIndex++;
			
			_currentSpeed = Mathf.Sign(_currentSpeed) * Speeds[_currentSpeedIndex];
		}
	}
	
	public void DecreaseSpeed()
	{
		if (_currentSpeedIndex > 0)
		{
			_currentSpeedIndex--;
			
			_currentSpeed = Mathf.Sign(_currentSpeed) * Speeds[_currentSpeedIndex];
		}
		else if (_currentSpeedIndex == 0)
		{
			_currentSpeedIndex = -1;
			_currentSpeed = 0.0f;
		}
	}
	
	public void Reverse()
	{
		_currentSpeed = -1.0f * _currentSpeed;
	}
	
	public void Stop()
	{
		_currentSpeedIndex = -1;
		_currentSpeed = 0.0f;
	}
}
