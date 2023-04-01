using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class SproutsHelper
{
    public static bool CheckAllSameRegion(List<Dot> dots)
    {
        if(dots == null || dots.Count == 0) return false;

        Dot firstDot = dots[0];

        foreach(Dot dot in dots.Skip(1))
        {
            if(!firstDot.availableRegions.Any(x => dot.availableRegions.Contains(x))) return false;
        }

        return true;
    }

    public static bool CheckAllSameBoundary(List<Dot> dots)
    {
        if (dots == null || dots.Count == 0) return false;

        Dot firstDot = dots[0];

        foreach (Dot dot in dots.Skip(1))
        {
            if (!firstDot.availableBoundaries.Any(x => dot.availableBoundaries.Contains(x))) return false;
        }

        return true;
    }

    public static bool CheckAnySameBoundary(List<Dot> dots)
    {
        if (dots == null || dots.Count == 0) return false;

        Dot firstDot = dots[0];

        foreach(Dot dot in dots.Skip(1))
        {
            if (CheckIfShareSameBoundary(firstDot, dot))
            {
                return true;
            }
        }

        return false;
    }

    public static bool CheckIfShareSameBoundary(Dot dot1, Dot dot2)
    {
        bool result = dot1.availableBoundaries.Any(x => dot2.availableBoundaries.Contains(x));
        return result;
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
