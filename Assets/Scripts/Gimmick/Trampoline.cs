using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public float bouncePower = 12F; // ’µ‚Ë•Ô‚è—Í

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();

        if(player != null )
        {
            player.Bounce(bouncePower);
        }
    }
}
