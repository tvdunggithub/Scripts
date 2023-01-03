using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//Giới hạn di chuyển, chuyển đổi trạng thái bắn.
public class Player : MonoBehaviour
{
    Vector2 rawInput;
    Vector2 minBounds;
    Vector2 maxBounds;
    Vector2 newPos;
    
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float paddingLeftRight = 0.5f;
    [SerializeField] float paddingTopBottom = 2f;
    Shooter shooter;
    public GameObject shield;

    void Awake()
    {
        shooter = GetComponent<Shooter>();
    }

    void Start()
    {
        InitBounds();
        shield.SetActive(false);
    }


    void Update()
    {
        Move();
    }

    //Chuyển tọa độ sang world point để giới hạn di chuyển
    void InitBounds()
    {
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
    }

    /*
    -Chuyển tốc độ rawInput nhận dc dự người dùng thành tốc độ delta(nhanh hơn)
    -Tạo ra Vector 2 mới để giới hạn di chuyển của player (newPos) bằng Mathf.Clamp
    (thông tin truyền vào bao gồm vị trí hiện tại tức giá trị cần kẹp, x-y trái phải 
    và trên dưới tính theo trục tọa độ với điểm gốc(0,0) là vị trí
    khi bấm start của player)
    */
    private void Move()
    {
        Vector2 delta = rawInput * moveSpeed * Time.deltaTime;
        newPos.x = Mathf.Clamp(transform.position.x + delta.x,
            minBounds.x + paddingLeftRight, maxBounds.x - paddingLeftRight);
        newPos.y = Mathf.Clamp(transform.position.y + delta.y,
            minBounds.y + paddingTopBottom, maxBounds.y - paddingTopBottom);
        transform.position = newPos;
    }

    //Input từ người dùng sẽ đi vào OnMove và chuyển thành Vector2
    //Từ Vector2 nhận được => xử lý thêm và update hình ảnh
    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }

    //Chuyển trạng thái của shooter
    void OnFire(InputValue value)
    {
        if(shooter != null)
        {
            shooter.isFiring = value.isPressed;
        }
    }

}
