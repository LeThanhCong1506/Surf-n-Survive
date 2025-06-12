using System.Collections;
using UnityEngine;

/// <summary>
/// Handles player collision events, including interactions with obstacles and the ground.
/// Triggers appropriate animations and game state changes based on collision types.
/// </summary>
public class PlayerCollision
{
    private PlayerController m_playerController;
    private Animator m_animation;
    private PlayerAnimation m_playerAnimation;

    public PlayerCollision(PlayerController playerController,Animator animation, PlayerAnimation playernAnimation)
    {
        m_playerController = playerController;
        m_animation = animation;
        m_playerAnimation = playernAnimation;
    }

    public void HandleCollision(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            AudioManager.Instance.PlayDeathSound();
            m_playerController.Gameover = true;
            m_animation.SetBool("Fall", true);
            m_animation.SetBool("Jump", false);
            m_animation.SetInteger("Bend", 3);
            collision.gameObject.GetComponent<EdgeCollider2D>().enabled = false;
            m_playerController.StartCoroutine(m_playerAnimation.PLayAnimationDie());
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            m_playerController.IsOnGround = true;
            m_playerController.StartCoroutine(WaitForGround());
        }
    }

    IEnumerator WaitForGround()
    {
        yield return new WaitForSeconds(0.01f);
        m_animation.SetBool("Jump", false);
    }
}
