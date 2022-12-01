using System.Collections.Generic;
using UnityEngine;
using SOEvents;

public class SfxMain : MonoBehaviour
{
    [SerializeField] GameObjectFloatSOEvent playerTakeDamageEvent;
    [SerializeField] IntSOEvent playerLevelUpEvent;
    [SerializeField] List<AudioClip> playerDamagedClips = new List<AudioClip>();
    [SerializeField] [Range(0f, 1f)] float playerDamagedClipVolume;
    [SerializeField] AudioClip playerLevelUpClip;
    [SerializeField] [Range(0f, 1f)] float playerLevelUpClipVolume;

    private void Awake() {
        playerTakeDamageEvent.AddListener(PlayPlayerDamagedSound);
        playerLevelUpEvent.AddListener(PlayLevelUpSound);
    }

    private void PlayPlayerDamagedSound(GameObject player, float damageValue)
    {
        AudioSource.PlayClipAtPoint(playerDamagedClips[Random.Range(0, playerDamagedClips.Count)], player.transform.position, playerDamagedClipVolume);
    }

    public void PlaySound(AudioClip clip, Vector3 position, float volume = 0.25f, bool isDiscrete = true)
    {
        if (isDiscrete) AudioSource.PlayClipAtPoint(clip, position, volume);
    }

    public void PlayLevelUpSound(int playerLevel)
    {
        AudioSource.PlayClipAtPoint(playerLevelUpClip, Camera.main.transform.position, playerLevelUpClipVolume);
    }

}