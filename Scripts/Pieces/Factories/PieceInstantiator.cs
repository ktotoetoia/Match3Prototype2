using UnityEngine;
using System.Collections.Generic;

public class PieceInstantiator : MonoBehaviour, IPieceInstantiator
{
    [SerializeField] private List<GameObject> pieces;

    public IPiece Instantiate(PieceColor color, PieceType type)
    {
        GameObject piece = Instantiate(GetPiece(color,type));

        return piece.GetComponent<IPiece>();
    }

    public GameObject GetPiece(PieceColor color, PieceType type)
    {
        return pieces.Find(x => x.TryGetComponent(out IPiece piece) && piece.Color == color && piece.Type == type);
    }
}   