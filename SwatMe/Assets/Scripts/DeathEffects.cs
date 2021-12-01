using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEffects : MonoBehaviour
{
    [SerializeField] private SimpleAudioEvent deathSFX;
    private AudioSource m_AudioSource;

    private void Awake()
    {
        m_AudioSource = this.GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        deathSFX.Play(m_AudioSource);

        Destroy(this.gameObject, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
