using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_FadeScreen : MonoBehaviour
{
    private Animator anim;


    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void FadeIn() => anim.SetTrigger("fadeIn");

    public void FadeOut() => anim.SetTrigger("fadeOut");
}
