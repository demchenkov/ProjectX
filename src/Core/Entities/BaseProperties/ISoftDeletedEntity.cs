using System;

namespace Core.Entities.BaseProperties
{
    public interface ISoftDeletedEntity
    {
        DateTime DeletedOn { get; }
        int DeletedBy { get; }
    }
}