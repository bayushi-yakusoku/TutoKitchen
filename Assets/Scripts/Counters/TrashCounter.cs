using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter {

    public static event EventHandler OnAnyTrashSomething;

    public override void Interact(Player player) {
        if (player.HasPresentedObject()) {
            player.GetPresentedObject().DestroySelf();
            OnAnyTrashSomething?.Invoke(this, EventArgs.Empty);
        }
    }
}
