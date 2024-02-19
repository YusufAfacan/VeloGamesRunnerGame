using UnityEngine;

public class RunnerAnimationManager : MonoBehaviour
{
    public static RunnerAnimationManager Instance;
    private Animator Animator => GetComponentInChildren<Animator>();

    private const string Jump = "Jump";
    private const string Slide = "Slide";


    private void Awake()
    {
        Instance = this;
    }

    public void PlayJump()
    {
        Animator.Play(Jump);
    }

    public void PlaySlide()
    {
        Animator.Play(Slide);
        
    }

}
