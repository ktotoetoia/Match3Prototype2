using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Piece : MonoBehaviour, IPiece
{
    [field: SerializeField] public PieceColor Color { get; private set; }
    [SerializeField] private float speed = 0.1f;
    [SerializeField] private float stopDistance = 0.01f;
    public bool Arrived { get { return positions.Count == 0; } }

    private float acceleration = 1;
    private Queue<Vector2> positions = new Queue<Vector2>();

    private void Update()
    {
        MoveToNextPosition();
    }

    private void MoveToNextPosition()
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
        positions.Enqueue(position);
    }

    public void SetPosition(Vector2 position)
    {
        transform.position = position;
    }

    public void OnMatch()
    {
        StartCoroutine(DestroyAfterMove());
    }

    private IEnumerator DestroyAfterMove()
    {
        yield return new WaitUntil(() => positions.Count == 0);

        Destroy(gameObject);
    }
}