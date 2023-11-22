using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class PlayerController : MonoBehaviour
{
    public Grid _grid;
    public float _speed = 5.0f;


    Vector3Int _cellPos = Vector3Int.zero;
    bool _isMoving = false;

    Animator _animator;
    Define.MoveDir _dir = Define.MoveDir.Down;
    public MoveDir Dir
    {
        get { return _dir; }
        set
        {
            if (_dir == value)
            {
                return;
            }
            switch (value)
            {
                case MoveDir.Up:
                    transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    _animator.Play("WALK_BACK");
                    break;
                case MoveDir.Down:
                    transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    _animator.Play("WALK_FRONT");
                    break;
                case MoveDir.Left:
                    transform.localScale= new Vector3(-1.0f,1.0f,1.0f);
                    _animator.Play("WALK_RIGHT");
                    break;
                case MoveDir.Right:
                    transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    _animator.Play("WALK_RIGHT");
                    break;
                case MoveDir.None:
                    if(_dir == MoveDir.Up)
                    {
                        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                        _animator.Play("IDLE_BACK");
                    }
                    else if (_dir == MoveDir.Down)
                    {
                        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                        _animator.Play("IDLE_FRONT");
                    }
                    else if (_dir == MoveDir.Left)
                    {
                        transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                        _animator.Play("IDLE_RIGHT");
                    }
                    else
                    {
                        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                        _animator.Play("IDLE_RIGHT");
                    }
                    break;
            }
            _dir = value;
        }
    }

    void Start()
    {
        _animator = GetComponent<Animator>();
        Vector3 pos = _grid.CellToWorld(_cellPos) + new Vector3(0.5f, 0.5f);
        transform.position = pos;
    }

    void Update()
    {
        GetDirInput();
        UpdatePosition();
        UpdateIsMoving();
    }
    void UpdatePosition()
    {
        if (_isMoving == false)
        {
            return;
        }
        Vector3 destPos = _grid.CellToWorld(_cellPos) + new Vector3(0.5f, 0.5f);
        Vector3 moveDir = destPos - transform.position;
        //µµÂø¿©ºÎ
        float dist = moveDir.magnitude;
        if (dist < _speed * Time.deltaTime)
        {
            transform.position = destPos;
            _isMoving = false;
        }
        else
        {
            transform.position += moveDir.normalized * _speed * Time.deltaTime;
            _isMoving = true;
        }
    }

    void UpdateIsMoving()
    {
        if (_isMoving == false)
        {
            switch (_dir)
            {
                case MoveDir.None:
                    break;
                case MoveDir.Up:
                    _cellPos += Vector3Int.up;
                    _isMoving = true;
                    break;
                case MoveDir.Down:
                    _cellPos += Vector3Int.down;
                    _isMoving = true;
                    break;
                case MoveDir.Left:
                    _cellPos += Vector3Int.left;
                    _isMoving = true;
                    break;
                case MoveDir.Right:
                    _cellPos += Vector3Int.right;
                    _isMoving = true;
                    break;
            }
        }
    }

    void GetDirInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Dir = MoveDir.Up;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            Dir = MoveDir.Down;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            Dir = MoveDir.Left;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Dir = MoveDir.Right;
        }
        else
        {
            Dir = MoveDir.None;
        }
    }
}
