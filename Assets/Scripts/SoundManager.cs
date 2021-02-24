using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}
public class SoundManager : Interacable
{
    #region
    private static SoundManager instance = null;
    public string bgmName;

    private int sceneIndex = 0;
    public static SoundManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType(typeof(SoundManager)) as SoundManager;

                if (instance == null)
                    Debug.LogError("ERROR::SOUND_MANAGER::NOT INITIALISED");
            }
            return instance;
        }
    }

    private void Start()
    {
        SoundManager.instance.PlayBGM(bgmName);
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion

    [SerializeField]
    private Sound[] sfx = null;
    [SerializeField]
    private Sound[] bgm = null;

    [SerializeField] AudioSource bgmPlayer = null;
    [SerializeField] AudioSource[] sfxPlayer = null;

    [SerializeField]
    private float  onHammerTimePitch = 1.7f;
    [SerializeField]
    private float offHammerTimePitch = 1.0f;

    public void PlayBGM(string _bgmName)
    {
        for(int i = 0; i < bgm.Length; i++)
        {
            if(_bgmName == bgm[i].name)
            {
                bgmPlayer.clip = bgm[i].clip;
                bgmPlayer.Play();
            }
        }
    }

    public void StopBGM()
    {
        bgmPlayer.Stop();
    }
    public void PlaySFX(string _sfxName)
    {
        for (int i = 0; i < sfx.Length; i++)
        {
            if (_sfxName == sfx[i].name)
            {
                for(int x = 0; x<sfxPlayer.Length; x++)
                {
                    if(!sfxPlayer[x].isPlaying)
                    {
                        sfxPlayer[x].clip = sfx[i].clip;
                        sfxPlayer[x].Play();
                        return;
                    }
                }
                Debug.Log("");
                return;
            }
        }
        Debug.Log(_sfxName + " cannot be found in the sfx array");
    }

    public override void OnHammerTime()
    {
        bgmPlayer.pitch = onHammerTimePitch;
    }

    public override void OffHammerTime()
    {
        bgmPlayer.pitch = offHammerTimePitch;
    }

}
