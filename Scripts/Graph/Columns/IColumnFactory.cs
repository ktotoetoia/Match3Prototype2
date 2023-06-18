using System.Collections.Generic;
using UnityEngine;

public interface IColumnFactory
{
    public IColumn Create(List<IPieceContainer> containers);
}