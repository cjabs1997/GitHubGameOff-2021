using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Once I find my examples I'll clean this up and make more sense of it

/// <summary>
/// Base class for Steering Behaviors. These will be used to "steer" entities to make their movement
/// feel more believable and smooth. You should NEVER create an asset from this class, only the extensions
/// of it. 
/// 
/// The goal with these will be to make offshoots of each of the behaviors and then plug them into specific
/// bug Scriptable Objects.
/// </summary>
public abstract class SteeringBehavior : ScriptableObject
{

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    protected abstract Vector2 ForceToAdd();

    // This is OMEGA gormless until I actually get things in place but this will work for now.
    public abstract void Move();
}
