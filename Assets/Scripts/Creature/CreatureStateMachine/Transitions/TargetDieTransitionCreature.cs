public class TargetDieTransitionCreature : TransitionCreature
{
    private void Update()
    {
        if (Target.IsAlive == false)
            NeedTransit = true;
    }
}
