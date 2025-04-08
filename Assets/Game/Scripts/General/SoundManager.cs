using UnityEngine;

public class SoundManager
{
    public const string coinSound = "coin";

    public const string actionMusic = "action";


    private SoundManager() { }

    private static SoundManager _instance;

    public static SoundManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new SoundManager();
            return _instance;
        }
    }
    public void PlaySound(string soundName, Vector3 pos)
    {
        //                                   AudioClip  Transform Volume Is3D   Randomization
        //SoundInstance.InstantiateOnTransform(Clip_Fire, transform, -1, false, SoundInstance.Randomization.Medium);
        SoundInstance.InstantiateOnPos(SoundInstance.GetClipFromLibrary(soundName), pos, 1.0f, true, SoundInstance.Randomization.Medium);
    }
    public void PlaySound(string soundName)
    {
        //                                   AudioClip  Transform Volume Is3D   Randomization
        //SoundInstance.InstantiateOnTransform(Clip_Fire, transform, -1, false, SoundInstance.Randomization.Medium);
        SoundInstance.InstantiateOnPos(SoundInstance.GetClipFromLibrary(soundName), new Vector3(), 1.0f, false, SoundInstance.Randomization.Medium);
    }

    public void StartMusic(string musicName)
    {
        SoundInstance.StartMusic(musicName, 1f);
    }

    public void SwitchMusic()
    {
        SoundInstance.StartMusic(SoundInstance.GetNextMusic().name, 1f);
    }

    public void PauseMusic()
    {
        SoundInstance.PauseMusic(1.5f);
    }

    public void ResumeMusic()
    {
        SoundInstance.ResumeMusic(1.5f);
    }
}