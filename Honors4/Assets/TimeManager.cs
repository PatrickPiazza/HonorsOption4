using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class TimeManager : MonoBehaviour
{
	public static int totalCompletions;
	public static int totalFalls;
	[SerializeField] private TextMeshProUGUI comText;
	[SerializeField] private TextMeshProUGUI fallText;
	
	void Awake()
	{
		if(PlayerPrefs.HasKey("totalCompletions"))
		{
			RestoreValues();
		}
		
		else
		{
			NewGame();
		}
	}
	
	void Update()
	{
		comText.text = "Total Completions: "+totalCompletions;
		fallText.text = "Total Falls: "+totalFalls;
	}

	public void NewGame ()
	{
		if(PlayerPrefs.HasKey("totalCompletions"))
		{
			DeleteValues();
		}
		
		totalCompletions = 0;
		totalFalls = 0;
		StoreValues();
	}
	
	static public void StoreValues()
	{
		PlayerPrefs.SetInt("totalCompletions", totalCompletions);
		PlayerPrefs.SetInt("totalFalls", totalFalls);
		PlayerPrefs.Save();
	}
	
	void RestoreValues()
	{
		totalCompletions = PlayerPrefs.GetInt("totalCompletions");
		totalFalls = PlayerPrefs.GetInt("totalFalls");
	}
	
	static public void DeleteValues()
	{
		PlayerPrefs.DeleteKey("totalCompletions");
		PlayerPrefs.DeleteKey("totalFalls");
	}
}
