public class AllCreatureDieTransition : Transition
{
    void Update()
    {
        if (Target == null)
            NeedTransit = true;
    }
}
