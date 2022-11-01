﻿using System.Reflection;
using PISWF.domain.registermc.service;

namespace PISWF.domain.registermc.model.entity;

public class RegisterMcComparer<T> : IComparer<T>
{
    private SortParameters<RegisterMC> _sortParameters;

    
    public RegisterMcComparer(SortParameters<RegisterMC> sortParameters)
    {
        _sortParameters = sortParameters;
    }

    public int Compare(T x, T y)
    {
        if (ReferenceEquals(x, y)) return 0;
        if (ReferenceEquals(null, y)) return 1;
        if (ReferenceEquals(null, x)) return -1;

        foreach (var sortParameter in _sortParameters.NameSortColumns)
        {
            var property = sortParameter.Property;
            try
            {
                var valueX = property.GetValue(x) as IComparable;
                var valueY = property.GetValue(y) as IComparable;

                var result = (valueX).CompareTo(valueY);
                if (result != 0)
                    return sortParameter.IsAscending ? result : -result;
            }
            catch (Exception e)
            {
                throw new TypeAccessException(
                    $"Недопустимое свойство для сортировки {property.Name}, Объект соритовки {typeof(RegisterMC)}");
            }
        }

        return 0;
    }
}