using DG.Tweening;
using System;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
public class RunnerMovement : MonoBehaviour
{
    public static RunnerMovement Instance;
    public float RunSpeed { get; private set; }
    public bool IsDashing;
    public bool IsBouncingBack;
    public bool IsInvulnerable;
    public bool IsSliding;

    [SerializeField] float _minRunSpeed;
    [SerializeField] float _maxRunSpeed;
    [SerializeField] float _accelaration;
    [SerializeField] float _jumpHeight;
    [SerializeField] float _checkGroundDistance;
    [SerializeField] float _invulnerabilityWindow;

    private enum CurrentPos { Mid, Left, Right };
    [SerializeField] private CurrentPos _currentPos;

    private enum HorizontalState { None, DashingLeft, DashingMid, DashingRight, };
    [SerializeField] private HorizontalState _horizontalState;

    private Rigidbody RigidBody => GetComponent<Rigidbody>();
    private CapsuleCollider Collider => GetComponent<CapsuleCollider>();

    private RunnerAnimationManager Animator => RunnerAnimationManager.Instance;

    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        Accelerate();
        KeepBoundaries();

#if (UNITY_EDITOR)

        if (Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            DashLeft();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            DashRight();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            FastLanding();
        }

#endif


    }

    private void KeepBoundaries()
    {
        if (transform.position.x > 2)
        {
            Vector3 pos = transform.position;
            pos.x = 2;
            transform.position = pos;
            RigidBody.velocity = Vector3.zero;
            _currentPos = CurrentPos.Right;
            IsDashing = false;
            IsBouncingBack = false;
            _horizontalState = HorizontalState.None;
        }

        if (transform.position.x < -2)
        {
            Vector3 pos = transform.position;
            pos.x = -2;
            transform.position = pos;
            RigidBody.velocity = Vector3.zero;
            _currentPos = CurrentPos.Left;
            IsDashing = false;
            IsBouncingBack = false;
            _horizontalState = HorizontalState.None;
        }

        if (_currentPos == CurrentPos.Left && -0.02f < transform.position.x)
        {
            Vector3 pos = transform.position;
            pos.x = 0;
            transform.position = pos;
            RigidBody.velocity = Vector3.zero;
            _currentPos = CurrentPos.Mid;
            IsDashing = false;
            IsBouncingBack = false;
            _horizontalState = HorizontalState.None;
        }

        if (_currentPos == CurrentPos.Right && transform.position.x < 0.02f)
        {
            Vector3 pos = transform.position;
            pos.x = 0;
            transform.position = pos;
            RigidBody.velocity = Vector3.zero;
            _currentPos = CurrentPos.Mid;
            IsDashing = false;
            IsBouncingBack = false;
            _horizontalState = HorizontalState.None;
        }
    }

    private void Accelerate()
    {
        if (RunSpeed == _maxRunSpeed) return;

        RunSpeed = Mathf.Clamp(RunSpeed + _accelaration * Time.deltaTime, _minRunSpeed, _maxRunSpeed);
    }

    public void DashLeft()
    {
        if (_horizontalState != HorizontalState.None) { return; }
        if (IsDashing) { return; }
        if (IsSliding) { return; }
        if (_currentPos == CurrentPos.Left) return;

        transform.DOMoveX(transform.position.x - 2, 1).OnComplete(() =>
        {
            IsDashing = false;
            if (_currentPos == CurrentPos.Mid) _currentPos = CurrentPos.Left;
            else if (_currentPos == CurrentPos.Right) _currentPos = CurrentPos.Mid;
        });
        
        IsDashing = true;
    }

    public void DashRight()
    {
        if (_horizontalState != HorizontalState.None) { return; }
        if (IsDashing) { return; }
        if (IsSliding) { return; }
        if (_currentPos == CurrentPos.Right) return;

        transform.DOMoveX(transform.position.x + 2, 1).OnComplete(() =>
        {
            IsDashing = false;
            if (_currentPos == CurrentPos.Mid) _currentPos = CurrentPos.Right;
            else if (_currentPos == CurrentPos.Left) _currentPos = CurrentPos.Mid;
        });

        IsDashing = true;
    }

    private bool GroundCheck()
    {
        return Physics.Raycast(transform.position + Collider.center, Vector3.down, Collider.center.y + _checkGroundDistance);
    }

    public void Jump()
    {
        if (!GroundCheck()) { return; };
        if (IsDashing) { return ; }
        if (IsSliding) { return; }

        RigidBody.useGravity = true;

        float jumpForce = Mathf.Sqrt(_jumpHeight * -2 * Physics.gravity.y);
        RigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        Animator.PlayJump();
    }

    public void Slide()
    {
        if (IsSliding) return;
        if (IsDashing) { return; }

        Debug.Log("Slide");
        IsSliding = true;
        Animator.PlaySlide();
    }

    public void FastLanding()
    {
        if (GroundCheck()) 
        {
            Slide();
            return; 
        };

        if (IsDashing) { return; }

        float dashForce = 5f;
        RigidBody.AddForce(Vector3.down * dashForce, ForceMode.Impulse);

    }

    public void BounceBack()
    {
        if (IsBouncingBack) return;

        switch (_currentPos)
        {
            case CurrentPos.Mid:
                
                transform.DOMoveX(0, 1);
                break;
            case CurrentPos.Left:
               
                transform.DOMoveX(-2, 1);
                break;
            case CurrentPos.Right:
                
                transform.DOMoveX(2, 1);
                break;
        }

        IsBouncingBack = true;
    }

    public async void BeInvulnerable()
    {
        DamageBlink();
        IsInvulnerable = true;
        TimeSpan time = TimeSpan.FromSeconds(_invulnerabilityWindow);
        await UniTask.Delay(time, DelayType.DeltaTime, PlayerLoopTiming.Update, new CancellationTokenSource().Token);
        IsInvulnerable = false;
    }

    public void DamageBlink()
    {
        SkinnedMeshRenderer renderer = GetComponentInChildren<SkinnedMeshRenderer>();
        Color originalColor = renderer.material.color;
        renderer.material.DOColor(Color.red, 1).SetLoops(3, LoopType.Yoyo).OnComplete(() => renderer.material.color = originalColor);
    }
}
