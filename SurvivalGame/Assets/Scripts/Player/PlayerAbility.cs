using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
    
    public float cooldownTime;
    public float activeTime;
    public enum PlayerAbilityKey { Q, R, X, C};
    [SerializeField] private PlayerAbilityKey playerAbilityKey;
    private void Update()
    {
        switch (playerAbilityKey)
        {
            case PlayerAbilityKey.Q:
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    Activate();
                    
                }
                break;
            case PlayerAbilityKey.R:
                if (Input.GetKeyDown(KeyCode.R))
                {
                    Activate();

                }
                break;
            case PlayerAbilityKey.X:
                if (Input.GetKeyDown(KeyCode.X))
                {
                    Activate();

                }
                break;
            case PlayerAbilityKey.C:
                if (Input.GetKeyDown(KeyCode.C))
                {
                    Activate();
                    return;
                }
                break;
            
        }
    }
    public virtual void Activate()
    {

    }
}
