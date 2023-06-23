using System.Collections.Generic;

public interface IMatchGetter
{
    public List<IPieceContainer> GetVerticalMatchs(IPieceContainer container,  bool anyColor = false);
    public List<IPieceContainer> GetHorizontalMatchs(IPieceContainer container, bool anyColor = false);
    List<IPieceContainer> GetSquareMatch(IPieceContainer center, int radius);
}