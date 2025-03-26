using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private float sfxminimumDistance;
    [SerializeField] private AudioSource[] sfx;
    [SerializeField] private AudioSource[] bgm;

    public bool playBgm;
    public int bgmIndex;

    private bool canPlaySFX;


    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        else
            instance = this;

        Invoke("AllowSFX", 1f);
    }

    private void Update()
    {
        if (!playBgm)
            StopAllBGM();
        else
            if (!bgm[bgmIndex].isPlaying)
                bgm[bgmIndex].Play();
    }

    public void PlaySFX(int _sfxIndex, Transform _source)
    {
        if (sfx[_sfxIndex].isPlaying)
            return;

        if (canPlaySFX == false)
            return;

        if (_source != null && Vector2.Distance(PlayerManager.instance.player.transform.position, _source.position) > sfxminimumDistance)
            return;

        if(_sfxIndex < sfx.Length)
        {
            sfx[_sfxIndex].pitch = Random.Range(0.75f, 1.2f);
            sfx[_sfxIndex].Play();
        }
    }

    public void StopSFX(int _sfxIndex) => sfx[_sfxIndex].Stop();

    public void StopSFXWithTime(int _index) => StartCoroutine(DecreaseVolume(sfx[_index]));

    private IEnumerator DecreaseVolume(AudioSource _audio)
    {
        float defaultVolume = _audio.volume;

        while (_audio.volume < .1f)
        {
            _audio.volume = _audio.volume * .2f;
            yield return new WaitForSeconds(.6f);

            if(_audio.volume <= .1f)
            {
                _audio.Stop();
                _audio.volume = defaultVolume;
                break;
            }
        }
    }

    public void PlayBGM(int _bgmIndex)
    {
        bgmIndex = _bgmIndex;

        StopAllBGM();
        bgm[_bgmIndex].Play();
    }

    public void PlayRandomBGM()
    {
        bgmIndex = Random.Range(0, bgm.Length);
        PlayBGM(bgmIndex);
    }

    private void StopAllBGM()
    {
        for (int i = 0; i < bgm.Length; i++)
        {
            bgm[i].Stop();
        }
    }

    private void AllowSFX() => canPlaySFX = true;
}
