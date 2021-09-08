using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : SingletonMonoBehaviour<CheckpointManager>
{
    private Checkpoint _currentCheckpoint;

    public void SetNewCheckPoint(Checkpoint checkpoint)
    {
        this._currentCheckpoint = checkpoint;
        EventHandler.CallCheckpointUpdatedEvent(checkpoint);
    }

    public Checkpoint GetLastCheckpoint()
    {
        return _currentCheckpoint;
    }
}
