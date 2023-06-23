using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Piece : MonoBehaviour, IPiece
{
    [field: SerializeField] public PieceColor Color { get; protected set; }

    [SerializeField] protected float speed = 0.1f;
    [SerializeField] protected float stopDistance = 0.01f;

    public virtual PieceType Type { get; protected set; }

    public bool Arrived { get { return positions.Count == 0; } }

    protected float acceleration = 1;
    protected Queue<Vector2> positions = new Queue<Vector2>();

    protected bool isReadyToDestroy;

    private void Update()
    {
        MoveToNextPosition();
    }

    protected void MoveToNextPosition()
    {
        if (positions.Count == 0)
            return;

        Vector2 position = positions.Peek();

        transform.position = Vector2.MoveTowards(transform.position, position, acceleration * Time.deltaTime);

        if (Vector2.Distance(transform.position, position) < stopDistance) 
            positions.Dequeue();

        acceleration = positions.Count == 0 ? speed : acceleration + speed;
    }

    public void AddPosition(Vector2 position)
    {
        if (!isReadyToDestroy)
        {
            positions.Enqueue(position);
        }
    }

    public void SetPosition(Vector2 position)
    {
        transform.position = position;
    }

    public virtual void OnMatch(IMatchInfo matchInfo,IPieceContainer container)
    {
        StartCoroutine(DestroyAfterMove());
    }

    protected IEnumerator DestroyAfterMove()
    {
        isReadyToDestroy = true;
        yield return new WaitUntil(() => positions.Count == 0);

        Destroy(gameObject);
    }
}