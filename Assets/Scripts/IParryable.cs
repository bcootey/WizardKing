using UnityEngine;

public interface IParryable
{
    bool IsParryable { get; set; }
    
    bool IsParried { get; set; }
    void SetIsParryable();
    void SetIsNotParryable();
}
