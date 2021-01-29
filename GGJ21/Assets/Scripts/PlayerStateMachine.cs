using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateTransition
{
    MovementUp,
    MovementDown,
    Jump,
    Climb,
    Land,
    ExitClimb
}

public abstract class State
{
    public abstract void HandleStateTransition(Player p, StateTransition transition);
}

public class Idle : State
{
    public override void HandleStateTransition(Player p, StateTransition transition)
    {
        switch (transition)
        {
            case StateTransition.MovementDown:
                p.SetNewState(new Walking());
                break;
            case StateTransition.Climb:
                if (p.CanClimb())
                {
                    p.StartClimb();
                    p.SetNewState(new Climbing());
                }
                break;
            case StateTransition.Jump:
                if (p.IsGrounded())
                {
                    p.Jump();
                    p.SetNewState(new Jumping());
                }
                break;
        }
    }
}

public class Walking : State
{
    public override void HandleStateTransition(Player p, StateTransition transition)
    {
        switch (transition)
        {
            case StateTransition.MovementUp:
                p.SetNewState(new Idle());
                break;
            case StateTransition.Jump:
                if (p.IsGrounded())
                {
                    p.Jump();
                    p.SetNewState(new Jumping());
                }
                break;
            case StateTransition.Climb:
                if (p.CanClimb())
                {
                    p.StartClimb();
                    p.SetNewState(new Climbing());
                }
                break;
        }
    }
}

public class Jumping : State
{
    public override void HandleStateTransition(Player p, StateTransition transition)
    {
        switch (transition)
        {
            case StateTransition.Land:
                p.SetNewState(new Idle());
                break;
            case StateTransition.Climb:
                if (p.CanClimb())
                {
                    p.StartClimb();
                    p.SetNewState(new Climbing());
                }
                break;
        }
    }
}

public class Climbing : State
{
    public override void HandleStateTransition(Player p, StateTransition transition)
    {
        switch (transition)
        {
            case StateTransition.MovementUp:
                p.StopClimbing();
                p.SetNewState(new Idle());
                break;
        }
    }
}