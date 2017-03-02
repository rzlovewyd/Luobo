using UnityEngine;

public class Sound : Singleton<Sound> 
{
    public string ResourceDir = "";

    AudioSource m_BgSound;
    AudioSource m_EffectSound;

    protected override void Awake()
    {
        base.Awake();

        m_BgSound = gameObject.AddComponent<AudioSource>();
        m_BgSound.playOnAwake = false;
        m_BgSound.loop = true;

        m_EffectSound = gameObject.AddComponent<AudioSource>();
    }

    //音乐大小
    public float BgVolume
    {
        get { return m_BgSound.volume; }
        set { m_BgSound.volume = value; }
    }

    //音效大小
    public float EffectVolume
    {
        get { return m_EffectSound.volume; }
        set { m_EffectSound.volume = value; }
    }

    //播放音乐
    public void PlayBg(string audioName)
    {
        //当前正在播放的音乐文件
        string oldName = m_BgSound.clip != null ? m_BgSound.clip.name : "";
        if(oldName != audioName)
        {
            //音乐文件路径
            string path = string.IsNullOrEmpty(ResourceDir) ? audioName : ResourceDir + "/" + audioName;

            //加载音乐文件
            AudioClip clip = Resources.Load<AudioClip>(path);

            //播放
            if (clip != null)
            {
                m_BgSound.clip = clip;
                m_BgSound.Play();
            }
        }
    }

    //停止音乐
    public void StopBg()
    {
        m_BgSound.Stop();
        m_BgSound.clip = null;
    }

    //播放音效
    public void PlayEffect(string audioName)
    {
        //路径
        string path = string.IsNullOrEmpty(ResourceDir) ? audioName : ResourceDir + "/" + audioName;

        //音频
        AudioClip clip = Resources.Load<AudioClip>(path);

        //播放
        m_EffectSound.PlayOneShot(clip);
    }
}
