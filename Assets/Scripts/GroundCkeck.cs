using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCkeck : MonoBehaviour
{
    [SerializeField] private Player pl;
    private void OnTriggerEnter2D(Collider2D collision) { if (pl.isJumping && collision.tag == "Map") pl.isJumping = false; pl.anim.SetTrigger("grounded"); }
}
