using Mirror;
using UnityEngine;

public class PlayerInput : NetworkBehaviour
{
    [SerializeField] private PlayerMovement _mover;
    [SerializeField] private PlayerAnimationController _animatiomController;
 
    private void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        _mover.Move(horizontal, vertical);
        _mover.Sprint(Input.GetMouseButton(0));


        _animatiomController.PlayAnimation(horizontal, vertical, _mover);
    }
}
