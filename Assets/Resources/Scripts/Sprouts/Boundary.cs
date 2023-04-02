using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary 
{
    // public List<Dot> dots { get; } = new List<Dot>();
    public Region refRegion { get; }
    public List<Tuple<string, string>> segments { get; } = new List<Tuple<string, string>>();

    public int boundaryType { get; private set; } = 0; // The type of the boundary, whether it is interior, exterior, or neither.

    // Make a starting boundary from 1 dot. There are no segments in starting boundaries.
    /*public Boundary(Dot singleDot) 
    { 
        dots.Add(singleDot);
    }*/

    public Boundary(Region region)
    {
        refRegion = region;
    }

    // Make a new Boundary from 2 existing ones. When connecting 2 boundaries, you NEED the 2 segments from dot connection when making a 2 boundary move
    public Boundary(Region region, Boundary b1,  Boundary b2) 
    {
        // Add dots from both b1 and b2
        /* foreach(Dot dot in b1.dots)
         {
             dots.Add(dot);
         }
         foreach (Dot dot in b2.dots)
         {
             dots.Add(dot);
         } */

        refRegion = region;

        // Add segments from b1 and b2
        if (b1.segments != null)
        {
            foreach (Tuple<string, string> segment in b1.segments)
            {
                segments.Add(segment);
            }
        }
        if (b2.segments != null)
        {
            foreach (Tuple<string, string> segment in b2.segments)
            {
                segments.Add(segment);
            }
        }
    }

    public void AddSegment(Tuple<string,string> newSegment)
    {
        segments.Add(newSegment);
    }

    /* Sets the type of the boundary. Interior/Exterior are for boundaries created by one region moves.
     * 0 = neither
     * 1 = exterior
     * 2 = interior
    */
    public void SetType(int type)
    {
        if ((type < 0) || (type > 2))
        {
            Debug.LogError("Cannot set boundaru type to: " +  type);
            return;
        }
        boundaryType = type;
    }
}
