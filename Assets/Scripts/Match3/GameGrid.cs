using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class GameGrid : MonoBehaviour
{

    public int xSize, ySize;
    public float pieceWidht = 0.5f;
    public GameObject[] _pieces;
    private GridItem[,] _itens;

    public static int minItensForMatch = 3;
    public GridItem _currentSelectItem;
    public float delayEntreMatchs = 0.2f;

    public bool canPlay;

    void Start()
    {
        canPlay = true;
        GetPieces();
        FillGrid();
        ClearGrid();
        GridItem.OnMouseOverItemEventhandler += OnMouseOverItem;

    }

    void OnDisable()
    {
        GridItem.OnMouseOverItemEventhandler -= OnMouseOverItem;
    }

    void FillGrid()
    {
        _itens = new GridItem[xSize, ySize];

        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                _itens[x, y] = InstantiatePiece(x, y);
            }
        }
    }

    void ClearGrid()
    {
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                MatchInfo matchInfo = GetMatchInformation(_itens[x, y]);
                if (matchInfo.validMatch)
                {
                    Destroy(_itens[x, y].gameObject);
                    _itens[x, y] = InstantiatePiece(x, y);
                    y--;
                }
            }
        }
    }

    GridItem InstantiatePiece(int x, int y)
    {
        GameObject randomPiece = _pieces[Random.Range(0, _pieces.Length)];
        GridItem newPiece = (Instantiate(randomPiece, new Vector3(x * pieceWidht, y), Quaternion.identity) as GameObject).GetComponent<GridItem>();
        newPiece.OnItemPositionChanged(x, y);
        return newPiece;
    }

    void OnMouseOverItem(GridItem item)
    {
        if (_currentSelectItem == item /*|| !canPlay*/)
        {
            return;
        }

        if (_currentSelectItem == null)
        {
            _currentSelectItem = item;
        }
        else
        {
            float xDiff = Mathf.Abs(item.x - _currentSelectItem.x);
            float yDiff = Mathf.Abs(item.y - _currentSelectItem.y);

            if (xDiff + yDiff == 1)
            {
                //Debug.Log(_currentSelectItem.name);
                StartCoroutine(TryMatch(_currentSelectItem, item));
            }
            else
            {
                Debug.LogError("Não podem movmentar mais 1 deuma unidade");
            }

            _currentSelectItem = null;
        }
    }

    IEnumerator TryMatch(GridItem a, GridItem b)
    {
        canPlay = false;

        yield return StartCoroutine(Swap(a, b));
        MatchInfo matchA = GetMatchInformation(a);
        MatchInfo matchB = GetMatchInformation(b);

        if (!matchA.validMatch && !matchB.validMatch)
        {
            yield return StartCoroutine(Swap(a, b));
            yield break;
        }

        if (matchA.validMatch)
        {
            yield return StartCoroutine(DestroyItens(matchA.match));
            yield return new WaitForSeconds(delayEntreMatchs);
            yield return StartCoroutine(UpdateGridAfterMatch(matchA));
        }
        else if (matchB.validMatch)
        {
            yield return StartCoroutine(DestroyItens(matchB.match));
            yield return new WaitForSeconds(delayEntreMatchs);
            yield return StartCoroutine(UpdateGridAfterMatch(matchB));
        }
        canPlay = true;
    }

    IEnumerator DestroyItens(List<GridItem> itens)
    {
        foreach (GridItem i in itens)
        {
            yield return StartCoroutine(i.transform.Scale(Vector3.zero, 0.03f));
            Destroy(i.gameObject);
        }
    }

    IEnumerator UpdateGridAfterMatch(MatchInfo match)
    {

        if (match.matchStartingY == match.matchEngindY)
        {
            for (int x = match.matchStartingX; x <= match.matchEndingX; x++)
            {
                for (int y = match.matchStartingY; y <= ySize - 1; y++)
                {
                    GridItem upperIndex = _itens[x, y - 1];
                    GridItem current = _itens[x, y];
                    _itens[x, y] = upperIndex;
                    _itens[x, y - 1] = current;

                    _itens[x, y].OnItemPositionChanged(_itens[x, y].x, _itens[x, y].y - 1);
                }
                _itens[x, ySize - 1] = InstantiatePiece(x, ySize - 1);
            }
        }
        else if (match.matchEndingX == match.matchStartingX)
        {
            int matchHeight = 1 + (match.matchEngindY - match.matchStartingY);
            for (int y = match.matchStartingY + matchHeight; y <= ySize - 1; y++)
            {
                GridItem lowerIndex = _itens[match.matchStartingX, y - matchHeight];
                GridItem current = _itens[match.matchStartingX, y];
                _itens[match.matchStartingX, y - matchHeight] = current;
                _itens[match.matchStartingX, y] = lowerIndex;
            }

            for (int y = 0; y < ySize - matchHeight; y++)
            {
                _itens[match.matchStartingX, y].OnItemPositionChanged(match.matchStartingX, y);
            }

            for (int i = 0; i < match.match.Count; i++)
            {
                _itens[match.matchStartingX, (ySize - 1) - i] = InstantiatePiece(match.matchStartingX, (ySize - 1) - i);
            }
        }

        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                MatchInfo matchInfo = GetMatchInformation(_itens[x, y]);
                if (matchInfo.validMatch)
                {
                   // yield return new WaitForSeconds(delayEntreMatchs);
                    yield return StartCoroutine(DestroyItens(matchInfo.match));
                    yield return new WaitForSeconds(delayEntreMatchs);
                    yield return StartCoroutine(UpdateGridAfterMatch(matchInfo));
                }
            }
        }
    }

    IEnumerator Swap(GridItem a, GridItem b)
    {
        Vector3 aux;

        ChangeRigidbodyStatus(false);

        float timeToTransition = 0.1f;

        aux = a.transform.position;

        StartCoroutine(a.transform.Move(b.transform.position, timeToTransition));
        StartCoroutine(b.transform.Move(aux, timeToTransition));

        yield return new WaitForSeconds(timeToTransition + 0.05f);

        SwapIndices(a, b);

        ChangeRigidbodyStatus(true);
    }

    void SwapIndices(GridItem a, GridItem b)
    {
        GridItem tempA = _itens[a.x, a.y];
        _itens[a.x, a.y] = b;
        _itens[b.x, b.y] = tempA;

        int bOldX = b.x;
        int bOldY = b.y;

        b.OnItemPositionChanged(a.x, a.y);
        a.OnItemPositionChanged(bOldX, bOldY);


    }

    List<GridItem> SearchHorizontally(GridItem item)
    {
        List<GridItem> hItem = new List<GridItem> { item };
        int left = item.x - 1;
        int right = item.x + 1;

        while (left >= 0 && _itens[left, item.y].id == item.id)
        {
            hItem.Add(_itens[left, item.y]);
            left--;
        }
        while (right < xSize && _itens[right, item.y].id == item.id)
        {
            hItem.Add(_itens[right, item.y]);
            right++;
        }

        return hItem;
    }

    List<GridItem> SearchVertically(GridItem item)
    {
        List<GridItem> vItem = new List<GridItem> { item };
        int lower = item.y - 1;
        int upper = item.y + 1;

        while (lower >= 0 && _itens[item.x, lower].id == item.id)
        {
            vItem.Add(_itens[item.x, lower]);
            lower--;
        }
        while (upper < ySize && _itens[item.x, upper].id == item.id)
        {
            vItem.Add(_itens[item.x, upper]);
            upper++;
        }

        return vItem;
    }

    MatchInfo GetMatchInformation(GridItem item)
    {
        MatchInfo m = new MatchInfo();
        m.match = null;

        List<GridItem> hMatch = SearchHorizontally(item);
        List<GridItem> vMatch = SearchVertically(item);

        if (hMatch.Count >= minItensForMatch && hMatch.Count > vMatch.Count)
        {
            m.matchStartingX = GetMinimunX(hMatch);
            m.matchEndingX = GetMaximunX(hMatch);

            m.matchStartingY = m.matchEngindY = hMatch[0].y;

            m.match = hMatch;
        }
        else if (vMatch.Count >= minItensForMatch)
        {
            m.matchStartingY = GetMinimunY(vMatch);
            m.matchEngindY = GetMaximunY(vMatch);

            m.matchStartingX = m.matchEndingX = vMatch[0].x;

            m.match = vMatch;
        }

        return m;
    }

    int GetMinimunX(List<GridItem> itens)
    {
        float[] indices = new float[itens.Count];
        for (int i = 0; i < indices.Length; i++)
        {
            indices[i] = itens[i].x;
        }
        return (int)Mathf.Min(indices);
    }

    int GetMaximunX(List<GridItem> itens)
    {
        float[] indices = new float[itens.Count];
        for (int i = 0; i < indices.Length; i++)
        {
            indices[i] = itens[i].x;
        }
        return (int)Mathf.Max(indices);
    }

    int GetMinimunY(List<GridItem> itens)
    {
        float[] indices = new float[itens.Count];
        for (int i = 0; i < indices.Length; i++)
        {
            indices[i] = itens[i].y;
        }
        return (int)Mathf.Min(indices);
    }

    int GetMaximunY(List<GridItem> itens)
    {
        float[] indices = new float[itens.Count];
        for (int i = 0; i < indices.Length; i++)
        {
            indices[i] = itens[i].y;
        }
        return (int)Mathf.Max(indices);
    }

    void GetPieces()
    {
        _pieces = Resources.LoadAll<GameObject>("Prefabs");

        for (int i = 0; i < _pieces.Length; i++)
        {
            _pieces[i].GetComponent<GridItem>().id = i;
        }
    }

    void ChangeRigidbodyStatus(bool status)
    {
        foreach (GridItem g in _itens)
        {
            g.GetComponent<Rigidbody2D>().isKinematic = !status;
        }
    }
}
