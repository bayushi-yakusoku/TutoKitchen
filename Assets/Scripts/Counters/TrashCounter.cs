using System;

public class TrashCounter : BaseCounter {

    public static event EventHandler OnAnyTrashSomething;

    public new static void ResetStaticData() {
        OnAnyTrashSomething = null;
    }

    public override void Interact(Player player) {
        if (player.HasPresentedObject()) {
            player.GetPresentedObject().DestroySelf();
            OnAnyTrashSomething?.Invoke(this, EventArgs.Empty);
        }
    }
}
