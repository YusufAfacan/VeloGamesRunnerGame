using DG.Tweening;
using UnityEngine;
using UnityEngine.Pool;

public class Coin : Spawnable
{
    private ObjectPool<Coin> _pool;


    private void Start()
    {
        SpinAnimation();
    }


    private void SpinAnimation()
    {
        transform.GetChild(0).transform.DORotate(new Vector3(0,360,0), 2f, RotateMode.FastBeyond360).
            SetLoops(-1, LoopType.Restart).SetRelative().SetEase(Ease.Linear);
    }

    public void SetPool(ObjectPool<Coin> pool)
    {
        _pool = pool;
    }

    private void Update()
    {
        if (transform.position.z < minZPos)
        {
            _pool.Release(this);
        }
    }
}
