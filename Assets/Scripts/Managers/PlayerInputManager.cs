using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is a simple input method
/// </summary>
public class PlayerInputManager : MonoBehaviour
{
    //Singleton class
    public static PlayerInputManager Instance { get; private set; }

    [SerializeField] KeyCode key_up, key_down, key_left,
                            key_right, key_shoot, key_escape;

    bool pressed_up, pressed_down, pressed_left,
        pressed_right, pressed_shoot, pressed_escape;

    public bool GetEscapePrompt()
    {
        return pressed_escape;
    }
    public bool GetShootPrompt()
    {
        return pressed_shoot;
    }
    public int GetHorizontalAxis()
    {
        int x = 0;
        if (pressed_left) x -= 1;
        if (pressed_right) x += 1;
        return x;
    }

    public int GetVerticalAxis()
    {
        int y = 0;
        if (pressed_down) y -= 1;
        if (pressed_up) y += 1;
        return y;
    }

    void Awake()
    {
        //Initiate Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else Destroy(this.gameObject);
    }

    void FixedUpdate()
    {
        pressed_up = Input.GetKey(key_up);
        pressed_down = Input.GetKey(key_down);
        pressed_left = Input.GetKey(key_left);
        pressed_right = Input.GetKey(key_right);
        pressed_shoot = Input.GetKey(key_shoot);
        pressed_escape = Input.GetKey(key_escape);
    }
}
