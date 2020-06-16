using Model.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_infantsSchool.Ext
{
    public static class RecursionHelper
    {
        public static void LoopToAppendChildren(List<ActionTreeDto> all, ActionTreeDto curItem, int pid)
        {
            var subItems = all.Where(ee => ee.Pid == curItem.Id).ToList();

            if (subItems.Count > 0)
            {
                curItem.Children = new List<ActionTreeDto>();
                curItem.Children.AddRange(subItems);
            }
            else
            {
                curItem.Children = null;
            }

            foreach (var subItem in subItems)
            {
                LoopToAppendChildren(all, subItem, pid);
            }
        }
       
    }
}
