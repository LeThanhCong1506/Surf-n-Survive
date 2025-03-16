using UnityEngine;

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
        playerController.GetComponent<Animator>().SetBool("Jump", true);
        playerRb.AddForce(Vector2.up * playerController.JumpForce);
        playerController.IsOnGround = false;
    }

    public void HandleHoldBend()
    {
        playerController.WaitTurn = true;
        playerController.GetComponent<Animator>().SetInteger("Bend", 2);
        endHeight = playerController.GetComponent<BoxCollider2D>().size.y;
        playerController.GetComponent<BoxCollider2D>().size =
            new Vector2(playerController.GetComponent<BoxCollider2D>().size.x, endHeight * 0.33333f * 2);
        playerController.GetComponent<BoxCollider2D>().offset =
            new Vector2(playerController.GetComponent<BoxCollider2D>().offset.x, -1);
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
