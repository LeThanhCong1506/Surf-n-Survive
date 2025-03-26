using UnityEngine;

public class PlayerPowerUp
{
    private PlayerController m_playerController;
    private PlayerAnimation m_playerAnimation;

    public PlayerPowerUp(PlayerController playerController, PlayerAnimation playerAnimation)
    {
        m_playerController = playerController;
        m_playerAnimation = playerAnimation;
    }

    public void HandleTrigger(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Speed"))
        {
            AudioManager.Instance.PlayHitSpeedUpSound();
            m_playerController.AteSpeed = true;
            m_playerController.StartCoroutine(m_playerAnimation.WaitForStartSpeedPowerUp());
        }
    }
}
