using System;

public interface IHasProgress {
    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;

    public class OnProgressChangedEventArgs : EventArgs {
        public float progressNormalized;
    }

    public bool IsFlashing() { return false; }
}
