using UnityEngine;

[System.Serializable]
public class Sound
{
    [field: SerializeField] public AudioClip Audio {private set; get;}
    [field: SerializeField, Range(0f,1f)] public float Volume {private set; get;} = 1f;
    [field: SerializeField] public float Range {private set; get;} = 500f;

    public void Play(Vector3 position)
    {
        SoundManager.Instance.PlaySound(this, position);
    }

    public void PlayAtSource(AudioSource audioSource)
    {
        audioSource.PlayOneShot(Audio, Volume);
    }
}