public class BeginMoveTransition : Transition
{
    private void Update()
    {
        if (Spawner.IsWaveReady == true)
            NeedTransit = true;
    }
}
