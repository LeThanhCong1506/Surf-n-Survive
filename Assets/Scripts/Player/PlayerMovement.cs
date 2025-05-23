using UnityEngine;

/// <summary>
/// Handles the player's movement, including jumping, bending, and updating the distance traveled.
/// Integrates with the PlayerController to manage movement-related states and animations.
/// </summary>
public class PlayerMovement
{
    private PlayerController playerController;
    private Rigidbody2D playerRb;
    private float endHeight;

    public PlayerMovement(PlayerController controller, Rigidbody2D rb)
    {
        playerController = controller;
        playerRb = rb;
    }

    public void UpdateDistance(ref float distance, ref int distanceToInt
        , float speedPowerUpDuration, bool ateSpeed)
    {
        if (!ateSpeed)
            distance += Time.deltaTime * speedPowerUpDuration;
        else
            distance += Time.deltaTime * speedPowerUpDuration * 2;
        distanceToInt = Mathf.RoundToInt(distance);
    }

    public void HandleJump()
    {
        AudioManager.Instance.PlayJumpSound();
        playerController.GetComponent<Animator>().SetBool("Jump", true);
        playerRb.AddForce(Vector2.up * playerController.JumpForce);
        playerController.IsOnGround = false;
    }

    public void HandleHoldBend()
    {
        AudioManager.Instance.PlayBendDownSound();
        playerController.WaitTurn = true;
        playerController.GetComponent<Animator>().SetInteger("Bend", 2);
        endHeight = playerController.GetComponent<BoxCollider2D>().size.y;
        playerController.GetComponent<BoxCollider2D>().size =
            new Vector2(playerController.GetComponent<BoxCollider2D>().size.x, endHeight * 0.33333f * 2);
        playerController.GetComponent<BoxCollider2D>().offset =
            new Vector2(playerController.GetComponent<BoxCollider2D>().offset.x, -0.6f);
    }

    public void HandleReleaseBend()
    {
        playerController.GetComponent<Animator>().SetInteger("Bend", 3);
        playerController.GetComponent<BoxCollider2D>().size =
            new Vector2(playerController.GetComponent<BoxCollider2D>().size.x, endHeight);
        playerController.GetComponent<BoxCollider2D>().offset =
            new Vector2(playerController.GetComponent<BoxCollider2D>().offset.x, 0);
        playerController.WaitTurn = false;
    }
}
