using UnityEngine;

public class SwipeDetector : MonoBehaviour
{
    private Vector2 _fingerDownPos;
    private Vector2 _fingerUpPos;

    public bool DetectSwipeAfterRelease = false;

    public float SwipeThreshhold = 20f;

    private RunnerMovement RunnerMovement => RunnerMovement.Instance;

    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                _fingerUpPos = touch.position;
                _fingerDownPos = touch.position;
            }

            if (touch.phase == TouchPhase.Moved)
            {
                if (!DetectSwipeAfterRelease)
                {
                    _fingerDownPos = touch.position;
                    DetectSwipe();
                }
            }

            if (touch.phase == TouchPhase.Ended)
            {
                _fingerDownPos = touch.position;
                DetectSwipe();
            }
        }
    }

    void DetectSwipe()
    {
        if (VerticalMoveValue() > SwipeThreshhold && VerticalMoveValue() > HorizontalMoveValue())
        {
            if (_fingerDownPos.y - _fingerUpPos.y > 0)
            {
                OnSwipeUp();
            }
            else if (_fingerDownPos.y - _fingerUpPos.y < 0)
            {
                OnSwipeDown();
            }
            _fingerUpPos = _fingerDownPos;

        }
        else if (HorizontalMoveValue() > SwipeThreshhold && HorizontalMoveValue() > VerticalMoveValue())
        {
            if (_fingerDownPos.x - _fingerUpPos.x > 0)
            {
                OnSwipeRight();
            }
            else if (_fingerDownPos.x - _fingerUpPos.x < 0)
            {
                OnSwipeLeft();
            }
            _fingerUpPos = _fingerDownPos;

        }
    }

    float VerticalMoveValue()
    {
        return Mathf.Abs(_fingerDownPos.y - _fingerUpPos.y);
    }

    float HorizontalMoveValue()
    {
        return Mathf.Abs(_fingerDownPos.x - _fingerUpPos.x);
    }

    void OnSwipeLeft()
    {
        RunnerMovement.DashLeft();
    }

    void OnSwipeRight()
    {
        RunnerMovement.DashRight();
    }

    void OnSwipeUp()
    {
        RunnerMovement.Jump();
    }

    void OnSwipeDown()
    {
        RunnerMovement.FastLanding();
    }

}