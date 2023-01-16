public class BeginMoveTransitionCreature : TransitionCreature
{
    private void Update()
    {
        if (Spawner.IsWaveReady == true)
            NeedTransit = true;
    }
}
