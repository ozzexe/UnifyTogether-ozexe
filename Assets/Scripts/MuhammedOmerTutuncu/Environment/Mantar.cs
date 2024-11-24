using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mantar : Environment
{
    private float jumpForce;
    protected override void EtkilesimeGir(Collider player)
    {
        CharacterController characterController = player.GetComponent<CharacterController>();
        jumpForce = enValueSO.jumpForce;
        if (characterController != null)
        {
            Vector3 jumpVelocity = new Vector3(0, jumpForce, 0);
            characterController.Move(jumpVelocity * Time.deltaTime);

            //Animasyon, Ses ve VFX efektleri eklenecektir.
        }
    }
}
