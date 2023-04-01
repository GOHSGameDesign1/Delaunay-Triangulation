using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class SproutsHelper
{
    public static bool CheckSameRegion(List<Dot> dots)
    {
        if(dots == null || dots.Count == 0) return false;

        Dot firstDot = dots[0];

        foreach(Dot dot in dots.Skip(1))
        {
            if(!firstDot.availableRegions.Any(x => dot.availableRegions.Contains(x))) return false;
        }

        return true;
    }

    public static bool CheckSameBoundary(List<Dot> dots)
    {
        if (dots == null || dots.Count == 0) return false;

        Dot firstDot = dots[0];

        foreach (Dot dot in dots.Skip(1))
        {
            if (!firstDot.availableBoundaries.Any(x => dot.availableBoundaries.Contains(x))) return false;
        }

        return true;
    }

    public static List<Dot> GetDotsOfSameBoundary(Boundary searchBound) 
    { 
        List<Dot> result = new List<Dot>();

        foreach(Dot dot in SproutsManager.dots)
        {
            if (dot.availableBoundaries.Contains(searchBound))
            {
                result.Add(dot);
            }
        }

        return result;
    }

    public static List<Region> GetSharedRegions(List<Dot> dots)
    {
        List<Region> result = new List<Region>();

        Dot firstDot = dots.First();

        foreach (Region compareRegion in firstDot.availableRegions)
        {
            if (dots.All(x => x.availableRegions.Contains(compareRegion)))
            {
                result.Add(compareRegion);
            }
        }

        return result;
    }

}
