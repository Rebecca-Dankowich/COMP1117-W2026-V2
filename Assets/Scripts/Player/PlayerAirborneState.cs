using UnityEngine;

public class PlayerAirborneState : PlayerBaseState
{
    public override void EnterState(Player player)
    {
        player.anim.SetBool("IsGrounded", false);
    }

    public override void UpdateState(Player player)
    {
        player.anim.SetFloat("VerticalVelocity", player.rBody.linearVelocityY);

        // Transition back to ground
        if(player.CheckGrounded() && player.rBody.linearVelocityY <= 0.1f)
        {
            player.SwitchState(player.GroundedState);
        }
    }

    public override void FixedUpdateState(Player player)
    {
        player.rBody.linearVelocity = new Vector2(player.moveInput.x * player.data.moveSpeed, player.rBody.linearVelocityY);

        player.FlipSprite(player.moveInput.x);
    }

    public override void OnJumpPressed(Player player)
    {
        // Double jump
        if(player.jumpsRemaining > 0)
        {
            player.rBody.linearVelocity = new Vector2(player.rBody.linearVelocityX, player.data.jumpForce);

            player.anim.SetTrigger("Jump");

            AudioManager.Instance.PlayJump();
            player.jumpsRemaining--;
        }
    }

    public override void ExitState(Player player) { }
}