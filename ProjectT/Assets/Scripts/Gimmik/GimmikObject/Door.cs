using UnityEngine;

public class Door : Interactive_Object
{
    protected override void GimmikActivate()
    {
        base.GimmikActivate();
        Debug.Log("�� ����");
    }
    protected override void GimmikDeactivate()
    {
        base.GimmikDeactivate();
        Debug.Log("������ ����");
    }
}
