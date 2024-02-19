using UnityEngine;

public class MoveTowardRunner : MonoBehaviour
{
    [SerializeField] private float _baseMoveSpeed;
    private void Update()
    {
        MoveForward();
    }

    private void MoveForward()
    {
        float totalMoveSpeed = _baseMoveSpeed + RunnerMovement.Instance.RunSpeed;
        transform.Translate(Time.deltaTime * totalMoveSpeed * -transform.forward);
    }
}
