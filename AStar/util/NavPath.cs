﻿using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using VirtualBright.Util;

// This is a fork of Facepunch's NavPath class from the AI testing
public class NavPath
{
    public Vector3 TargetPosition;
    public List<Vector3> Points = new List<Vector3>();

    public bool IsEmpty => Points.Count <= 1;

    public void Update( Vector3 from, Vector3 to )
    {
        if (AStar.AStarDebug)
        {
            DebugDraw(0, 0.5f);
        }
        bool needsBuild = false;

        if ( !TargetPosition.IsNearlyEqual( to, 5 ) )
        {
            TargetPosition = to;
            needsBuild = true;
        }

        if ( needsBuild )
        {
            // var from_fixed = NavMesh.GetClosestPoint( from );
            // var tofixed = NavMesh.GetClosestPoint( to );
            // if (from_fixed.HasValue && tofixed.HasValue)
            {
                Points.Clear();
                // NavMesh.GetClosestPoint( from );
                Points = AStar.GetPath(from + Vector3.Up * 0f, to + Vector3.Up * 0f).Reverse().ToList();
                // NavMesh.BuildPath( from_fixed.Value, tofixed.Value, Points );
                Points.Add(to);
                //Points.Add( NavMesh.GetClosestPoint( to ) );
            }
        }

        if ( Points.Count <= 1 )
        {
            return;
        }

        var deltaToCurrent = from - Points[0];
        var deltaToNext = from - Points[1];
        var delta = Points[1] - Points[0];
        var deltaNormal = delta.Normal;

        if ( deltaToNext.WithZ( 0 ).Length < 20 )
        {
            Points.RemoveAt( 0 );
            return;
        }

        // If we're in front of this line then
        // remove it and move on to next one
        if ( deltaToNext.Normal.Dot( deltaNormal ) >= 1.0f )
        {
            Points.RemoveAt( 0 );
        }
    }

    public float Distance( int point, Vector3 from )
    {
        if ( Points.Count <= point ) return float.MaxValue;

        return Points[point].WithZ( from.z ).Distance( from );
    }

    public Vector3 GetDirection( Vector3 position )
    {
        if (Points.Count == 0)
        {
            return Vector3.Zero;
        }

        if ( Points.Count == 1 )
        {
            return (Points[0] - position).WithZ(0).Normal;
        }

        return (Points[1] - position).WithZ( 0 ).Normal;
    }

    public void DebugDraw( float time, float opacity = 1.0f )
    {
        var draw = Sandbox.Debug.Draw.ForSeconds( time );
        var lift = Vector3.Up * 2;

        draw.WithColor( Color.White.WithAlpha( opacity ) ).Circle( lift + TargetPosition, Vector3.Up, 20.0f );

        int i = 0;
        var lastPoint = Vector3.Zero;
        foreach ( var point in Points )
        {
            if ( i > 0 )
            {
                draw.WithColor( i == 1 ? Color.Green.WithAlpha( opacity ): Color.Cyan.WithAlpha( opacity ) ).Arrow( lastPoint + lift, point + lift, Vector3.Up, 5.0f );
            }

            lastPoint = point;
            i++;
        }
    }
}
