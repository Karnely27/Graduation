public class AllEnemiesDieTransition : TransitionCreature
{
    private void Update()
    {
        if (Target == null)
            NeedTransit = true;
    }
}
