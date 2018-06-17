using System;
using System.Collections.Generic;
using System.Web.Mvc;
using StudentTestingSystem.Services.TransportModels.Admin.Group.Response;

namespace StudentTestingSystem.Extensions
{
    public static class SelectListExtension
    {
        public static List<SelectListItem> ToStudentSelectListItems(this IEnumerable<GroupListDto> groups, Guid? currentGroupId)
        {
            var items = new List<SelectListItem>();
            foreach (var group in groups)
            {
                if (group.Id == currentGroupId)
                {
                    items.Add(new SelectListItem
                    {
                        Selected = true,
                        Text = group.Number.ToString(),
                        Value = group.Id.ToString()
                    });
                }
                else
                {
                    items.Add(new SelectListItem
                    {
                        Selected = false,
                        Text = group.Number.ToString(),
                        Value = group.Id.ToString()
                    });
                }
            }
            return items;
        }

        public static List<SelectListItem> ToGroupSelectListItems(this IEnumerable<GroupListDto> groups, Guid? currentGroupId)
        {
            var items = new List<SelectListItem>();
            foreach (var group in groups)
            {
                if (group.Id == currentGroupId)
                {
                    items.Add(new SelectListItem
                    {
                        Selected = true,
                        Text = group.Number.ToString(),
                        Value = group.Number.ToString()
                    });
                }
                else
                {
                    items.Add(new SelectListItem
                    {
                        Selected = false,
                        Text = group.Number.ToString(),
                        Value = group.Number.ToString()
                    });
                }
            }
            return items;
        }
    }
}