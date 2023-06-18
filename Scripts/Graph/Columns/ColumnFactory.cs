using System.Collections.Generic;
using UnityEngine;

public class ColumnFactory : IColumnFactory
{
    public IColumn Create(List<IPieceContainer> containers)
    {
        return new Column(containers);
    }
}