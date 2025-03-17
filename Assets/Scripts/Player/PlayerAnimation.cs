using System.Collections;
using UnityEngine;

public class PlayerAnimation
{
    private PlayerController m_playerController;
    private Animator m_playerAnim;
    private GameManager m_gameManager;

    public PlayerAnimation(PlayerController controller, Animator playerAnimator, GameManager gameManager)
    {
        m_playerController = controller;
        m_playerAnim = playerAnimator;
        m_gameManager = gameManager;
    }

    public IEnumerator PLayAnimationDie()
    {
        float offsetIncrement = -0.12f;
        float waitTime = 0.0575f;
        BoxCollider2D boxCollider = m_playerController.gameObject.GetComponent<BoxCollider2D>();

        for (int i = 1; i <= 35; i++)
        {
            boxCollider.offset = new Vector2(boxCollider.offset.x, i * offsetIncrement);
            yield return new WaitForSeconds(waitTime);
        }

        m_playerAnim.SetBool("Fall", false);
        m_playerController.gameObject.SetActive(false);
    }

    public IEnumerator WaitForStartSpeedPowerUp()
    {
        m_playerAnim.SetBool("Speed", true);
        yield return new WaitForSeconds(0.2f);
        m_gameManager.DeactivateEdgeCollider2D();
        m_gameManager.IncreaseAllMoveLeftSpeed(10, 0);
        m_playerController.StartCoroutine(WaitForEndSpeedPowerUp());
    }

    private IEnumerator WaitForEndSpeedPowerUp()
    {
        yield return new WaitForSeconds(4);
        m_gameManager.IncreaseAllMoveLeftSpeed(-4, 0);
        yield return new WaitForSeconds(2);
        m_gameManager.IncreaseAllMoveLeftSpeed(-3, 0);
        m_playerAnim.SetBool("Speed", false);
        yield return new WaitForSeconds(1);
        m_gameManager.IncreaseAllMoveLeftSpeed(-2, 0);
        yield return new WaitForSeconds(1);
        m_gameManager.IncreaseAllMoveLeftSpeed(-1, 0);
        m_gameManager.ActivateEdgeCollider2D();
        m_playerController.AteSpeed = false;
    }
}
