using UnityEngine;

public class RunnerCollisionCheck : MonoBehaviour
{
    private RunnerMovement RunnerMovement => RunnerMovement.Instance;
    private GameManager GameManager => GameManager.Instance;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<TrainRamp>() != null)
        {
            if (RunnerMovement.IsDashing)
            {
                RunnerMovement.BounceBack();
                GameManager.HarmPlayer();
            }
        }

        if (collision.gameObject.GetComponent<TrainBody>() != null)
        {
            if (RunnerMovement.transform.position.y >= 3f) { return; }

            RunnerMovement.BounceBack();
            GameManager.HarmPlayer();
        }

        if (collision.gameObject.GetComponent<SlideObstacle>() != null)
        {
            if(RunnerMovement.IsSliding) { return; }
            SlideObstacleSpawner.Instance.Pool.Release(collision.gameObject.GetComponent<SlideObstacle>());
            GameManager.HarmPlayer();
        }

        if (collision.gameObject.GetComponent<JumpObstacle>() != null)
        {
            GameManager.HarmPlayer();
            JumpObstacleSpawner.Instance.Pool.Release(collision.gameObject.GetComponent<JumpObstacle>());
        }

        if (collision.gameObject.GetComponent<BlockObstacle>() != null)
        {
            GameManager.HarmPlayer();
            BlockObstacleSpawner.Instance.Pool.Release(collision.gameObject.GetComponent<BlockObstacle>());
        }

        if (collision.gameObject.GetComponent<MovingObstacle>() != null)
        {
            GameManager.HarmPlayer();
            MovingObstacleSpawner.Instance.Pool.Release(collision.gameObject.GetComponent<MovingObstacle>());
        }

        if (collision.gameObject.GetComponent<Coin>() != null)
        {
            GameManager.CollectCoin();
            CoinSpawner.Instance.Pool.Release(collision.gameObject.GetComponent<Coin>());
        }
    }
}
