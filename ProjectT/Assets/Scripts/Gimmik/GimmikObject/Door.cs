using UnityEngine;

public class Door : Interactive_Object
{
    protected override void GimmikActivate()
    {
        base.GimmikActivate();
        Debug.Log("문 열림");
    }
    protected override void GimmikDeactivate()
    {
        base.GimmikDeactivate();
        Debug.Log("아이템 없음");
    }
}
