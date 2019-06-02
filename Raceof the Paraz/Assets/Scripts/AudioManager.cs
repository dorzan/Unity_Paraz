using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Static instance 
    private static AudioManager instance;
    public static AudioManager Instace
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AudioManager>();
                if(instance == null)
                {
                    instance = new GameObject("spawned AudioManager", typeof(AudioManager)).GetComponent<AudioManager>();
                }
            }
        return instance;
       }
       private set
       {
            instance = value;
       }
    }
    #endregion
    #region Fields
    private AudioSource musicSource;
    private AudioSource musicSource2;
    private AudioSource sfxSource;
    private bool firstMusicSourceIsPlaying;
    #endregion

    #region Sounds
    private AudioClip tele;
    [SerializeField] private AudioClip mu1;
    private AudioClip mu2;
    private AudioClip boom;
    private AudioClip omer;
    private AudioClip won;
    private AudioClip FootmanDeath;

    #endregion

    IDictionary<string, AudioClip> dict;
    IDictionary<int, AudioClip> taunts;
    public int numOfTaunts = 3;
    public string soundPath;

    private void Awake()
    {
        // make sure we dont destroy this instance 
        musicSource = this.gameObject.AddComponent<AudioSource>();
        musicSource2 = this.gameObject.AddComponent<AudioSource>();
        sfxSource = this.gameObject.AddComponent<AudioSource>();

        //loop the music tracks
        musicSource.loop = true;
        musicSource2.loop = true;

        soundPath = Application.streamingAssetsPath + "/Sounds/";
      
        dict = new Dictionary<string, AudioClip>(); 
        taunts = new Dictionary<int, AudioClip>();
        StartCoroutine(LoadAudio());



    }

    private IEnumerator LoadAudio()
    {
        tele = Resources.Load<AudioClip>("Sounds/teleport sound effect");
        boom = Resources.Load<AudioClip>("Sounds/explosion");
        mu1 = Resources.Load<AudioClip>("Sounds/mu");
        mu2 = Resources.Load<AudioClip>("Sounds/mu2");
        omer = Resources.Load<AudioClip>("Sounds/omer leha");
        won = Resources.Load<AudioClip>("Sounds/won");
        FootmanDeath = Resources.Load<AudioClip>("Sounds/Castle Fight/Footman/FootmanDeath");


        taunts.Add(0, mu1);
        taunts.Add(1, mu2);
        taunts.Add(2, omer);
        dict.Add("teleport", tele);
        dict.Add("boom", boom);
        dict.Add("won", won);
        dict.Add("footmanDeath", FootmanDeath);

        yield return null;
    }

    public void PlayMusic (AudioClip musicClip)
    {
        AudioSource activeSource = (firstMusicSourceIsPlaying) ? musicSource : musicSource2;
        activeSource.clip = musicClip;
        activeSource.volume = 1;
        activeSource.Play();

    }

    public void PlayMusicWithFade (AudioClip newClip, float transitionTime = 1.0f)
    {
        AudioSource activeSource = (firstMusicSourceIsPlaying) ? musicSource : musicSource2;

        StartCoroutine(UpdateMusicWithFade(activeSource, newClip, transitionTime));

    }

    private IEnumerator UpdateMusicWithFade(AudioSource activeSource, AudioClip newClip, float transitionTime)
    {
        if (!activeSource)
            activeSource.Play();

        float t = 0.0f;
        //fade out
        for(t = 0; t < transitionTime; t+=Time.deltaTime)
        {
            activeSource.volume = (1 - (t / transitionTime));
            yield return null;
        }
        activeSource.Stop();
        activeSource.clip = newClip;
        activeSource.Play();

        //fade in
        for (t = 0; t < transitionTime; t += Time.deltaTime)
        {
            activeSource.volume = ((t / transitionTime));
            yield return null;
        }


    }

    public void playSFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void playSFX(AudioClip clip, float volume)
    {
        sfxSource.PlayOneShot(clip, volume);
    }

    public void playSFX(string name, float volume)
    {
        sfxSource.PlayOneShot(dict[name], volume);
    }

    public void playSFX(string name)
    {
        sfxSource.PlayOneShot(dict[name]);
    }

    public void playTaunt(int index)
    {
        sfxSource.PlayOneShot(taunts[index]);
    }

    public void playRandomTaunt()
    {
        System.Random rnd = new System.Random();
        int temp = rnd.Next(numOfTaunts);
        sfxSource.PlayOneShot(taunts[temp]);

    }

}
