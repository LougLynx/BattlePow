using UnityEngine;

public class AudioManager : MonoBehaviour
{
	[SerializeField] AudioSource effectAudioSource;
	[SerializeField] AudioSource defaultAudioSource;
	[SerializeField] AudioSource bossAudioSource;
	[SerializeField] AudioClip Shotclip;
	[SerializeField] AudioClip ReloadClip;
	[SerializeField] AudioClip EnergyClip;

	public void PlayShot()
	{
		effectAudioSource.PlayOneShot(Shotclip);
	}

	public void PlayReload()
	{
		effectAudioSource.PlayOneShot(ReloadClip);
	}

	public void PlayEnerty()
	{
		effectAudioSource.PlayOneShot(EnergyClip);
	}

	public void PlayDefaultAudio()
	{
		bossAudioSource.Stop();
		defaultAudioSource.Play();
	}
	public void PlayBossAudio()
	{
		defaultAudioSource.Stop();
		bossAudioSource.Play();
	}

	public void StopAudioGame()
	{
		effectAudioSource.Stop();
		defaultAudioSource.Stop();
		bossAudioSource.Stop();
	}

}
