using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class demTime : MonoBehaviour
{
	public TextMeshProUGUI timeText;
	private float startTime;
	public float timeSk;
	private bool isSk = false;
	[SerializeField] private GameManager gameManager;
	void Start()
	{
		startTime = Time.time;
	}
	// Update is called once per frame 
	void Update()
	{
		float thoiGianDaTroiQua;
		thoiGianDaTroiQua = Time.time - startTime;

		int minutes = ((int)thoiGianDaTroiQua / 60);
		int secords = ((int)thoiGianDaTroiQua % 60);
		if (Time.timeScale == 0)
		{
			timeText.text = "";
		}
		else
		{
			timeText.text = string.Format("{0:00}:{1:00}", minutes, secords);
		}
		if (thoiGianDaTroiQua >= timeSk && !isSk)
		{
			Sukien();
		}
	}
	void Sukien()
	{
		gameManager.CallBoss();
	}
}
